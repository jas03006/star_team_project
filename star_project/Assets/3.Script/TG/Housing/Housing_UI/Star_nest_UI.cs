using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
//using static UnityEngine.Rendering.DebugUI;
using System.ComponentModel;

public enum adjective { 
    none=-1, 
    lovely=0,
    Powerful = 1,
    Hidden_Power_Of = 2,
    Sweet = 3
}
public enum noun { 
    none=-1,
    jjang = 0,
    Tiger = 1,
    Fire_Punch = 2
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
            "Housing_Info",
            "catchingstar_info",
            "emozi_List",
            "background_List",
            "Achievements_List",
            "challenge_Userdatas",
            "Noun_ID_List",
            "Adjective_ID_List",
            "popularity_history"};
    string[] save_select = { "profile_background",
            "profile_picture",
            "title_adjective",
            "title_noun",
            "planet_name",
            "info",
            "memo_info",
            "Achievements_List"};
    private bool is_editing = false;

    public GameObject UI_Container;

    public GameObject cancel_check_UI_ob;

    public Image character_image;
    public Image background_image;
    public TMP_Text pop_text;
    [SerializeField] private Color pop_color_yes;
    [SerializeField] private Color pop_color_no;
    public TMP_Text title_text;
    public TMP_Text nickname_text;
    public TMP_Text planet_name_text;
    public TMP_Text intro_text;
    public TMP_Text level_profile_text;
    public Image level_profile_image;
    public TMP_Text chapter_stage_text;
    public TMP_Text[] achiv_select_text_arr;

    [Header("Memo UI")]
    public TMP_InputField memo_input;

    public Transform memo_container;
    public GameObject memo_prefab;

    [Header("Pop Up")]
    public GameObject pop_up_button;
    public GameObject pop_up_result_UI;

    [Header("Dependent UI")]
    public GameObject[] edit_btn_arr;
    public Button save_btn;
    public GameObject add_friend_btn_ob;
    public GameObject request_complete_btn_ob;
    public GameObject delete_friend_btn_ob;
    public GameObject memo_input_ob;
    public GameObject friend_only_ob;
    //public GameObject add_friend_button;

    [Header("Edit Picture")]
    public GameObject edit_picture_UI;
    public Button emozi_btn;
    public Button bg_btn;
    [SerializeField] private GameObject emozi_list_view;
    [SerializeField] private Transform emozi_list_container;
    [SerializeField] private GameObject emozi_element_prefab;
    [SerializeField] private GameObject bg_list_view;
    [SerializeField] private Transform bg_list_container;
    [SerializeField] private GameObject bg_element_prefab;

    [Header("Edit Title")]
    public GameObject edit_title_UI;
    public TMP_Dropdown edit_title_adjective;
    public TMP_Dropdown edit_title_noun;

    [Header("Edit Intro")]
    public GameObject edit_info_UI;
    public TMP_InputField edit_info_input;

    [Header("Edit Planet Name")]
    public GameObject edit_planet_name_UI;
    public TMP_InputField edit_planet_name_input;

    [Header("Edit Achivements")]
    public Achiv_Select_Manager achiv_select_manager;
    public GameObject edit_achiv_UI;

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
    public Sprite[] nest_sprite_arr;
    int[] required_ark_arr = { 10, 20, 30 };
    int[] required_gold_arr = { 10, 20, 30 };

    private UserData user_data;

    public Star_nest star_nest;
    private string now_nickname;

    private void Start()
    {
        
    }

    #region init
    public void init_title_edit_UI() {
        edit_title_adjective.ClearOptions();
        List<TMP_Dropdown.OptionData> option_list = new List<TMP_Dropdown.OptionData>();
        //foreach (var item in Enum.GetValues(typeof(adjective)))
        foreach (var item in user_data.Adjective_ID_List)
        {
            option_list.Add(new TMP_Dropdown.OptionData(item.ToString()));
        }
        edit_title_adjective.AddOptions(option_list);

        edit_title_noun.ClearOptions();
        option_list = new List<TMP_Dropdown.OptionData>();
       //foreach (var item in Enum.GetValues(typeof(noun)))
        foreach (var item in user_data.Noun_ID_List)
        {
            option_list.Add(new TMP_Dropdown.OptionData(item.ToString()));
        }
        edit_title_noun.AddOptions(option_list);
    }

    public void show_my_profile_UI() {
        show_UI(true);
    }

    public void show_UI(bool is_profile = false, string nick_name_ = null) {

        if (!is_profile && (TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id))
        {
            show_level_UI();
        }
        else {
            UI_Container.SetActive(true);
            load_info(is_profile, nick_name_);
        }
    }

    public void hide_UI() {
        UI_Container.SetActive(false);
        //user_data = null;
    }

    public void hide_UI_ob(GameObject go) {
        go.SetActive(false);
    }
    public void show_UI_ob(GameObject go)
    {
        go.SetActive(true);
    }

    public void click_X_btn() {
        if (is_editing)
        {
            show_UI_ob(cancel_check_UI_ob);
        }
        else {
            hide_UI();
        }
    }
    public void init_emozi_list()
    {
        for (int i =0; i < emozi_list_container.childCount; i++ ) {
            Destroy(emozi_list_container.GetChild(i).gameObject);
        }
        //TODO: read emozi inventory data and creat emozi element and add it into container
        foreach (int i in user_data.emozi_List) {
            GameObject go = Instantiate(emozi_element_prefab, emozi_list_container);
            go.GetComponent<Image>().sprite = SpriteManager.instance.Num2emozi(i);
            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => { apply_edit_picture(emozi: i); AudioManager.instance.SFX_Click(); });            
        }
    }
    public void init_bg_list()
    {
        for (int i = 0; i < bg_list_container.childCount; i++)
        {
            Destroy(bg_list_container.GetChild(i).gameObject);
        }
        //TODO: read bg inventory data and creat bg element and add it into container
        foreach (int i in user_data.background_List)
        {
            GameObject go = Instantiate(bg_element_prefab, bg_list_container);
            go.GetComponent<Image>().sprite = SpriteManager.instance.Num2BG(i);
            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => { apply_edit_picture(bg: i); AudioManager.instance.SFX_Click(); });
        }
    }
    public void load_info(bool is_profile_click = false, string nick_name_ = null) {


        if (is_profile_click && (nick_name_ == null || nick_name_ == string.Empty))
        { //내 프로필 클릭인경우
            now_nickname = TCP_Client_Manager.instance.my_player.object_id;
            user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname(TCP_Client_Manager.instance.my_player.object_id, load_select);
            nickname_text.text = TCP_Client_Manager.instance.my_player.object_id;           


            //pop_up_button.SetActive(false);
            pop_up_button.GetComponentInChildren<Button>().interactable = false;

            add_friend_btn_ob.SetActive(false);
            request_complete_btn_ob.SetActive(false);
            delete_friend_btn_ob.SetActive(false);
            memo_input_ob.SetActive(false);
            friend_only_ob.SetActive(true);


            show_edit_btn();

            init_title_edit_UI();
            init_emozi_list();
            init_bg_list();
            achiv_select_manager.init(user_data.Achievements_List, user_data.challenge_Userdatas);
        }
        else {
            now_nickname = (nick_name_ == null ? TCP_Client_Manager.instance.now_room_id : nick_name_);
            user_data = BackendGameData_JGD.Instance.get_userdata_by_nickname(now_nickname, load_select);
            nickname_text.text = now_nickname;


            hide_edit_btn();

            if (FriendList_JGD.is_friend(now_nickname))
            {
                add_friend_btn_ob.SetActive(false);
                request_complete_btn_ob.SetActive(false);
                delete_friend_btn_ob.SetActive(true);

                memo_input_ob.SetActive(true);
                friend_only_ob.SetActive(false);
            }
            else {
                BackendFriend_JDG.GetSentRequestFriend();
                if (BackendFriend_JDG.is_requested(now_nickname))
                {
                    add_friend_btn_ob.SetActive(false);
                    request_complete_btn_ob.SetActive(true);

                }
                else {
                    add_friend_btn_ob.SetActive(true);
                    request_complete_btn_ob.SetActive(false);
                }
                delete_friend_btn_ob.SetActive(false);
                memo_input_ob.SetActive(false);
                friend_only_ob.SetActive(true);
            }
        }

        update_profile_UI();
        level_profile_text.text = (user_data.housing_Info.level + 1).ToString();
        level_profile_image.sprite = nest_sprite_arr[user_data.housing_Info.level];

        load_memos_UI();
        
    }
    #endregion

    #region edit
    
    public void click_emozi_list_btn() {
        emozi_btn.interactable = false;
        emozi_list_view.SetActive(true);
        bg_btn.interactable = true;
        bg_list_view.SetActive(false);
    }
    public void click_bg_list_btn()
    {
        emozi_btn.interactable = true;
        emozi_list_view.SetActive(false);
        bg_btn.interactable = false;
        bg_list_view.SetActive(true);
    }
    public void show_edit_picture_UI()
    {
        //edit.text = intro_text.text;
        edit_picture_UI.SetActive(true);
        click_emozi_list_btn();
    }
    public void apply_edit_picture(int emozi = -1, int bg = -1)
    {
        /* if (edit_info_input.text.Equals(string.Empty))
         {
             return;
         }*/
        if (emozi >= 0) {
            user_data.profile_picture = emozi;
            is_editing = true;
        }
        if (bg >= 0)
        {
            user_data.profile_background = bg;
            is_editing = true;
        }
        
        update_profile_UI();
    }
    public void show_edit_title_UI()
    {
        edit_title_adjective.value = edit_title_adjective.options.FindIndex(option => option.text == user_data.title_adjective.ToString());
        edit_title_noun.value = edit_title_noun.options.FindIndex(option => option.text == user_data.title_noun.ToString());
        //edit_title_adjective.captionText.text = user_data.title_adjective.ToString();
        //edit_title_noun.captionText.text = user_data.title_noun.ToString();
        edit_title_UI.SetActive(true);
    }
    public void apply_edit_title()
    {
        adjective ad_ = (adjective)Enum.Parse(typeof(adjective), edit_title_adjective?.captionText?.text);
        noun noun_ = (noun)Enum.Parse(typeof(noun), edit_title_noun?.captionText?.text);
         if (ad_ == adjective.none || noun_ == noun.none)
         {
             return;
         }
        user_data.title_adjective = ad_;
        user_data.title_noun = noun_;
        is_editing = true;
        update_profile_UI();
    }

    public void show_edit_info_UI() {
        edit_info_input.text = intro_text.text;
        edit_info_UI.SetActive(true);
    }
    public void apply_edit_info()
    {
        if (edit_info_input.text.Equals(string.Empty))
        {
            return;
        }
        user_data.info = edit_info_input.text;
        is_editing = true;
        update_profile_UI();        
    }
    public void show_edit_planet_name_UI()
    {
        edit_planet_name_input.text = planet_name_text.text;
        edit_planet_name_UI.SetActive(true);
    }
    public void apply_edit_planet_name()
    {
        if (edit_planet_name_input.text.Equals(string.Empty)) {
            return;
        }
        user_data.planet_name = edit_planet_name_input.text;
        is_editing = true;
        update_profile_UI();
    }
    public void show_edit_achiv_UI()
    {
        //TODO: 편집 창에 현재 선택된 업적 표기
       // edit_planet_name_input.text = planet_name_text.text;
        edit_achiv_UI.SetActive(true);
        
        achiv_select_manager.refresh(user_data.Achievements_List);
    }
    public void apply_edit_achiv()
    {
        //TODO: 선택된 업적을 프로필 창에 반영
        //TODO: 업적 선택 현황을 userdata에 반영
        //TODO: 관련 DB column 을 select에 추가하기
        user_data.Achievements_List = new List<int>();
        for (int i=0; i < achiv_select_manager.select_list.Count; i++) {
            user_data.Achievements_List.Add(achiv_select_manager.select_list[i]);
        } 
        
        is_editing = true;
        update_profile_UI();
    }
    public void update_profile_UI() {
        pop_text.text = user_data.popularity.ToString();
        title_text.text = user_data.title_adjective.ToString() + " " + user_data.title_noun.ToString();

        planet_name_text.text = user_data.planet_name;

        intro_text.text = user_data.info;

        int[] result = user_data.catchingstar_info.Check_stage_progress();
        chapter_stage_text.text = "챕터" + result[0] + "\n" + result[1] + "스테이지";

        //TODO: 선택 프로필 사진 업데이트 기능
        //TODO: 선택 배경 사진 업데이트 기능
        character_image.sprite = SpriteManager.instance.Num2emozi(user_data.profile_picture);
        background_image.sprite = SpriteManager.instance.Num2BG(user_data.profile_background);

        //character_image = Image_Manager.instance.gt_image(user_data.select_image_id);

        for (int i=0; i < achiv_select_text_arr.Length; i++) {
            achiv_select_text_arr[i].text = (user_data.Achievements_List.Count > i ? BackendChart_JGD.chartData.challenge_list[user_data.Achievements_List[i]].title:"");
        }

        if (is_editing && save_btn!=null)
        {
            save_btn.interactable = true;
        }
        else {
            save_btn.interactable = false;
        }

        //인기도
        if (TCP_Client_Manager.instance.my_player.object_id == now_nickname || user_data.popularity_history.Contains(TCP_Client_Manager.instance.my_player.object_id))
        {
            //TODO: 인기도 버튼 상태 변경
            pop_up_button.GetComponentInChildren<Image>().color = pop_color_yes;
        }
        else { 
            pop_up_button.GetComponentInChildren<Image>().color = pop_color_no;
        }

        StartCoroutine(update_UI_co());

        UIManager_YG.Instance.update_profile();
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
            is_editing = false;
            save_btn.interactable = false;
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
            BackendGameData_JGD.userData.Achievements_List = user_data.Achievements_List;

            BackendGameData_JGD.Instance.GameDataUpdate(save_select);
            is_editing = false;
            save_btn.interactable = false;

            UIManager_YG.Instance.update_profile();
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
        save_btn.interactable = false;
    }

    #endregion

    #region pop up
    public void pop_up()
    {
        if (user_data.popularity_history.Contains(TCP_Client_Manager.instance.my_player.object_id))
        {
            user_data.popularity -= 1;
            user_data.popularity_history.Remove(TCP_Client_Manager.instance.my_player.object_id);
            pop_up_button.GetComponentInChildren<Image>().color = pop_color_no;
        }
        else {
            user_data.popularity += 1;
            user_data.popularity_history.Add(TCP_Client_Manager.instance.my_player.object_id);
            pop_up_button.GetComponentInChildren<Image>().color = pop_color_yes;
        }

        
        string[] select = {"popularity",
            "popularity_history"};
        BackendGameData_JGD.Instance.update_userdata_by_nickname(now_nickname, select, user_data);
        pop_text.text = user_data.popularity.ToString();
        //show_pop_up_result(true);
    }

    public void show_pop_up_result(bool success) {
        StartCoroutine(show_pop_up_result_co(success));
    }

    public IEnumerator show_pop_up_result_co(bool success) {
        pop_up_result_UI.SetActive(true);
        yield return new WaitForSeconds(1);
        pop_up_result_UI.SetActive(false);

    }

    #endregion

    public void add_friend_btn() {
        if (now_nickname !=null && now_nickname != TCP_Client_Manager.instance.my_player.object_id)
        {
            BackendFriend_JDG.Instance.SendFriendRequest(now_nickname);
            add_friend_btn_ob.SetActive(false);
            request_complete_btn_ob.SetActive(true);
        }
    }
    public void delete_friend_btn()
    {
        if (now_nickname != null && now_nickname != TCP_Client_Manager.instance.my_player.object_id)
        {
            Backend.Friend.BreakFriend(FriendList_JGD.friend_dic[now_nickname]);
            add_friend_btn_ob.SetActive(true);
            request_complete_btn_ob.SetActive(false);
            delete_friend_btn_ob.SetActive(false);
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
            //성유경
            QuestManager.instance.Check_mission(Criterion_type.starnest);
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
        BackendGameData_JGD.Instance.update_userdata_by_nickname(now_nickname,  select,  user_data);
        if (now_nickname == TCP_Client_Manager.instance.my_player.object_id)
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
        text_arr[0].text = "<color=#FF9900>" + uuid_+ ":</color>" + content_;
        text_arr[1].text = text_arr[0].text;
    }

    #endregion

    public IEnumerator update_UI_co()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
    }
}
