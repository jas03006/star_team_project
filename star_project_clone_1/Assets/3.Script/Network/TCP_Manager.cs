using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// .net ���̺귯��
using System;
//���� ����� �ϱ� ���� ���̺귯��
using System.Net;
using System.Net.Sockets;
using System.IO;//�����͸� �а� ���� �ϱ� ���� ���̺귯��
using System.Threading;//��Ƽ ������ �ϱ� ���� ���̺귯��
using TMPro;

public enum command_flag { 
    join = 0, // �Ͽ콺 ����
    move = 1, // �̵�
    build = 2, // �ǹ� ��ġ
    remove = 3, // �ǹ� ����
    update = 4 // �ǹ� ���� ������Ʈ
}

public class TCPManger : MonoBehaviour
{
    private string IPAdress = "54.180.24.82";
    private string Port = "7777";

    private Net_Room_Manager net_room_manager;

    [SerializeField] private TMP_Text Status;
    [SerializeField] private bool is_server;
    //�⺻���� �������
    //.net -> ��Ŷ -> Stream 
    // �����͸� �д� �κ� -> thread 

    StreamReader reader;//�����͸� �д� ��
    StreamWriter writer;//�����͸� ���� ��

    private List<Client_Handler> client_handler_list;
    private Queue<Net_Request> request_queue;

    //client
   // private int now_room_id = -1;
    private void Start()
    {
        if (is_server)
        {
            net_room_manager = new Net_Room_Manager();
            client_handler_list = new List<Client_Handler>();
            request_queue = new Queue<Net_Request>();
            StartCoroutine(process_all_client_co());
            Server_open();
        }
        else {
            //Client_Connect();
        }
    }


    private Queue<string> log = new Queue<string>();
    void status_Message()
    {
        if (log.Count > 0)
        {
            Status.text = log.Dequeue();
        }
    }
    #region Server
    // 
    public void Server_open()
    {
        Thread thread = new Thread(ServerConnect);
        thread.IsBackground = true;
        thread.Start();
    }
    private void ServerConnect()//������ �����ִ� ��-> ������ ����� ��
    {
        //���������� ��� -> update ��ó�� ���
        //-> �޼����� ���� ������ ������ 
        //�帧���ٰ� ����ó�� -> try-catch
        try
        {
            TcpListener tcp =
                new TcpListener(IPAddress.Any,//IPAddress.Parse(IPAdress),
                int.Parse(Port));
            //TcpListener ��ü ����
            tcp.Start();//������ ���� -> ������ ���ȴ�
            log.Enqueue("Server Open");
            int id_ = 0;
            while (true) {
                TcpClient client = tcp.AcceptTcpClient();

                //TcpListener�� ������ �� ������ ��ٷȴٰ� 
                //������ �Ǹ� client �Ҵ�
                log.Enqueue("Client ���� Ȯ�� �Ϸ�");

                Client_Handler ch = new Client_Handler(client, this);
                ch.id = id_;
                id_++;
                client_handler_list.Add(ch);

                ch.start();


                /*while (client.Connected)
                {
                    string readData = reader.ReadLine();
                    Debug.Log(readData);
                }*/
            }                  
            
        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);
        }

    }
    #endregion

    private IEnumerator process_all_client_co() {
        while (true) {
            status_Message();
            process_request();
            //process_all_client();
            yield return null;
        }
    }
    private void process_all_client() {
        Client_Handler ci;
        string readerData;
        for ( int i =0; i < client_handler_list.Count; i++){
            ci = client_handler_list[i];
            if (ci.client.Connected)
            {
                readerData = ci.reader.ReadLine();
                if (readerData != string.Empty) {
                    Debug.Log(ci.id + ": " + readerData);
                }               
            }
            else {
                client_handler_list.RemoveAt(i);
                i--;
            }
        }
    }

    public void handler_removeAt(int ind) {
        client_handler_list.RemoveAt(ind);
        //net_room_manager.
    }

    #region parse msg

    public void add_request(Client_Handler client, string msg) {
        request_queue.Enqueue(new Net_Request(client, msg));
    }
    public void process_request() {
        while (request_queue.Count > 0) {
            parse_msg(request_queue.Dequeue());
        }
    }
    public void parse_msg(Net_Request req) {
        Debug.Log(req.client.uuid + ": "+req.msg);
        string[] cmd_arr = req.msg.Split(" ");
        switch ((command_flag)int.Parse(cmd_arr[0])) {
            case command_flag.join:
                int host_id = int.Parse(cmd_arr[1]);
                req.client.uuid = int.Parse(cmd_arr[2]);
                req.client.position = Vector3.forward;
                net_room_manager.join_room(req.client , host_id); //�� ����(����)
                net_room_manager.room_RPC(host_id, req.msg); // ���� �����ڵ鿡�� ���ο� �������� ������ ����
                req.client.writer.WriteLine(req.msg + " " + net_room_manager.get_people_positions(host_id)); // ���� ������ ��ġ ���� ����
                break;
            case command_flag.move:
                req.client.position = new Vector3(float.Parse(cmd_arr[5]), 0, float.Parse(cmd_arr[6]));
                net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                break;
            case command_flag.update:
                net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                break;
            default:
                break;
        } 
    }
    
    #endregion
}
