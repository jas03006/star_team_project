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
        user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select);

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

    
}
