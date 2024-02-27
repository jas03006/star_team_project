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
using System.Text;
using Unity.VisualScripting;

using UnityEngine.SceneManagement;
using UnityEditor;
using BackEnd;
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

// 현재
// 로비 Scene(글로벌 채팅) -> 하우징 Scene(하우스 채팅)
// my_player, TCP_Client_Manager를 DonDestory해서 계속 쓰도록 함
public class TCP_Client_Manager : MonoBehaviour
{
    public static TCP_Client_Manager instance = null;

    private string IPAdress = "13.125.169.138";
    private string Port = "7777";
    private Queue<string> log = new Queue<string>();
    StreamReader reader;//데이터를 읽는 놈
    StreamWriter writer;//데이터를 쓰는 놈
    public string now_room_id { private set; get; } = "-";
    public string target_room_id { private set; get; } = "-";

    [SerializeField] private List<Button> set_button_list;
    [SerializeField] private List<Button> lobby_button_list;
    [SerializeField] private List<GameObject> planet_button_list;
    [SerializeField] private List<Button> stage_button_list;
    [SerializeField] private string planet_scene_name;
    [SerializeField] private string lobby_scene_name;
    [SerializeField] private string stage_scene_name;
    public Dictionary<string, Net_Move_Object_TG> net_mov_obj_dict; //object_id, object
    private Queue<string> msg_queue;

    [SerializeField] public PlayerMovement my_player;
    [SerializeField] private GameObject guest_prefab;
    
    TcpClient client;
    private int respawn_flag = 7777;

    public Housing_UI_Manager housing_ui_manager;
    [SerializeField] private ChatBoxManager chat_box_manager;

    [SerializeField] private GameObject invite_UI;
    [SerializeField] private TMP_Text invite_text;
    [SerializeField] private Button invite_agree_button;

    public PlacementSystem placement_system;
    public Camera_My_Planet camera_my_planet;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        msg_queue = new Queue<string>();
        net_mov_obj_dict = new Dictionary<string, Net_Move_Object_TG>();

        init(Backend.UserNickName);
        //Client_Connect();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == planet_scene_name)
        {
            my_player.find_grid();
            placement_system = FindObjectOfType<PlacementSystem>();
            housing_ui_manager = FindObjectOfType<Housing_UI_Manager>();
            camera_my_planet = FindObjectOfType<Camera_My_Planet>();            
        }
    }
    private void Update()
    {
        while (msg_queue.Count > 0)
        {
            parse_msg(msg_queue.Dequeue());
        }
    }

    public void init(string uuid_)
    {
        my_player.init(uuid_);
        join_global();
    }

    public void load_house()
    {
        hide_lobby_buttons();
        my_player.stop_DOTween();
        placement_system.init_house(now_room_id);
        housing_ui_manager.init_housing_UI();
        //housing 생성, 배치, 업데이트 등등 0213
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
        Debug.Log("Connected!");
        return;
    }
    private void client_connect()//서버에 접근하는 쪽 
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
        //Debug.Log(msg);
        string[] cmd_arr = msg.Split(" ");
        // try
        //{
            string uuid_;
            string host_id;
            switch ((command_flag)int.Parse(cmd_arr[0]))
            {
                case command_flag.join: // join rood_id host_id position_data

                uuid_ = cmd_arr[2];
                    if (my_player.object_id != uuid_)
                    {
                        create_guest(uuid_, get_respawn_point(uuid_)); //첫 리스폰 좌표를 넣기
                    }
                    else {                        
                        if (cmd_arr.Length > 3) {
                            chat_box_manager.clear();
                            remove_all_guest(except_self: true);

                            host_id = cmd_arr[1];
                            now_room_id = host_id;
                            /*if (host_id == -1) { //글로벌 채팅 방에 접속하는 것이라면 //글로벌로 나가는 경우 RPC를 본인에게 쏘지 않는 것으로 함
                                my_player.transform.position = Vector3.zero;
                                break;
                            }*/

                            net_mov_obj_dict[cmd_arr[2]] = my_player;                            
                            creat_all_guest(cmd_arr[3]);
                        }                        
                    }   
                    break;
                case command_flag.exit: // exit room_id host_id //게임 나가기 (방 나가기와 다른것으로 함)
                    uuid_ = cmd_arr[2];
                    if (my_player.object_id != uuid_)
                    {
                        remove_guest(uuid_);
                    }
                    else {
                        exit_game();
                    }                    
                    break;
                case command_flag.move:
                    if (cmd_arr[2] != my_player.object_id) {
                        Vector3 start_pos = new Vector3(float.Parse(cmd_arr[3]), 0, float.Parse(cmd_arr[4]));
                        if (start_pos.x == respawn_flag)
                        {
                            start_pos = get_respawn_point(cmd_arr[2]);
                        }
                        net_mov_obj_dict[cmd_arr[2]].move(start_pos, new Vector3(float.Parse(cmd_arr[5]), 0, float.Parse(cmd_arr[6])));
                    }                    
                    break;
                case command_flag.update:
                    host_id = cmd_arr[1];
                    uuid_ = cmd_arr[2];
                    if (now_room_id== host_id && my_player.object_id != uuid_)
                    {
                        load_house();
                        Debug.Log("update!");
                    }                    
                    break;
                case command_flag.chat:
                    host_id = cmd_arr[1];
                    string chat_msg = string.Empty;
                    for (int i=3; i< cmd_arr.Length; i++) {
                        chat_msg += cmd_arr[i] + " ";
                    }                    
                    Debug.Log(chat_msg);
                    //if ()
                    //{ // 글로벌 채팅인 경우 
                        chat_box_manager.chat(chat_msg, host_id == "-");
                    /*}
                    else {
                    chat_box_manager.chat(chat_msg);
                    }*/                    
                    break;
                case command_flag.interact:
                    //TODO: 오브젝트 인터액션 (오브젝트를 모두 담고있는 class를 하나 구현한 뒤, object id에 맞는 오브젝트의 상호작용 실행)
                    break;
                case command_flag.invite:
                    //TODO: 초대 알림 띄우기
                    if (cmd_arr[2] == my_player.object_id) {
                        show_invite_UI(cmd_arr[1]);
                    }                
                break;
            default:
                    break;
            }
       // } catch (Exception e){
       //     Debug.Log($"parse error! {e.Message}");
       // }
       
    }
    #endregion

    #region function
    public void invite(string uuid_)
    {

        send_invite_request(uuid_);

    }
    public void join(string room_id_)
    {
        target_room_id = room_id_;
        if (!SceneManager.GetActiveScene().name.Equals(planet_scene_name))
        {            
            SceneManager.sceneLoaded -= join_OnSceneLoaded;
            SceneManager.sceneLoaded += join_OnSceneLoaded;
            SceneManager.LoadScene(planet_scene_name);
        }
        else {
            send_join_and_load(target_room_id);
        }
    }
    public void join_OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == planet_scene_name)
        {
            send_join_and_load_target();
        }
    }
    public void send_join_and_load_target()
    {
        send_join_and_load(target_room_id);
    }
    public void send_join_and_load(string room_id_) {
        if (send_join_request(room_id_, my_player.object_id))
        {
            now_room_id = room_id_;
            hide_lobby_buttons();
            load_house();
        }
    }
    public void go_myplanet()
    {
        target_room_id = my_player.object_id;
        SceneManager.sceneLoaded -= join_OnSceneLoaded;
        SceneManager.sceneLoaded += join_OnSceneLoaded;
        SceneManager.LoadScene(planet_scene_name);
       // send_join_and_load(my_player.object_id);
    }
    public void exit_game()
    {
        Application.Quit();
        //client = null;
    }
    public void exit_room() {
        now_room_id = "-";
        chat_box_manager.clear();        
        remove_all_guest(except_self: true);
        my_player.stop_DOTween();
        my_player.transform.position = -Vector3.right * 1000f;        
        //SceneManager.LoadScene(lobby_scene_name);
    }

    public Vector3 get_respawn_point(string uuid_) {
        return placement_system.get_spawn_point(uuid_ == now_room_id);
    }
    public Vector3 get_respawn_point(bool is_my_planet)
    {
        return placement_system.get_spawn_point(is_my_planet);
    }
    #endregion

    #region guest management
    private void create_guest(string uuid_, Vector3 position) {
        if (position.x == respawn_flag) {
            position = get_respawn_point(uuid_);
        }
        GameObject new_guest = GameObject.Instantiate(guest_prefab, position, Quaternion.identity);
        Net_Move_Object_TG nmo = new_guest.GetComponent<Net_Move_Object_TG>();
        nmo.init(uuid_, true);
        (nmo as PlayerMovement).look_user();
        net_mov_obj_dict[uuid_.ToString()] = nmo;
        //TODO: uuid를 이용해 DB에서 유저 정보를 받아와서 스킨 등 정보를 입히는 과정을 추가해야함
    }
    private void creat_all_guest(string position_datas)
    {

        string[] position_datas_arr = position_datas.Split("|"); // uuid vector3

        for (int i = 0; i < position_datas_arr.Length - 1; i++)
        {
            string[] data_pair = position_datas_arr[i].Split(":");
            string uuid_ = data_pair[0];
            Vector3 position_ = new Vector3(float.Parse(data_pair[1]), my_player.transform.position.y, float.Parse(data_pair[3]));
            if (uuid_ == my_player.object_id)
            {
                if (position_.x == respawn_flag)
                {
                    position_ = get_respawn_point(uuid_);
                }
                my_player.transform.position = position_;

                camera_my_planet.init();
                my_player.look_user();
            }
            else
            {
                create_guest(uuid_, position_);
            }
        }
    }

    private void remove_guest(string uuid_) {
        Debug.Log($"remove: {uuid_}");
        Net_Move_Object_TG guest = net_mov_obj_dict[uuid_.ToString()];
        Destroy(guest.gameObject);
        net_mov_obj_dict.Remove(uuid_.ToString());
    }

    private void remove_all_guest(bool except_self = false) {
        foreach (string key in net_mov_obj_dict.Keys) {
            if (except_self && my_player.object_id == key) {
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
    public bool send_move_request(string object_id, Vector3 start_pos, Vector3 dest_pos)
    {
        return sending_Message($"{(int)command_flag.move} {now_room_id} {object_id} {start_pos.x} {start_pos.z} {dest_pos.x} {dest_pos.z}");
    }
    public bool send_join_request(string room_id, string object_id)
    {
        return sending_Message($"{(int)command_flag.join} {room_id} {object_id}");
    }
    public bool send_exit_request(string room_id, string object_id)
    {
        return sending_Message($"{(int)command_flag.exit} {room_id} {object_id}");
    }
    public bool send_update_request()
    {
        return sending_Message($"{(int)command_flag.update} {now_room_id} {my_player.object_id}");
    }
    public bool send_chat_request(string chat_msg, bool is_global=false)
    {
        if (is_global) {
            return sending_Message($"{(int)command_flag.chat} {"-"} {my_player.object_id} {chat_msg}");
        }
        return sending_Message($"{(int)command_flag.chat} {now_room_id} {my_player.object_id} {chat_msg}");
    }
    public bool send_interact_request(string object_id, int interaction_id, int param)
    {
        return sending_Message($"{(int)command_flag.interact} {now_room_id} {object_id} {interaction_id} {param}");
    }
    public bool send_invite_request(string object_id)
    {
        if (now_room_id  != "-" && now_room_id == my_player.object_id) {
            return sending_Message($"{(int)command_flag.invite} {now_room_id} {object_id}");
        }
        return false;
    }

    #endregion

    #region UI
    public void show_invite_UI(string room_id_ ="-") {
        if (room_id_ == "-" ) {
            return;
        }
        invite_text.text = $"{room_id_}님이 마이플래닛으로 초대하였습니다!";

        invite_agree_button.onClick.RemoveAllListeners();
        invite_agree_button.onClick.AddListener(delegate { join(room_id_);});
        invite_agree_button.onClick.AddListener(delegate { hide_invite_UI(); });
        invite_UI.SetActive(true);
    }
    public void hide_invite_UI()
    {   
        invite_UI.SetActive(false);
    }

    public void hide_set_buttons()
    {
        for (int i = 0; i < set_button_list.Count; i++)
        {
            set_button_list[i].gameObject.SetActive(false);
        }
    }
    public void hide_lobby_buttons()
    {
        for (int i = 0; i < lobby_button_list.Count; i++)
        {
            lobby_button_list[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < planet_button_list.Count; i++)
        {
            planet_button_list[i].gameObject.SetActive(true);
        }
    }
    public void hide_planet_buttons(bool is_going_lobby = true)
    {
        if (is_going_lobby) {
            for (int i = 0; i < lobby_button_list.Count; i++)
            {
                lobby_button_list[i].gameObject.SetActive(true);
            }
        }
        
        for (int i = 0; i < planet_button_list.Count; i++)
        {
            planet_button_list[i].gameObject.SetActive(false);
        }
    }


    #endregion

    #region  buttons
   

    public void send_chat_button() {
        
        if (string.IsNullOrEmpty(chat_box_manager.chat_input_field.text)) {
            chat_box_manager.chat_input_field.Select();
            return;
        }
        
        string chat_msg = my_player.object_id + ":" + chat_box_manager.chat_input_field.text;
        Debug.Log(chat_msg);
        send_chat_request(chat_msg, chat_box_manager.is_global_chat);
        chat_box_manager.clear_input();
        chat_box_manager.chat_input_field.Select();
    }
    public void invite_btn1()
    {
        
        send_invite_request("11");
        
    }

    public void set_id_btn() {
        my_player.init("11");
        join_global();
    }
    public void set_id_btn2()
    {
        my_player.init("22");
        join_global();
    }
    public void set_id_btn3()
    {
        my_player.init("33");
        join_global();
    }
    public void join_global()
    {
        Client_Connect();
        now_room_id = "-";    
        if (send_join_request(now_room_id, my_player.object_id))
        {
            hide_set_buttons();
            hide_planet_buttons();
        }
    }

    public void exit_room_btn() {
        now_room_id = "-";    
        if (send_join_request(now_room_id, my_player.object_id))
        {
            exit_room();
            hide_planet_buttons(is_going_lobby:false);
            //hide_buttons();
        }
    }
    
  
    

    
    #endregion
}
