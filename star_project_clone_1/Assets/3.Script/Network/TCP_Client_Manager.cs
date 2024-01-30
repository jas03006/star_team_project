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

public class TCP_Client_Manager : MonoBehaviour
{
    private string IPAdress = "127.0.0.1";
    private string Port = "7777";
    private Queue<string> log = new Queue<string>();
    StreamReader reader;//�����͸� �д� ��
    StreamWriter writer;//�����͸� ���� ��
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
    private void client_connect()//������ �����ϴ� �� 
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
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        if (sending_Message($"{(int)command_flag.move} {now_room_id} {UnityEngine.Random.Range(0, 10)}"))
        {


        }
    }
    public void join_btn2()
    {
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        now_room_id = 1;
        if (sending_Message($"{(int)command_flag.join} {now_room_id}"))
        {


        }
    }
    public void join_btn()
    {
        now_room_id = 0;
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        if (sending_Message($"{(int)command_flag.join} {now_room_id}"))
        {


        }
    }

    public void Sending_btn()
    {
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
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
