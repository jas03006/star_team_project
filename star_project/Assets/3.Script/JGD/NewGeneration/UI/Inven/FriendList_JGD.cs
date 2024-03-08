using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FriendList_JGD : MonoBehaviour
{
    public UIManager_JGD uimanager;
    //public Star_nest_UI star_nest_UI;

    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject location;

    [SerializeField] private GameObject Recommend_Friend_prefab;
    [SerializeField] private GameObject recommend_location;

    [Header("airship")]
    [SerializeField] private GameObject check_join_go;
    [SerializeField] private TMP_Text check_join_text;
    [SerializeField] private GameObject check_invite_go;
    [SerializeField] private TMP_Text check_invite_text;
    [SerializeField] private GameObject invite_result_go;
    [SerializeField] private TMP_Text invite_result_text;



    TMP_Text name;

    private GameObject select_profile_go = null;
    private string select_nickname = string.Empty;
    private string select_indate = string.Empty;

    public static Dictionary<string,string> friend_dic = new Dictionary<string,string>();
    private void Start()
    {
        
    }

    public static bool is_friend(string nickname_) {
        return friend_dic.ContainsKey(nickname_);
    }

    public void GetFriendList(bool is_airship = false)
    {
        var bro = Backend.Friend.GetFriendList();

        friend_dic.Clear();

        //int index = 0;
        foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            GameObject list = Instantiate(Friend, location.transform);
            //list.transform.SetParent(location.transform);
            name = list.GetComponentInChildren<TMP_Text>();
            name.text = nickName;
            Button[] buttons = list.GetComponentsInChildren<Button>();
            if (is_airship) {
                if (buttons.Length >= 2)
                {
                    buttons[0].onClick.AddListener(() => TCP_Client_Manager.instance.join(nickName));
                    buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                    buttons[0].onClick.AddListener(() => hide_airchip_UI());

                    buttons[1].onClick.AddListener(() => TCP_Client_Manager.instance.invite(nickName));
                    buttons[1].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                }
                else {
                    buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                    buttons[0].onClick.AddListener(() => select_element(nickName, inDate));
                }

            }
            else  {
                if (buttons.Length>=2) {
                    buttons[0].onClick.AddListener(() => UIManager_YG.Instance.star_nest_UI.show_UI(true, nickName));
                    buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());

                    buttons[1].onClick.AddListener(() => KillMyFriend(inDate, list));
                    buttons[1].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                }else
                {
                    buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                    buttons[0].onClick.AddListener(() => select_element(nickName, inDate));
                }


            }
            friend_dic[nickName] = inDate;

            //index++;
        }
        
        QuestManager.instance.Check_challenge(Clear_type.add_friend);

    }
    public void ClearFriendList()
    {
        int cnt= location.transform.childCount;
        for (int i =0; i < cnt; i++) {
            Destroy(location.transform.GetChild(i).gameObject);            
        }
        friend_dic.Clear();
    }
    public void KillMyFriend(string Friend, GameObject list)
    {
        Backend.Friend.BreakFriend(Friend);
        Debug.Log($"{Friend} 컷!!!!!!!!!!!!!!!!");
        Destroy(list);
    }

    public void go_myplanet_btn() {
        // TCP_Client_Manager.instance.go_myplanet();
        TCP_Client_Manager.instance.join(TCP_Client_Manager.instance.my_player.object_id);
    }


    private FriendList_JGD airship_UI = null;
    private string airship_UI_tag = "airship_UI";
    [SerializeField] private GameObject go_myplanet_btn_ob;
    public void open_Airchip_UI() {
        if (airship_UI == null) {
            airship_UI = GameObject.FindGameObjectWithTag(airship_UI_tag).GetComponent<FriendList_JGD>();
        }
        
        if (!airship_UI.transform.GetChild(0).gameObject.activeSelf)
        {
            airship_UI.transform.GetChild(0).gameObject.SetActive(true);
            airship_UI.ClearFriendList();
            airship_UI.GetFriendList(true);      
            
        }
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id) {
            go_myplanet_btn_ob.SetActive(false);
        }
        else {
            go_myplanet_btn_ob.SetActive(true);
        }
    }

    public void hide_airchip_UI() {
        if (airship_UI == null)
        {
            airship_UI = GameObject.FindGameObjectWithTag(airship_UI_tag).GetComponent<FriendList_JGD>();
        }

        if (airship_UI.transform.GetChild(0).gameObject.activeSelf)
        {
            airship_UI.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ClearRecommendList()
    {
        int cnt = recommend_location.transform.childCount;
        for (int i = 0; i < cnt; i++)
        {
            Destroy(recommend_location.transform.GetChild(i).gameObject);
        }
    }
    public void show_recommend_friends() {
        ClearRecommendList();
        BackendFriend_JDG.GetSentRequestFriend();
       
        BackendReturnObject bro = Backend.GameData.Get("USER_DATA", new Where(),100);


        if (bro.IsSuccess())
        {
            if (bro.Rows().Count > 0)
            {
                int numcount = 0;
                LitJson.JsonData gameDataJson = bro.FlattenRows();

                foreach (LitJson.JsonData friendJson in gameDataJson)
                {
                    try {
                        string nickName = friendJson["nickname"].ToString();

                        if (nickName == null || friend_dic.ContainsKey(nickName) || TCP_Client_Manager.instance.my_player.object_id == nickName)
                        {
                            continue;
                        }

                        var n_bro = Backend.Social.GetUserInfoByNickName(nickName);
                        if (!n_bro.IsSuccess() || !n_bro.HasReturnValue())
                        {
                            continue;
                        }
                        //example
                        string inDate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

                        GameObject list = Instantiate(Recommend_Friend_prefab, recommend_location.transform);

                        name = list.GetComponentInChildren<TMP_Text>();
                        name.text = nickName;

                        int ind = numcount;
                        Button[] buttons = list.GetComponentsInChildren<Button>();
                        if (buttons.Length >= 2)
                        {
                            buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                            buttons[0].onClick.AddListener(() => UIManager_YG.Instance.star_nest_UI.show_UI(true, nickName));

                            if (BackendFriend_JDG.is_requested(nickName))
                            {
                                buttons[1].interactable = false;
                                buttons[1].GetComponentInChildren<TMP_Text>().text = "요청 완료";
                            }
                            else
                            {
                                buttons[1].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                                buttons[1].onClick.AddListener(() => Backend.Friend.RequestFriend(inDate));
                                buttons[1].onClick.AddListener(() => BackendFriend_JDG._sentRequestList.Add(nickName));
                                buttons[1].onClick.AddListener(() => off_friend_request(buttons[1]));
                            }

                            //  buttons[1].onClick.AddListener(() => BackendFriend_JDG.Instance.reject_friend_request(ind));
                            //  buttons[1].onClick.AddListener(() => MyFriend(list));
                        }
                        else {
                            buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                            buttons[0].onClick.AddListener(() => select_element(nickName, inDate));
                        }
                        numcount++;
                    } catch {
                        continue;
                    }
                    
                }

            }
        }
       
    }

    private void off_friend_request(Button btn) {
        try {
            btn.interactable = false;
            btn.GetComponentInChildren<TMP_Text>().text = "요청 완료";
        } catch {
        }        
    }
    public void init_selections() {
        select_nickname = string.Empty;
        select_indate = string.Empty;
        select_profile_go = null;
        if (uimanager != null)
        {
            uimanager.deactivate_btn_after_unselect();
        }
    }

    public void select_element(string nickName, string indate, GameObject go = null) {
        select_nickname = nickName;
        select_indate = indate;
        select_profile_go = go;
        if (uimanager !=null) {
            uimanager.activate_btn_after_select();
        }       
    }

    public void show_profile() {
        if (select_indate != string.Empty)
        {
            UIManager_YG.Instance.star_nest_UI.show_UI(true, select_nickname);
            init_selections();
        }
    }

    public void accept_friend() {
        if (select_indate != string.Empty) {
            Backend.Friend.AcceptFriend(select_indate);
        }
        
        if (select_profile_go != null) {
            Destroy(select_profile_go);
            init_selections();
        }
        
    }
    public void reject_friend()
    {
        if (select_indate != string.Empty)
        {
            Backend.Friend.RejectFriend(select_indate);
        }
        if (select_profile_go != null)
        {
            Destroy(select_profile_go);
            init_selections();
        }
        
    }



    public void invite_friend()
    {
        if (select_nickname != string.Empty)
        {
            TCP_Client_Manager.instance.invite(select_nickname);
        }
    }
    public void join_friend()
    {
        if (select_nickname != string.Empty)
        {
            TCP_Client_Manager.instance.join(select_nickname);
            init_selections();
            hide_airchip_UI();
        }
    }

    public void show_check_join() {
        check_join_go.SetActive(true);
        check_join_text.text = "<color=white><"+select_nickname+"></color>님의 \r\n행성으로 이동할까요?";
    }

    public void show_check_invite()
    {
        check_invite_go.SetActive(true);
        check_invite_text.text = "<color=white><" + select_nickname + "></color>님을 \r\n내 행성으로 초대할까요?";
    }
    public void show_invite_result()
    {
        invite_result_go.SetActive(true);
       // invite_result_text.text = "친구에게 초대장이 발송됐어요!";
    }
}
