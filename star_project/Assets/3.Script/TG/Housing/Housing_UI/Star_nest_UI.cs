using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
using static UnityEngine.Rendering.DebugUI;
using System.ComponentModel;

public enum adjective { 
    none=-1, 
    lovely=0
}
public enum noun { 
    none=-1,
    jjang = 0
}

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

    public GameObject pop_up_button;
    public GameObject pop_up_result_UI;

    public GameObject level_up_button; 
    public GameObject level_up_UI; 


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
    /* param.Add("profile_background", userData.profile_background);
                 param.Add("profile_picture", userData.profile_picture);
                 param.Add("popularity", userData.popularity);
                 param.Add("title_adjective", userData.title_adjective);
                 param.Add("title_noun", userData.title_noun);
                 param.Add("planet_name", userData.planet_name);
                 param.Add("ark", userData.ark);
                 param.Add("gold", userData.gold);
                 param.Add("ruby", userData.ruby);*/
    public void load_info() {
        string[] select = { "profile_background",
            "profile_picture",
            "popularity",
            "title_adjective",
            "title_noun",
            "planet_name", 
            "info", 
            "memo_info" };
        user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select);

        //TODO: 이미지 매니저 만들기 -> 유저 데이터에 선택 이미지 정보 추가 -> 이미지 매니저를 통해 가져오기
        //character_image = Image_Manager.instance.gt_image(user_data.select_image_id);
        pop_text.text = user_data.popularity.ToString();

        //TODO: 유저 데이터에 칭호 선택 정보 저장
        title_text.text = user_data.title_adjective.ToString() +" "+ user_data.title_noun.ToString();
        nickname_text.text = TCP_Client_Manager.instance.now_room_id;
        planet_name_text.text = user_data.planet_name;
        // TCP_Client_Manager.instance.placement_system.housing_info.level.ToString();
        intro_text.text = user_data.info;
        load_memos_UI();

        if (TCP_Client_Manager.instance.now_room_id != TCP_Client_Manager.instance.my_player.object_id)
        {
            pop_up_button.SetActive(true);
            level_up_button.SetActive(false);
        }
        else {
            pop_up_button.SetActive(false);
            level_up_button.SetActive(true);
        }
    }

    #region pop up
    public void pop_up()
    {
        user_data.popularity += 1;
        string[] select = {"popularity"};
        BackendGameData_JGD.Instance.update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select, user_data);
        show_pop_up_result();
    }

    public void show_pop_up_result() {
        StartCoroutine(show_pop_up_result_co());
    }

    public IEnumerator show_pop_up_result_co() {
        pop_up_result_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        pop_up_result_UI.SetActive(false);

    }

    #endregion

    #region level up
    public void show_level_UI()
    {
        level_up_UI.SetActive(true);
        // TCP_Client_Manager.instance.placement_system.housing_info.level.ToString();
    }
    public void hide_level_UI()
    {
        level_up_UI.SetActive(false);
    }
    

    public void level_up() {
        if (can_level_up()) {
            TCP_Client_Manager.instance.placement_system.level_up();
        }
    }

    public bool can_level_up() {
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region profile edit


    #endregion

    #region memo
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
        BackendGameData_JGD.Instance.update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id,  select,  user_data);
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

    #endregion
}
