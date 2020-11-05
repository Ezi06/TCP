using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System;






public class NewTCPServer : MonoBehaviour
{
    public int port;

    private TcpListener tcpListener;

    private Task clientListenTask;

    private TcpClient connectedTcpClient;

    void Start()
    {

        clientListenTask = Task.Run(Listen);
    }

    private void Listen()
    {
        tcpListener = new TcpListener(IPAddress.Any, port);
        tcpListener.Start();

        Byte[] bytes = new byte[1024];

        while (true)
        {

            using (connectedTcpClient = tcpListener.AcceptTcpClient())
            {
                using (NetworkStream stream = connectedTcpClient.GetStream())
                {
                    int length;

                    while ((length = stream.Read(bytes, 0 , bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);

                        string clientMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("client message received:" + clientMessage);
                    }
                }
            }

        }

    }


    
}
