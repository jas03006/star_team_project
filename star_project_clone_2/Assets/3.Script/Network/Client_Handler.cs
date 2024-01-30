using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Threading;
public class Client_Handler
{
    public int id = -1;
    public TcpClient client;
    public TCPManger manager;
    public StreamReader reader;
    public StreamWriter writer;
    public int room_id;

    public Client_Handler(TcpClient client_, TCPManger manager_)
    {
        client = client_;
        reader = new StreamReader(client.GetStream());
        writer = new StreamWriter(client.GetStream());
        writer.AutoFlush = true;

        manager = manager_;
    }

    public void start()
    {
        Thread t = new Thread(new ThreadStart(process_client));
        t.Start();
    }

    public void process_client() {
        string readerData;
        while (client.Connected)
        {
            readerData = reader.ReadLine();
            if (readerData != string.Empty)
            {                
                manager.add_request(this,  readerData);
            }
        }

        manager.handler_removeAt(id);
    }
}
