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

public class TCP_Client_Manager : MonoBehaviour
{
    private string IPAdress = "127.0.0.1";
    private string Port = "7777";
    private Queue<string> log = new Queue<string>();
    StreamReader reader;//데이터를 읽는 놈
    StreamWriter writer;//데이터를 쓰는 놈
    private int now_room_id = -1;

    #region client

    private void Start()
    {
        Client_Connect();
    }

    public void Client_Connect()
    {
        log.Enqueue("client_connect");
        Thread thread = new Thread(client_connect);
        thread.IsBackground = true;
        thread.Start();
    }
    private void client_connect()//서버에 접근하는 쪽 
    {
        try
        {
            TcpClient client = new TcpClient();
            //Server =IP Start point -> cleint = ip end point
            IPEndPoint ipent =
                new IPEndPoint(IPAddress.Parse(IPAdress),
                int.Parse(Port));
            client.Connect(ipent);
            log.Enqueue("client Server Connect Compelete!");


            reader = new StreamReader(client.GetStream());

            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

            while (client.Connected)
            {
                string readerData = reader.ReadLine();
                Debug.Log(readerData);
            }
        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);
        }
    }
    public void move_btn()
    {
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        if (sending_Message($"{(int)command_flag.move} {now_room_id} {UnityEngine.Random.Range(0, 10)}"))
        {


        }
    }
    public void join_btn2()
    {
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        now_room_id = 1;
        if (sending_Message($"{(int)command_flag.join} {now_room_id}"))
        {


        }
    }
    public void join_btn()
    {
        now_room_id = 0;
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        if (sending_Message($"{(int)command_flag.join} {now_room_id}"))
        {


        }
    }

    public void Sending_btn()
    {
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        if (sending_Message("hi" + UnityEngine.Random.Range(0, 10)))
        {


        }
    }
    #endregion

    private bool sending_Message(string me)
    {
        if (writer != null)
        {
            writer.WriteLine(me);
            return true;
        }
        else
        {
            Debug.Log("Writer Null");
            return false;
        }
    }
}
