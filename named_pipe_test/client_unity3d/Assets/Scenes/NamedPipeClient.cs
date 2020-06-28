using System;
using System.IO;
using System.IO.Pipes;
using UnityEngine;

using System.Security.Principal;

public class NamedPipeClient : MonoBehaviour
{
    // Init
    public string content = "";
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI() {
        content = GUILayout.TextArea(content, GUILayout.Width(200));
        if(GUILayout.Button("SendData"))
        {
            SendData(content);
        }
    }

    private static void SendData(string str)
    {
        try
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "clouddeskpipeTest", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None))
            {
                pipeClient.Connect();
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.WriteLine(str);
                    sw.Flush();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
