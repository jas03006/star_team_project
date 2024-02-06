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
    public static TCP_Client_Manager instance = null;

    private string IPAdress = "3.38.246.236";//"13.125.236.235";
    private string Port = "7777";
    private Queue<string> log = new Queue<string>();
    StreamReader reader;//데이터를 읽는 놈
    StreamWriter writer;//데이터를 쓰는 놈
    private int now_room_id = -1;

    [SerializeField] private List<Button> button_list;
    private Dictionary<string, Net_Move_Object_TG> net_mov_obj_dict; //object_id, object
    private Queue<string> msg_queue;

    [SerializeField] private Player_Network_TG my_player;
    [SerializeField] private GameObject guest_prefab;
 
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

            net_mov_obj_dict[my_player.object_id.ToString()] = my_player;

            while (client.Connected)
            {
                string readerData = reader.ReadLine();
                if (readerData != null && !readerData.Equals(string.Empty)) {
                    Debug.Log(readerData);
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
        string[] cmd_arr = msg.Split(" ");
        try
        {
            int uuid_;
            switch ((command_flag)int.Parse(cmd_arr[0]))
            {
                case command_flag.join: // join rood_id host_id position_data
                    uuid_ = int.Parse(cmd_arr[2]);
                    if (my_player.object_id != uuid_)
                    {
                        create_guest(uuid_, Vector3.forward);
                    }
                    else {
                        if (cmd_arr.Length > 3) {
                            creat_all_guest(cmd_arr[3]);
                        }                        
                    }   
                    break;
                case command_flag.move:
                    net_mov_obj_dict[cmd_arr[2]].move(new Vector3(float.Parse(cmd_arr[3]), 0, float.Parse(cmd_arr[4])), new Vector3(float.Parse(cmd_arr[5]), 0, float.Parse(cmd_arr[6])));
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
        } catch {
            Debug.Log("parse error!");
        }
       
    }
    #endregion

    #region guest creation
    private void create_guest(int uuid_, Vector3 position) {
        GameObject new_guest = GameObject.Instantiate(guest_prefab, position, Quaternion.identity);
        Net_Move_Object_TG nmo = new_guest.GetComponent<Net_Move_Object_TG>();
        nmo.init(uuid_, true);
        net_mov_obj_dict[uuid_.ToString()] = nmo;
        //TODO: uuid를 이용해 DB에서 유저 정보를 받아와서 스킨 등 정보를 입히는 과정을 추가해야함
    }
    private void creat_all_guest(string position_datas)
    {
        
        string[] position_datas_arr = position_datas.Split("|"); // uuid vector3

        for (int i =0; i < position_datas_arr.Length-1;i++) {
            string[] data_pair = position_datas_arr[i].Split(":");
            int uuid_ = int.Parse(data_pair[0]);
            Vector3 position_ = new Vector3(float.Parse(data_pair[1]), my_player.transform.position.y, float.Parse(data_pair[3]));
            if (uuid_ == my_player.object_id)
            {
                my_player.transform.position = position_;
            }
            else {
                create_guest(uuid_, position_);
            }            
        }
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
        Client_Connect();
    }
    public void set_id_btn2()
    {
        my_player.init(22);
        Client_Connect();
    }
    public void set_id_btn3()
    {
        my_player.init(33);
        Client_Connect();
    }
    public void join_btn()
    {
        now_room_id = 11;
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        if (send_join_request(now_room_id, my_player.object_id))
        {
            load_house();
        }
    }
    public void join_btn2()
    {
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        now_room_id = 22;
        if (send_join_request(now_room_id, my_player.object_id))
        {
            load_house();
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

    public void move_btn()
    {
        //만약 메세지를 보냈다면
        //내가 보낸 메세지도 message box에 넣을 것        
        if (sending_Message($"{(int)command_flag.move} {now_room_id} {UnityEngine.Random.Range(0, 10)}"))
        {


        }
    }

    
    #endregion
}
