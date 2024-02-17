using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
using static UnityEngine.Rendering.DebugUI;
using System.ComponentModel;
public class Star_nest_UI : MonoBehaviour
{
    public GameObject UI_Container;

    public Image character_image;
    public TMP_Text pop_text;
    public TMP_Text title_text;
    public TMP_Text nickname_text;
    public TMP_Text planet_name_text;
    public TMP_Text intro_text;
    public TMP_InputField memo_input;

    public Transform memo_container;
    public GameObject memo_prefab;

    private UserData user_data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show_UI() {
        UI_Container.SetActive(true);
        load_info();
    }

    public void hide_UI() {
        UI_Container.SetActive(false);
        //user_data = null;
    }

    public void load_info() {
        string[] select = { "info", "memo_info" };
        user_data = get_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select);

        //TODO: 이미지 매니저 만들기 -> 유저 데이터에 선택 이미지 정보 추가 -> 이미지 매니저를 통해 가져오기
        //character_image = Image_Manager.instance.gt_image(user_data.select_image_id);
        //pop_text.text = user_data.popularity.ToString();

        //TODO: 유저 데이터에 칭호 선택 정보 저장
        title_text.text = user_data.info;
        nickname_text.text = TCP_Client_Manager.instance.now_room_id;
        planet_name_text.text = TCP_Client_Manager.instance.placement_system.housing_info.level.ToString();
        intro_text.text = user_data.info;
        load_memos_UI();
    }

    public void clear_memos_UI() {
        //memo_container
        int len = memo_container.childCount;
        for (int i =0; i < len; i++) {
            Destroy(memo_container.GetChild(i).gameObject);
        }
    }

    public void load_memos_UI() {
        if (user_data == null || user_data.memo_info == null)
        {
            Debug.Log("user data memo is null!");
            return;
        }
        clear_memos_UI();
        for (int i =0; i < user_data.memo_info.memo_list.Count; i++) {
            create_memo(user_data.memo_info.memo_list[i].UUID, user_data.memo_info.memo_list[i].content);           
        }
        
    }

    public void add_memo( ) {
        if (user_data == null || user_data.memo_info == null)
        {
            Debug.Log("user data memo is null!");
            return;
        }
        if (memo_input.text.Equals(string.Empty)) {
            return;
        }
        Memo memo = new Memo();
        memo.Change(TCP_Client_Manager.instance.my_player.object_id, memo_input.text);
        user_data.memo_info.Add_object(memo);
        memo_input.text = "";
        memo_input.Select();

        create_memo(memo.UUID, memo.content);

        string[] select = {  "memo_info" };
        update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id,  select,  user_data);
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id)
        {
            BackendGameData_JGD.userData.memo_info = user_data.memo_info;
        }
    }
    public void create_memo(string uuid_, string content_)
    {

        if (content_.Equals(string.Empty)) {
            return;
        }
        GameObject memo_go = Instantiate(memo_prefab);
        memo_go.transform.SetParent(memo_container);
        memo_go.transform.localScale = Vector3.one;
        TMP_Text[] text_arr = memo_go.GetComponentsInChildren<TMP_Text>();
        text_arr[0].text = uuid_;
        text_arr[1].text = content_;
    }

    public UserData get_userdata_by_nickname(string nickname, string[] select)
    {
        //select = { "Housing_Info" };
        var n_bro = Backend.Social.GetUserInfoByNickName(nickname);
        string gamer_indate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        BackendReturnObject bro = Backend.PlayerData.GetOtherData("USER_DATA", gamer_indate, select);
        if (bro.IsSuccess())
        {
            LitJson.JsonData gameDataJson = bro.FlattenRows();
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                UserData user_data = new UserData();
                for (int i =0; i < select.Length; i++) {
                    switch (select[i])
                    {
                        case "Friend_UUID_List":
                            //user_data.Friend_UUID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "inventory":
                            //user_data.inventory = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Achievements_List":
                            //user_data.Achievements_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "QuestInfo_List":
                            //user_data.QuestInfo_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Pet_Info":
                            //user_data.Pet_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Housing_Info":
                            user_data.Housing_Info = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "Char_Item_ID_List":
                           // user_data.Char_Item_ID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "StageInfo_List":
                           // user_data.StageInfo_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "House_Item_ID_List":
                            //user_data.House_Item_ID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "info":
                            user_data.info = gameDataJson[0]["info"].ToString();
                            break;
                        case "memo_info":
                            user_data.memo_info = new Memo_info(gameDataJson[0]["memo_info"]);
                            break;
                        case "Adjective_ID_List":
                            //user_data.Adjective_ID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "level":
                            user_data.level = int.Parse(gameDataJson[0]["level"].ToString());
                            break;
                        case "Noun_ID_List":
                           // user_data.Noun_ID_List = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        case "equipment":
                            //user_data.equipment = new HousingInfo_JGD(gameDataJson[0]["Housing_Info"]);
                            break;
                        default:
                            break;
                    }
                }
                return user_data;
            }
        }
        else
        {
            Debug.Log("Fail");
        }
        return null;
    }

    public void update_userdata_by_nickname(string nickname, string[] select, UserData user_data)
    {
        if (user_data ==null) {
            return;
        }
        string[] select_temp= { "info"};
        var n_bro = Backend.Social.GetUserInfoByNickName(nickname);
        string gamer_indate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        BackendReturnObject bro = Backend.PlayerData.GetOtherData("USER_DATA", gamer_indate, select_temp);

        if (bro.IsSuccess())
        {            
            string gameDataRowInDate = bro.GetInDate(); 
            Param param = new Param();

            for (int i = 0; i < select.Length; i++)
            {
                switch (select[i])
                {
                    case "Friend_UUID_List":
                        param.Add(select[i], user_data.Friend_UUID_List);
                        break;
                    case "inventory":
                        param.Add(select[i], user_data.inventory);
                        break;
                    case "Achievements_List":
                        param.Add(select[i], user_data.Achievements_List);
                        break;
                    case "QuestInfo_List":
                        param.Add(select[i], user_data.QuestInfo_List);
                        break;
                    case "Pet_Info":
                        param.Add(select[i], user_data.Pet_Info);
                        break;
                    case "Housing_Info":
                        param.Add(select[i], user_data.Housing_Info);
                        break;
                    case "Char_Item_ID_List":
                        param.Add(select[i], user_data.Char_Item_ID_List);
                        break;
                    case "StageInfo_List":
                        param.Add(select[i], user_data.StageInfo_List);
                        break;
                    case "House_Item_ID_List":
                        param.Add(select[i], user_data.House_Item_ID_List);
                        break;
                    case "info":
                        param.Add(select[i], user_data.info);
                        break;
                    case "memo_info":
                        param.Add(select[i], user_data.memo_info);
                        break;
                    case "Adjective_ID_List":
                        param.Add(select[i], user_data.Adjective_ID_List);
                        break;
                    case "level":
                        param.Add(select[i], user_data.level);
                        break;
                    case "Noun_ID_List":
                        param.Add(select[i], user_data.Noun_ID_List);
                        break;
                    case "equipment":
                        param.Add(select[i], user_data.equipment);
                        break;
                    default:
                        break;
                }
            }
            var bro_ = Backend.PlayerData.UpdateOtherData("USER_DATA", gameDataRowInDate, gamer_indate, param);
            if (bro_.IsSuccess()) {
                return;
            }
            else{
                Debug.Log("Fail");
            }                  
        }
        else
        {
            Debug.Log("Fail");
        }
        return;
    }
}
