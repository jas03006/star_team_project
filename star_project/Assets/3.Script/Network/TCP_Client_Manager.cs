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
using Unity.VisualScripting;

using UnityEngine.SceneManagement;
using UnityEditor;
using BackEnd;

// ��Ʈ��ũ ��û ���� enum
public enum command_flag
{
    join = 0, // �Ͽ콺 ����
    exit = 1, // �Ͽ콺 ����
    move = 2, // �̵�
    build = 3, // �ǹ� ��ġ
    remove = 4, // �ǹ� ����
    update = 5, // �ǹ� ���� ������Ʈ
    chat = 6, //ä�� ����
    interact = 7, //ä�� ����
    invite = 8, //�ʴ�
    emo = 9, //�̸�Ƽ�� ���
    kick = 10 //����
}

//������ TCP ������ �����ϴ� Ŭ����
//��Ʈ��ũ ��û�� ����, ó��
//ä��,�̸�Ƽ�� ���۵� ���
public class TCP_Client_Manager : MonoBehaviour
{
    public static TCP_Client_Manager instance = null;

    private string IPAdress = "13.125.169.138";
    private string Port = "7777";

    private Queue<string> log = new Queue<string>();

    StreamReader reader;//������ �б�
    StreamWriter writer;//������ ����

    private TMP_Text myplanet_text = null;
    private string now_room_id_ ="-"; //���� �����÷��� ���̵�
    public string now_room_id { //�����÷��� ���̵� ���� �� ���� ǥ��UI ������Ʈ
        private set { now_room_id_ = value; if (myplanet_text != null && myplanet_text.text != null) { myplanet_text.text = now_room_id_; } } 
        get { return now_room_id_; } 
    }
    public string target_room_id { private set; get; } = "-"; //�̵��ϰ� ���� ��ǥ �����÷��� ���̵�

    //�� �̵� �� ���� �ʿ��� UI�� ����Ʈ
    [SerializeField] private List<Button> set_button_list;
    [SerializeField] private List<Button> lobby_button_list;
    [SerializeField] private List<GameObject> planet_button_list;
    [SerializeField] private List<Button> stage_button_list;
    [SerializeField] private string planet_scene_name;
    [SerializeField] private string lobby_scene_name;
    [SerializeField] private string stage_scene_name;

    //���� �����÷��� ���� ������ �� �ִ� ������Ʈ�� ���� ��ųʸ�
    public Dictionary<string, Net_Move_Object_TG> net_mov_obj_dict; //object_id, object
    
    private Queue<string> msg_queue; //���� ��Ʈ��ũ ��û�� ��� ť

    [SerializeField] public PlayerMovement my_player; // �����÷��� �� ������ ĳ����
    [SerializeField] private GameObject guest_prefab; //�����÷��� �� Ÿ���� ĳ���͸� �����ϱ� ���� ������
    
    TcpClient client;
    private int respawn_flag = 7777; //���� ������ ��ġ�� �����ϴ� ��

    public Housing_UI_Manager housing_ui_manager;
    [SerializeField] private ChatBoxManager chat_box_manager;
    [SerializeField] private EmoziBoxManager emozi_box_manager;

    //�ʴ� ��û �޾��� �� ��µǴ� UI
    [SerializeField] private GameObject invite_UI;
    [SerializeField] private TMP_Text invite_text;
    [SerializeField] private Button invite_agree_button;

    public PlacementSystem placement_system;
    public Camera_My_Planet camera_my_planet;

    private Thread current_thread; //���� TCP ������ ���� ����ϰ� �ִ� ������ (�α׾ƿ� �ÿ� ��ü)
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

    //�����÷��� �� �ε� �� ó��
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == planet_scene_name)
        {
            my_player.find_grid();
            placement_system = FindObjectOfType<PlacementSystem>();
            housing_ui_manager = FindObjectOfType<Housing_UI_Manager>();
            camera_my_planet = FindObjectOfType<Camera_My_Planet>();
            chat_box_manager.hide_chat();
            emozi_box_manager.hide_box();

            myplanet_text = GameObject.FindGameObjectWithTag("myplanet_UI")?.GetComponent<TMP_Text>();
        }
        else {
            if (my_player != null) {
                my_player.transform.position = new Vector3(-1000f,-1000f,0);
            }
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

    //�����÷��� ���� �� �� �ε�
    public void load_house()
    {
        hide_lobby_buttons();
        my_player.stop_DOTween();
        placement_system.init_house(now_room_id);
        housing_ui_manager.init_housing_UI();
        //housing ����, ��ġ, ������Ʈ ��� 0213
    }

    #region client connection
    public void Client_Connect()
    {
        if (client != null) {
            return;
        }
        log.Enqueue("client_connect");
        current_thread = new Thread(client_connect);
        current_thread.IsBackground = true;
        current_thread.Start();
        while (client ==null || !client.Connected || writer == null) {
            Debug.Log("wait connection...");
        }
        Debug.Log("Connected!");
        return;
    }
    private void client_connect()
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



            while (client != null && client.Connected)
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

    private void OnDestroy()
    {
        Debug.Log("Destory TCP Client");
        client_disconnet();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void client_disconnet()
    {
        try { 
            if (client != null) { 
                client.GetStream().Close(); 
                client.Close(); reader = null; 
                writer = null; client = null; 
                current_thread.Abort(); 
            } 
        } catch(Exception e) { 
            log.Enqueue(e.Message); 
        }
    }
    #endregion

    #region parsing
    //���� ��Ʈ��ũ ��û�� ó��
    private void parse_msg(string msg) {
        if (msg == BitConverter.GetBytes((int)0).ToString()) { // connection check byte 
            return;
        }
        Debug.Log(msg);
        string[] cmd_arr = msg.Split(" ");
        // try
        //{
            string uuid_;
            string host_id;
            switch ((command_flag)int.Parse(cmd_arr[0]))
            {
                case command_flag.join: // join rood_id host_id position_data
                    host_id = cmd_arr[1];
                    uuid_ = cmd_arr[2];
                    if (my_player.object_id != uuid_)
                    {
                        create_guest(uuid_, get_respawn_point(uuid_)); //ù ������ ��ǥ�� �ֱ�
                    }
                    else {
                        if (cmd_arr.Length > 3) {
                            chat_box_manager.clear();
                            remove_all_guest(except_self: true);

                            host_id = cmd_arr[1];
                            now_room_id = host_id;
                            /*if (host_id == -1) { //�۷ι� ä�� �濡 �����ϴ� ���̶�� //�۷ι��� ������ ��� RPC�� ���ο��� ���� �ʴ� ������ ��
                                my_player.transform.position = Vector3.zero;
                                break;
                            }*/

                            net_mov_obj_dict[cmd_arr[2]] = my_player;                            
                            creat_all_guest(cmd_arr[3]);
                        }
                    }   
                    break;
                case command_flag.exit: // exit room_id host_id //���� ������ (�� ������� �ٸ������� ��)
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
                    //{ // �۷ι� ä���� ��� 
                        chat_box_manager.chat(chat_msg, host_id == "-");
                    /*}
                    else {
                    chat_box_manager.chat(chat_msg);
                    }*/                    
                    break;
                case command_flag.interact:
                    //TODO: ������Ʈ ���;׼� (������Ʈ�� ��� ����ִ� class�� �ϳ� ������ ��, object id�� �´� ������Ʈ�� ��ȣ�ۿ� ����)
                    uuid_ = cmd_arr[2];
                    if (uuid_ != my_player.object_id) {
                        string[] arr_ = cmd_arr[3].Split(":");
                        try
                        {
                            placement_system.objectPlacer.placedGameObject[placement_system.furnitureData.placedObjects[new Vector3Int(int.Parse(arr_[0]), 0, int.Parse(arr_[1]))].PlacedObjectIndex].GetComponent<SpecialObj>()?.interact(uuid_, 0, 1);
                        }
                        catch { 
                        
                        }
                     }
                    break;
                case command_flag.invite:
                    //TODO: �ʴ� �˸� ����
                    if (cmd_arr[2] == my_player.object_id) {
                        show_invite_UI(cmd_arr[1]);
                    }                
                    break;
                case command_flag.emo:
                    host_id = cmd_arr[1];
                    uuid_ = cmd_arr[2];
                    if (host_id == now_room_id && uuid_ != my_player.object_id)
                    {
                        (net_mov_obj_dict[uuid_] as PlayerMovement).show_emozi(int.Parse(cmd_arr[3]));
                    }
                    break;
                case command_flag.kick:
                    host_id = cmd_arr[1];
                    uuid_ = cmd_arr[2];
                    if (uuid_ == my_player.object_id ) { //���� ���� ���� ��
                        if (my_player.object_id != now_room_id) {
                            go_myplanet();
                        }                        
                    }/*
                    else{
                        if (host_id  == now_room_id) //���� �ִ� �÷����� ������ ���� ���� ��
                        {
                            create_guest(uuid_, get_respawn_point(uuid_)); //ù ������ ��ǥ�� �ֱ�
                        }                    
                    }*/
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
    public void killfriend_reaction(string nickname) {
        if (now_room_id.Equals(nickname))
        {
            go_myplanet();
        }
        else
        {
            kick(nickname);
        }
    }
    
    public void kick(string uuid_) {
          send_kick_request(my_player.object_id, uuid_);
    }
    
    public void invite(string uuid_)
    {

        send_invite_request(uuid_);

    }
    // room_id�� �����÷������� �湮
    public void join(string room_id_)
    {
        if (now_room_id == room_id_) {
            return;
        }
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
    //�����÷��� ������ �̵� �� Ÿ�ٸ����÷������� �湮 ��û
    public void join_OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == planet_scene_name)
        {
            send_join_and_load_target();
        }
    }
    //��ǥ �����÷������� �湮 ��û
    public void send_join_and_load_target()
    {
        send_join_and_load(target_room_id);
    }
    //�湮 ��û
    public void send_join_and_load(string room_id_) {
        if (send_join_request(room_id_, my_player.object_id)) //�湮 ���� �� ó��
        {
            move_planet(room_id_);
        }
    }

    public void move_planet(string room_id_) {
        now_room_id = room_id_;
        hide_lobby_buttons();
        load_house();
        my_player.show_UI();

        if (now_room_id != "-" && now_room_id != my_player.object_id)
        {
            QuestManager.instance.Check_challenge(Clear_type.visit_friendplanet);
        }
    }
    //������ �����÷������� �̵�
    public void go_myplanet()
    {        
        target_room_id = my_player.object_id;
        SceneManager.sceneLoaded -= join_OnSceneLoaded;
        SceneManager.sceneLoaded += join_OnSceneLoaded;
        SceneManager.LoadScene(planet_scene_name);
       // send_join_and_load(my_player.object_id);
    }
    //���� ����
    public void exit_game()
    {
        Application.Quit();
        //client = null;
    }
    //�����÷��ֿ��� ������
    public void exit_room() {
        my_player.hide_UI();
        now_room_id = "-";
        chat_box_manager.clear();        
        remove_all_guest(except_self: true);
        my_player.stop_DOTween();
        my_player.transform.position = -Vector3.right * 1000f;        
        //SceneManager.LoadScene(lobby_scene_name);
    }
    //�����÷��� �湮 �� ������ ��ġ ����
    public Vector3 get_respawn_point(string uuid_) {
        return placement_system.get_spawn_point(uuid_ == now_room_id);
    }
    public Vector3 get_respawn_point(bool is_my_planet)
    {
        return placement_system.get_spawn_point(is_my_planet);
    }
    #endregion

    #region guest management
    //Ÿ���� ĳ���� ����
    private void create_guest(string uuid_, Vector3 position) {
        if (position.x == respawn_flag) {
            position = get_respawn_point(uuid_);
        }
        GameObject new_guest = GameObject.Instantiate(guest_prefab, position, Quaternion.identity);
        Net_Move_Object_TG nmo = new_guest.GetComponent<Net_Move_Object_TG>();
        nmo.init(uuid_, true);
        (nmo as PlayerMovement).look_user();
        net_mov_obj_dict[uuid_.ToString()] = nmo;
        //TODO: uuid�� �̿��� DB���� ���� ������ �޾ƿͼ� ��Ų �� ������ ������ ������ �߰��ؾ���
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
    //Ÿ���� ĳ���� ����
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
            if (net_mov_obj_dict[key] != null) { 
                Destroy(net_mov_obj_dict[key].gameObject);  
            }
        }
        net_mov_obj_dict.Clear();
    }
   
    #endregion

    #region request
    //������ ������ ����
    private bool sending_Message(string me)
    {
        if (writer != null)
        {
            writer.WriteLine(me);
            return true;
        }
        else
        {
            Debug.Log($"Writer Null: {me}");
            return false;
        }
    }
    //�̵� ��û
    public bool send_move_request(string object_id, Vector3 start_pos, Vector3 dest_pos)
    {
        return sending_Message($"{(int)command_flag.move} {now_room_id} {object_id} {start_pos.x} {start_pos.z} {dest_pos.x} {dest_pos.z}");
    }
    //�湮 ��û
    public bool send_join_request(string room_id, string object_id)
    {
        return sending_Message($"{(int)command_flag.join} {room_id} {object_id}");
    }
    //������ ��û
    public bool send_exit_request(string room_id, string object_id)
    {
        return sending_Message($"{(int)command_flag.exit} {room_id} {object_id}");
    }
    //�����÷��� ������Ʈ ��û (�ٽ� �ε�)
    public bool send_update_request()
    {
        return sending_Message($"{(int)command_flag.update} {now_room_id} {my_player.object_id}");
    }
    //ä�� ���� ��û
    public bool send_chat_request(string chat_msg, bool is_global=false)
    {
        if (is_global) {
            return sending_Message($"{(int)command_flag.chat} {"-"} {my_player.object_id} {chat_msg}");
        }
        return sending_Message($"{(int)command_flag.chat} {now_room_id} {my_player.object_id} {chat_msg}");
    }
    //������Ʈ ��ȣ�ۿ� ��û
    public bool send_interact_request(string object_id, int interaction_id, int param)
    {
        return sending_Message($"{(int)command_flag.interact} {now_room_id} {my_player.object_id} {object_id} {interaction_id} {param}");
    }
    //�ʴ� ��û
    public bool send_invite_request(string object_id)
    {
        if (now_room_id  != "-" && now_room_id == my_player.object_id) {
            return sending_Message($"{(int)command_flag.invite} {now_room_id} {object_id}");
        }
        return false;
    }
    //�̸�Ƽ�� ���� ��û
    public bool send_emo_request(int emozi_id)
    {
        if (now_room_id == "-") {
            return false;
        }
        return sending_Message($"{(int)command_flag.emo} {now_room_id} {my_player.object_id} {emozi_id}");
    }
    //���� ��û
    public bool send_kick_request(string room_id, string object_id)
    {
        return sending_Message($"{(int)command_flag.kick} {room_id} {object_id}");
    }
    #endregion

    #region UI
    public void show_invite_UI(string room_id_ ="-") {
        if (room_id_ == "-" ) {
            return;
        }
        invite_text.text = $"<color=white><{room_id_}></color>���� \n�ʴ����� �����߽��ϴ�\n�༺���� �̵��ұ��?";

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
