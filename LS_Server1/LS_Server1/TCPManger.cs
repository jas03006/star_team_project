﻿using System.Collections;
using System.Collections.Generic;

// .net 라이브러리
using System;
//소켓 통신을 하기 위한 라이브러리
using System.Net;
using System.Net.Sockets;
using System.IO; //데이터를 읽고 쓰고 하기 위한 라이브러리
using System.Threading; //멀티 스레딩 하기 위한 라이브러리

using System.Diagnostics;
using System.Numerics;
public enum command_flag
{
    join = 0, // 하우스 참가
    exit = 1, // 하우스 퇴장
    move = 2, // 이동
    build = 3, // 건물 설치
    remove = 4, // 건물 제거
    update = 5, // 건물 상태 업데이트
    chat = 6, //채팅 전송
    interact = 7, //채팅 전송
    invite = 8 //초대
}

public class TCPManger 
{
    private string IPAdress = "13.125.236.235";
    private string Port = "7777";

    private Net_Room_Manager net_room_manager;

   
    private bool is_server;
    //기본적인 소켓통신
    //.net -> 패킷 -> Stream 
    // 데이터를 읽는 부분 -> thread 

    StreamReader reader;//데이터를 읽는 놈
    StreamWriter writer;//데이터를 쓰는 놈

    private List<Client_Handler> client_handler_list;
    private Queue<Net_Request> request_queue;
    Thread thread;
    Thread processing_thread;

    private int respawn_flag = 7777;
    //client
    // private int now_room_id = -1;
    public void start()
    {
        net_room_manager = new Net_Room_Manager();
        client_handler_list = new List<Client_Handler>();
        request_queue = new Queue<Net_Request>();

        processing_thread = new Thread(process_all_client_co);
        processing_thread.IsBackground = true;
        processing_thread.Start();
        // StartCoroutine(process_all_client_co());
        Server_open();
        // process_all_client_co();
        while (true) {
            Thread.Sleep(500);
            check_client_connections();
            status_Message();
        }
    }


    private Queue<string> log = new Queue<string>();
    void status_Message()
    {
        if (log.Count > 0)
        {
            //Status.text = log.Dequeue();
            Console.WriteLine(log.Dequeue());
        }
    }
    #region Server
    // 
    public void Server_open()
    {
        thread = new Thread(ServerConnect);
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
            while (true)
            {
                TcpClient client = tcp.AcceptTcpClient();

                //TcpListener에 연결이 될 때까지 기다렸다가 
                //연결이 되면 client 할당
                log.Enqueue("Client 접속 확인 완료");

                Client_Handler ch = new Client_Handler(client, this);
                ch.id = id_;                
                id_++;
                client_handler_list.Add(ch);

                ch.start();
            }

        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);
        }

    }
    #endregion

    private void process_all_client_co()
    {
        while (true)
        {
           // Thread.Sleep(20);
            
            process_request();
            //process_all_client();
           // yield return null;
        }
    }
    private void process_all_client()
    {
        Client_Handler ci;
        string readerData;
        for (int i = 0; i < client_handler_list.Count; i++)
        {
            ci = client_handler_list[i];
            if (ci.client.Connected)
            {
                readerData = ci.reader.ReadLine();
                if (readerData != string.Empty)
                {
                    Console.WriteLine(ci.id + ": " + readerData);
                }
            }
            else
            {
                client_handler_list.RemoveAt(i);
                i--;
            }
        }
    }

    public void handler_remove(Client_Handler ch)
    {
        int room_id = ch.room_id;
        if (room_id != -1) {
            net_room_manager.remove_from_room(ch, room_id);
        }        
        client_handler_list.Remove(ch);
    }

    public void check_client_connections() {
        for (int i =0; i < client_handler_list.Count; i++) {
            
            if (!client_handler_list[i].client.Connected) {
                Console.WriteLine($"Client {client_handler_list[i].uuid} disconnected");
                client_handler_list[i].close();
                client_handler_list.RemoveAt(i);
                i--;
            }
            try
            {
                //Console.WriteLine($"Check Connection: {client_handler_list[i].uuid}");
                client_handler_list[i].writer.WriteLine(BitConverter.GetBytes((int)0));
            }
            catch { 
            
            }
        }
    }

    #region parse msg

    public void add_request(Client_Handler client, string msg)
    {
        request_queue.Enqueue(new Net_Request(client, msg));
    }
    public void process_request()
    {
        while (request_queue.Count > 0)
        {
            parse_msg(request_queue.Dequeue());
        }
    }
    public void parse_msg(Net_Request req)
    {
        try
        {
            Console.WriteLine(req.client.uuid + ": " + req.msg);
            string[] cmd_arr = req.msg.Split(" ");
            int host_id;
            switch ((command_flag)int.Parse(cmd_arr[0]))
            {
                case command_flag.join:
                    host_id = int.Parse(cmd_arr[1]);
                    req.client.uuid = int.Parse(cmd_arr[2]);

                    net_room_manager.remove_from_room(req.client, req.client.room_id); //기존 있던 방에서 탈퇴

                    if (host_id == -1) { //글로벌 접속이라면 (방에서만 나가기)               
                        break;
                    }

                    req.client.position = new Vector3(respawn_flag, 0,0); //첫 리스폰을 알리는 좌표
                    net_room_manager.join_room(req.client, host_id); //룸 참가(서버)
                    net_room_manager.room_RPC(host_id, req.msg); // 기존 참여자들에게 새로운 참가자의 정보를 전송
                    req.client.writer.WriteLine(req.msg + " " + net_room_manager.get_people_positions(host_id)); // 기존 참여자 위치 정보 전송
                    break;
                case command_flag.exit: //게임에서 나가기
                    //net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                    net_room_manager.remove_from_room(req.client, req.client.room_id); //기존 있던 방에서 탈퇴
                    handler_remove(req.client);
                    req.client.close();
                    break;
                case command_flag.move:
                    req.client.position = new Vector3(float.Parse(cmd_arr[5]), 0, float.Parse(cmd_arr[6]));
                    net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                    break;
                case command_flag.update:
                    net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                    break;
                case command_flag.chat:
                    host_id = int.Parse(cmd_arr[1]);
                    if (host_id == -1) //global chat
                    {
                        every_RPC(req.msg);
                    }
                    else {
                        net_room_manager.room_RPC(host_id, req.msg);
                    }
                    
                    break;
                case command_flag.interact:
                    net_room_manager.room_RPC(int.Parse(cmd_arr[1]), req.msg);
                    break;
                case command_flag.invite:                    
                    target_RPC(int.Parse(cmd_arr[2]), req.msg);
                    break;
                default:
                    break;
            }
        }
        catch
        {
            Console.WriteLine("Parse Error");
        }
    }

    #endregion

    #region RPC
    public void global_world_RPC(string msg) {
        foreach (Client_Handler ch in client_handler_list)
        {
            if (ch.room_id == -1) {
            ch.writer.WriteLine(msg);
            }
        }
    }

    public void every_RPC(string msg)
    {
        foreach (Client_Handler ch in client_handler_list)
        {
            ch.writer.WriteLine(msg);           
        }
    }
    public void target_RPC(int uuid_, string msg)
    {
        foreach (Client_Handler ch in client_handler_list)
        {
            if (ch.uuid == uuid_)
            {
                ch.writer.WriteLine(msg);
                break;
            }
        }
    }
    #endregion

    #region thread closing
    private void close_all_thread()
    {
        for (int i = 0; i < client_handler_list.Count; i++)
        {
            client_handler_list[i].close();
        }
    }

    private void OnApplicationQuit()
    {
        thread.Abort();
        thread.Join();
        processing_thread.Abort();
        processing_thread.Join();
        close_all_thread();
    }
    private void OnDestroy()
    {
        thread.Abort();
        thread.Join();
        processing_thread.Abort();
        processing_thread.Join();
        close_all_thread();
    }
    #endregion
}
