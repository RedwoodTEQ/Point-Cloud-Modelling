using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace namedPipeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WaitData();
        }

        private static void WaitData()
        {
            Console.WriteLine("Pipe server init. Pipe name: clouddeskpipeTest");
            while (true)
            {
                try
                {
                    NamedPipeServerStream pipeServer = new NamedPipeServerStream("clouddeskpipeTest", PipeDirection.InOut, 2);
                    pipeServer.WaitForConnection();
                    StreamReader sr = new StreamReader(pipeServer);
                    string con = sr.ReadLine();

                    Console.WriteLine("Pipe server content: " + con);

                    sr.Close();
                    Thread.Sleep(50);

                    Console.WriteLine("Pipe server waiting ...");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Pipe server exeption: " + ex.Message);
                }
            }
        }
    }
}
