using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public class newTCPClient : MonoBehaviour
{

    public int port;
    public string ip;
    public string TestMesage = "Hello";

    private TcpClient tcpClient;

    private Task clientListenTask;




    void Start()
    {

        clientListenTask = Task.Run(Listen);
    }

    private void Listen()
    {
        Debug.Log(tcpClient);
        tcpClient = new TcpClient(ip, port);
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Debug.Log(tcpClient);
        });
    }
    

    [ContextMenu("Test")]
    public void SendMsg()
    {
        Debug.Log("sent");
        if (tcpClient == null)
        {
            return;
            
        }

        NetworkStream stream = tcpClient.GetStream();

        byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(TestMesage);

        stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
    }
}
