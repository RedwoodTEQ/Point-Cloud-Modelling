using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;

namespace client_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "clouddeskpipeTest", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None))
                {
                    pipeClient.Connect();
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.WriteLine("Hello world");
                        sw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
