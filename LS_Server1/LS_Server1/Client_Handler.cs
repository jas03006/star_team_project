using System.Collections;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Threading;
using System.Numerics;
//클라이언트 tcp 연결을 관리하는 handler
public class Client_Handler
{
    public int id = -1;
    public string uuid = "-"; //클라이언트의 id
    public TcpClient client;
    public TCPManger manager;
    public StreamReader reader;
    public StreamWriter writer;
    public string room_id = "-"; //현재 참가중인 마이플래닛 호스트의 id
    public Vector3 position;
    //private Thread listen_thread;
    private Task listen_task; //해당 클라이언트와 tcp 연결을 관리해주는 task
    CancellationTokenSource cts;
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
         cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        listen_task = new Task(process_client, token);
        listen_task.Start();

    }

    //클라이언트의 요청 listen
    public void process_client()
    {
        string readerData;
        try
        {
            while (client.Connected)
            {
               // Console.WriteLine($"wait reading: {uuid}");
                if (!reader.EndOfStream)
                {

                    readerData = reader.ReadLine();
                    if (readerData != string.Empty)
                    {
                        manager.add_request(this, readerData);
                    }
                }
                Thread.Sleep(10);
            }
        }
        catch { 
            
        }
        Console.WriteLine($"Client {uuid} disconnected");
        manager.handler_remove(this);
    }

    public void close()
    {
        if (listen_task != null)
        {
            try
            {
                cts.Cancel();
            }
            catch { }
        }
    }
}
