using System.Collections;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Threading;
using System.Numerics;
public class Client_Handler
{
    public int id = -1;
    public int uuid = -1;
    public TcpClient client;
    public TCPManger manager;
    public StreamReader reader;
    public StreamWriter writer;
    public int room_id;
    public Vector3 position;
    Thread listen_thread;
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
        listen_thread = new Thread(process_client);
        listen_thread.IsBackground = true;
        listen_thread.Start();

    }

    public void process_client()
    {
        string readerData;
        //float timer = 0f;
        while (client.Connected)
        {
            Thread.Sleep(10);
            if (!reader.EndOfStream)
            {

                readerData = reader.ReadLine();
                if (readerData != string.Empty)
                {
                    manager.add_request(this, readerData);
                }
            }
        }

        manager.handler_removeAt(id);
    }

    public void close()
    {
        if (listen_thread != null)
        {
            try
            {
                listen_thread.Abort();
                listen_thread.Join();
            }
            catch { }
        }
    }
}
