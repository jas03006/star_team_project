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
using System.Text;

public enum command_flag
{
    join = 0, // �Ͽ콺 ����
    exit = 1, // �Ͽ콺 ����
    move = 2, // �̵�
    build = 3, // �ǹ� ��ġ
    remove = 4, // �ǹ� ����
    update = 5, // �ǹ� ���� ������Ʈ
    chat = 6 //ä�� ����
}

public class TCP_Client_Manager : MonoBehaviour
{
    public static TCP_Client_Manager instance = null;

    private string IPAdress = "127.0.0.1";//"13.125.169.138";
    private string Port = "7777";
    private Queue<string> log = new Queue<string>();
    StreamReader reader;//�����͸� �д� ��
    StreamWriter writer;//�����͸� ���� ��
    private int now_room_id = -1;

    [SerializeField] private List<Button> button_list;
    private Dictionary<string, Net_Move_Object_TG> net_mov_obj_dict; //object_id, object
    private Queue<string> msg_queue;

    [SerializeField] private Player_Network_TG my_player;
    [SerializeField] private GameObject guest_prefab;
    
    TcpClient client;
    private int respawn_flag = 7777; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }

    private void Start()
    {
        msg_queue = new Queue<string>();
        net_mov_obj_dict = new Dictionary<string, Net_Move_Object_TG>();
        //Client_Connect();
    }
    private void Update()
    {
        while (msg_queue.Count > 0)
        {
            parse_msg(msg_queue.Dequeue());
        }
    }
    public void load_house()
    {
        hide_buttons();
    }

    #region client connection
    public void Client_Connect()
    {
        if (client != null) {
            return;
        }
        log.Enqueue("client_connect");
        Thread thread = new Thread(client_connect);
        thread.IsBackground = true;
        thread.Start();
        while (client ==null || !client.Connected || writer == null) {
            Debug.Log("wait connection...");
        }
        return;
    }
    private void client_connect()//������ �����ϴ� �� 
    {
        try
        {
            client = new TcpClient();
            //Server =IP Start point -> cleint = ip end point
            IPEndPoint ipent =
                new IPEndPoint(IPAddress.Parse(IPAdress),
                int.Parse(Port));
            client.Connect(ipent);
            log.Enqueue("client Server Connect Compelete!");

            net_mov_obj_dict[my_player.object_id.ToString()] = my_player;
            reader = new StreamReader(client.GetStream());

            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

            

            while (client.Connected)
            {
                string readerData = reader.ReadLine();
                if (readerData != null && !readerData.Equals(string.Empty)) {
                    msg_queue.Enqueue(readerData);
                    //parse_msg(readerData);                   
                }
                
            }
        }
        catch (Exception e)
        {
            log.Enqueue(e.Message);
        }
    }
    #endregion

    #region parsing
    private void parse_msg(string msg) {
        if (msg == BitConverter.GetBytes((int)0).ToString()) { // connection check byte 
            return;
        }
        Debug.Log(msg);
        string[] cmd_arr = msg.Split(" ");
      //  try
       // {
            int uuid_;
            switch ((command_flag)int.Parse(cmd_arr[0]))
            {
                case command_flag.join: // join rood_id host_id position_data
                    
                    uuid_ = int.Parse(cmd_arr[2]);
                    if (my_player.object_id != uuid_)
                    {
                        create_guest(uuid_, get_respawn_point(uuid_)); //ù ������ ��ǥ�� �ֱ�
                    }
                    else {                        
                        if (cmd_arr.Length > 3) {
                            remove_all_guest(except_self: true);
                            net_mov_obj_dict[cmd_arr[2]] = my_player;
                            now_room_id = int.Parse(cmd_arr[1]);
                            creat_all_guest(cmd_arr[3]);
                        }                        
                    }   
                    break;
                case command_flag.exit: // exit rood_id host_id
                    uuid_ = int.Parse(cmd_arr[2]);
                    if (my_player.object_id != uuid_)
                    {
                        remove_guest(uuid_);
                    }
                    else {
                        exit_room();
                    }                    
                    break;
                case command_flag.move:                    
                    Vector3 start_pos = new Vector3(float.Parse(cmd_arr[3]), 0, float.Parse(cmd_arr[4]));
                    if (start_pos.x == respawn_flag) {
                        start_pos = get_respawn_point(int.Parse(cmd_arr[2]));
                    }
                    net_mov_obj_dict[cmd_arr[2]].move(start_pos, new Vector3(float.Parse(cmd_arr[5]), 0, float.Parse(cmd_arr[6])));
                    break;
                case command_flag.update:
                    uuid_ = int.Parse(cmd_arr[1]);
                    if (my_player.object_id != uuid_)
                    {
                        Debug.Log("update!");
                    }                    
                    break;
                case command_flag.chat:
                    uuid_ = int.Parse(cmd_arr[1]);
                    Debug.Log(cmd_arr[2]+": "+ cmd_arr[3]);
                    break;
                default:
                    break;
            }
       // } catch {
       //     Debug.Log("parse error!");
       // }
       
    }
    #endregion

    public void exit_room() {
        now_room_id = -1;
        client = null;
    }

    public Vector3 get_respawn_point(int uuid_) {
        if (uuid_ == now_room_id) {
            return Vector3.zero;
        }
        return Vector3.forward*3;
    }

    #region guest management
    private void create_guest(int uuid_, Vector3 position) {
        if (position.x == respawn_flag) {
            position = get_respawn_point(uuid_);
        }
        GameObject new_guest = GameObject.Instantiate(guest_prefab, position, Quaternion.identity);
        Net_Move_Object_TG nmo = new_guest.GetComponent<Net_Move_Object_TG>();
        nmo.init(uuid_, true);
        net_mov_obj_dict[uuid_.ToString()] = nmo;
        //TODO: uuid�� �̿��� DB���� ���� ������ �޾ƿͼ� ��Ų �� ������ ������ ������ �߰��ؾ���
    }
    private void creat_all_guest(string position_datas)
    {

        string[] position_datas_arr = position_datas.Split("|"); // uuid vector3

        for (int i = 0; i < position_datas_arr.Length - 1; i++)
        {
            string[] data_pair = position_datas_arr[i].Split(":");
            int uuid_ = int.Parse(data_pair[0]);
            Vector3 position_ = new Vector3(float.Parse(data_pair[1]), my_player.transform.position.y, float.Parse(data_pair[3]));
            if (uuid_ == my_player.object_id)
            {
                if (position_.x == respawn_flag)
                {
                    position_ = get_respawn_point(uuid_);
                }
                my_player.transform.position = position_;
            }
            else
            {
                create_guest(uuid_, position_);
            }
        }
    }

    private void remove_guest(int uuid_) {
        Debug.Log($"remove: {uuid_}");
        Net_Move_Object_TG guest = net_mov_obj_dict[uuid_.ToString()];
        Destroy(guest.gameObject);
        net_mov_obj_dict.Remove(uuid_.ToString());
    }

    private void remove_all_guest(bool except_self = false) {
        foreach (string key in net_mov_obj_dict.Keys) {
            if (except_self && my_player.object_id == int.Parse(key)) {
                continue;
            }
            Destroy(net_mov_obj_dict[key].gameObject);
        }
        net_mov_obj_dict.Clear();
    }
   
    #endregion

    #region request
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
    public bool send_move_request(int object_id, Vector3 start_pos, Vector3 dest_pos)
    {
        return sending_Message($"{(int)command_flag.move} {now_room_id} {object_id} {start_pos.x} {start_pos.z} {dest_pos.x} {dest_pos.z}");
    }
    public bool send_join_request(int room_id, int object_id)
    {
        return sending_Message($"{(int)command_flag.join} {room_id} {object_id}");
    }
    public bool send_exit_request(int room_id, int object_id)
    {
        return sending_Message($"{(int)command_flag.exit} {room_id} {object_id}");
    }
    public bool send_update_request()
    {
        return sending_Message($"{(int)command_flag.update} {now_room_id}");
    }
    public bool send_chat_request(string chat_msg)
    {
        return sending_Message($"{(int)command_flag.chat} {now_room_id} {my_player.object_id} {chat_msg}");
    }
    #endregion   

    #region test buttons
    public void hide_buttons()
    {
        for (int i = 0; i < button_list.Count; i++)
        {
            button_list[i].gameObject.SetActive(false);
        }
    }

    public void send_chat_button() {
        string chat_msg = "hihi";
        send_chat_request(chat_msg);
    }

    public void set_id_btn() {
        my_player.init(11);
       
    }
    public void set_id_btn2()
    {
        my_player.init(22);

    }
    public void set_id_btn3()
    {
        my_player.init(33);
        
    }
    public void join_btn()
    {
        Client_Connect();
        now_room_id = 11;
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        if (send_join_request(now_room_id, my_player.object_id))
        {
            load_house();
        }
    }
    public void join_btn2()
    {
        Client_Connect();
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        now_room_id = 22;
        if (send_join_request(now_room_id, my_player.object_id))
        {
            load_house();
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

    public void move_btn()
    {
        //���� �޼����� ���´ٸ�
        //���� ���� �޼����� message box�� ���� ��        
        if (sending_Message($"{(int)command_flag.move} {now_room_id} {UnityEngine.Random.Range(0, 10)}"))
        {


        }
    }

    
    #endregion
}
