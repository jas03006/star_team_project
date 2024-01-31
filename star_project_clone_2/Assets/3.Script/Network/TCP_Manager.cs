using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// .net 라이브러리
using System;
//소켓 통신을 하기 위한 라이브러리
using System.Net;
using System.Net.Sockets;
using System.IO;//데이터를 읽고 쓰고 하기 위한 라이브러리
using System.Threading;//멀티 스레딩 하기 위한 라이브러리
using TMPro;

public enum command_flag { 
    join = 0, // 하우스 참가
    move = 1, // 이동
    build = 2, // 건물 설치
    remove = 3, // 건물 제거
    update = 4 // 건물 상태 업데이트
}

public class TCPManger : MonoBehaviour
{
    private string IPAdress = "54.180.24.82";
    private string Port = "7777";

    private Net_Room_Manager net_room_manager;

    [SerializeField] private TMP_Text Status;
    [SerializeField] private bool is_server;
    //기본적인 소켓통신
    //.net -> 패킷 -> Stream 
    // 데이터를 읽는 부분 -> thread 

    StreamReader reader;//데이터를 읽는 놈
    StreamWriter writer;//데이터를 쓰는 놈

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
    private void ServerConnect()//서버를 열어주는 쪽-> 서버를 만드는 쪽
    {
        //지속적으로 사용 -> update 문처럼 사용
        //-> 메세지가 들어올 때마다 열어줌 
        //흐름에다가 예외처리 -> try-catch
        try
        {
            TcpListener tcp =
                new TcpListener(IPAddress.Any,//IPAddress.Parse(IPAdress),
                int.Parse(Port));
            //TcpListener 객체 생성
            tcp.Start();//서버가 시작 -> 서버가 열렸다
            log.Enqueue("Server Open");
            int id_ = 0;
            while (true) {
                TcpClient client = tcp.AcceptTcpClient();

                //TcpListener에 연결이 될 때까지 기다렸다가 
                //연결이 되면 client 할당
                log.Enqueue("Client 접속 확인 완료");

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
                net_room_manager.join_room(req.client , host_id); //룸 참가(서버)
                net_room_manager.room_RPC(host_id, req.msg); // 기존 참여자들에게 새로운 참가자의 정보를 전송
                req.client.writer.WriteLine(req.msg + " " + net_room_manager.get_people_positions(host_id)); // 기존 참여자 위치 정보 전송
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
