using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
using static UnityEngine.Rendering.DebugUI;
using System.ComponentModel;
using UnityEngine.Rendering;

public enum adjective { 
    none=-1, 
    lovely=0
}
public enum noun { 
    none=-1,
    jjang = 0
}

/*public class Profile_Info_TG {
    public int profile_picture = 0;
    public string planet_name = string.Empty;
    public adjective title_adjective = adjective.none;
    public noun title_noun = noun.none;
    public string info = string.Empty;
}*/

public class Star_nest_UI : MonoBehaviour
{
    //private Profile_Info_TG profile_info = new Profile_Info_TG();
    string[] load_select = { "profile_background",
            "profile_picture",
            "popularity",
            "title_adjective",
            "title_noun",
            "planet_name",
            "info",
            "memo_info",
            "Housing_Info"};
    string[] save_select = { "profile_background",
            "profile_picture",
            "title_adjective",
            "title_noun",
            "planet_name",
            "info",
            "memo_info" };
    private bool is_editing = false;

    public GameObject UI_Container;

    public Image character_image;
    public TMP_Text pop_text;
    public TMP_Text title_text;
    public TMP_Text nickname_text;
    public TMP_Text planet_name_text;
    public TMP_Text intro_text;
    public TMP_Text level_profile_text;
    public Image level_profile_image;

    public TMP_InputField memo_input;

    
    public Transform memo_container;
    public GameObject memo_prefab;

    public GameObject pop_up_button;
    public GameObject pop_up_result_UI;

    public GameObject[] edit_btn_arr;

    //public GameObject add_friend_button;

    [Header("LevelUP")]
   // public GameObject level_up_button; 
    public GameObject level_up_UI;
    public TMP_Text before_nest_text;
    public TMP_Text after_nest_text;
    public Image before_nest_img;
    public Image arrow_img;
    public Image after_nest_img;

    public TMP_Text level_up_info_text;
    public TMP_Text level_up_gold;
    public TMP_Text level_up_ark;

    string[] nest_name_arr = { "LV1 별둥지", "LV2 별둥지", "LV3 별둥지", "LV4 별둥지" };
    public Sprite[] nest_sprite_arr ;
    int[] required_ark_arr = { 10, 20, 30 };
    int[] required_gold_arr = { 10, 20, 30 };

    private UserData user_data;

    public Star_nest star_nest;
    public void show_UI( bool is_profile = false) {

        if (!is_profile && (TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id))
        {
            show_level_UI();
        }
        else {
            UI_Container.SetActive(true);
            load_info(is_profile);
        }        
    }

    public void hide_UI() {
        UI_Container.SetActive(false);
        //user_data = null;
    }

    public void load_info(bool is_profile_click = false, string nick_name_ = null) {
        

        if (is_profile_click)
        { //내 프로필 클릭인경우
            user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname(TCP_Client_Manager.instance.my_player.object_id, load_select);
            nickname_text.text = TCP_Client_Manager.instance.my_player.object_id;
            pop_up_button.SetActive(false);
            show_edit_btn();
        }
        else {
            user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname((nick_name_ == null?TCP_Client_Manager.instance.now_room_id: nick_name_), load_select);
            nickname_text.text = TCP_Client_Manager.instance.now_room_id;
            pop_up_button.SetActive(true);
            hide_edit_btn();
        }

        update_profile_UI();
        level_profile_text.text = (user_data.housing_Info.level+1).ToString();
        level_profile_image.sprite = nest_sprite_arr[user_data.housing_Info.level];

        load_memos_UI();


    }
    public void update_profile_UI() {
        pop_text.text = user_data.popularity.ToString();
        title_text.text = user_data.title_adjective.ToString() + " " + user_data.title_noun.ToString();
        planet_name_text.text = user_data.planet_name;
        intro_text.text = user_data.info;
        //TODO: 선택 프로필 사진 업데이트 기능
        //TODO: 선택 배경 사진 업데이트 기능
        //character_image = Image_Manager.instance.gt_image(user_data.select_image_id);
    }
    public void cancel_edit_profile() {
        if (is_editing)
        {
            user_data.profile_background = BackendGameData_JGD.userData.profile_background;
            user_data.profile_picture = BackendGameData_JGD.userData.profile_picture;
            user_data.planet_name = BackendGameData_JGD.userData.planet_name;
            user_data.title_adjective = BackendGameData_JGD.userData.title_adjective;
            user_data.title_noun = BackendGameData_JGD.userData.title_noun;
            user_data.info = BackendGameData_JGD.userData.info;
            is_editing = true;
        }
    }
    public void save_edit_profile() {
        if (is_editing) {
            BackendGameData_JGD.userData.profile_background = user_data.profile_background;
            BackendGameData_JGD.userData.profile_picture = user_data.profile_picture;
            BackendGameData_JGD.userData.planet_name = user_data.planet_name;
            BackendGameData_JGD.userData.title_adjective = user_data.title_adjective;
            BackendGameData_JGD.userData.title_noun = user_data.title_noun;
            BackendGameData_JGD.userData.info = user_data.info;

            BackendGameData_JGD.Instance.GameDataUpdate(save_select);
            is_editing = true;
        }
    }
    
    public void hide_edit_btn() {
        for (int i =0; i < edit_btn_arr.Length; i++) {
            edit_btn_arr[i].SetActive(false);
        }
    }
    public void show_edit_btn()
    {
        for (int i = 0; i < edit_btn_arr.Length; i++)
        {
            edit_btn_arr[i].SetActive(true);
        }
    }

    #region pop up
    public void pop_up()
    {
        user_data.popularity += 1;
        string[] select = {"popularity"};
        BackendGameData_JGD.Instance.update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select, user_data);
        pop_text.text = user_data.popularity.ToString();
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

    public void add_friend_btn() {
        if (TCP_Client_Manager.instance.now_room_id != TCP_Client_Manager.instance.my_player.object_id)
        {
            BackendFriend_JDG.Instance.SendFriendRequest(TCP_Client_Manager.instance.now_room_id);
        }
    }

    #region level up
    public void show_level_UI()
    {
        level_up_UI.SetActive(true);
        update_level_UI();
    // TCP_Client_Manager.instance.placement_system.housing_info.level.ToString();
    }

    public void update_level_UI() {
        int level_ = BackendGameData_JGD.userData.housing_Info.level;

        before_nest_text.text = nest_name_arr[level_];
        before_nest_img.sprite = nest_sprite_arr[level_];

        if (level_ == nest_name_arr.Length - 1)
        {
            after_nest_text.gameObject.SetActive(false);
            after_nest_img.transform.parent.gameObject.SetActive(false);
            arrow_img.gameObject.SetActive(false);

            level_up_info_text.text = $"최고 레벨입니다!";
            level_up_gold.text = "";
            level_up_ark.text = "";

        }
        else {
            after_nest_text.gameObject.SetActive(true);
            after_nest_img.transform.parent.gameObject.SetActive(true);
            arrow_img.gameObject.SetActive(true);

            after_nest_text.text = nest_name_arr[level_ + 1];
            after_nest_img.sprite = nest_sprite_arr[level_ + 1];

            int[] level_width_arr = TCP_Client_Manager.instance.placement_system.furnitureData.level_boudary;
            level_up_info_text.text = $"플래닛 크기 확장\n{level_width_arr[level_]}X{level_width_arr[level_]} -> {level_width_arr[level_ + 1]}X{level_width_arr[level_ + 1]}";
            level_up_gold.text = required_gold_arr[level_].ToString();
            level_up_ark.text = required_ark_arr[level_].ToString();
        }     

        
    }
    public void hide_level_UI()
    {
        level_up_UI.SetActive(false);
    }
    

    public void level_up() {
        if (can_level_up()) {
            int level_ = BackendGameData_JGD.userData.housing_Info.level;
            TCP_Client_Manager.instance.placement_system.level_up();
            MoneyManager.instance.Spend_Money(required_gold_arr[level_], required_ark_arr[level_]);
            update_level_UI();
            if (star_nest != null) {
                star_nest.apply_level();
            }
        }
    }

    public bool can_level_up() {
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id)
        {
            int level_ = BackendGameData_JGD.userData.housing_Info.level;
            if (level_ < nest_name_arr.Length - 1 && required_gold_arr[level_]<= MoneyManager.instance.gold && required_ark_arr[level_] <= MoneyManager.instance.ark) {

                return true;

            }

            return false;

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
