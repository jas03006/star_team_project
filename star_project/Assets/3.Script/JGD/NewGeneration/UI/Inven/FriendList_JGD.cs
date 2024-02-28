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
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject location;

    [SerializeField] private GameObject Recommend_Friend_prefab;
    [SerializeField] private GameObject recommend_location;
    TMP_Text name;


    private Dictionary<string,string> friend_dic = new Dictionary<string,string>();
    private void Start()
    {
        
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
                buttons[0].onClick.AddListener(() => TCP_Client_Manager.instance.join(nickName));
                buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());

                buttons[1].onClick.AddListener(() => TCP_Client_Manager.instance.invite(nickName));
                buttons[1].onClick.AddListener(() => AudioManager.instance.SFX_Click());

            }
            else  {
                //TODO:친밀도 시스템
                //buttons[0].onClick.AddListener(() => TCP_Client_Manager.instance.join(nickName));
                buttons[1].onClick.AddListener(() => KillMyFriend(inDate, list));
                buttons[1].onClick.AddListener(() => AudioManager.instance.SFX_Click());

            }
            friend_dic[nickName] = inDate;

            //index++;
        }
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
        BackendReturnObject bro = Backend.GameData.Get("USER_DATA", new Where(),100);


        if (bro.IsSuccess())
        {
            if (bro.Rows().Count > 0)
            {
                int numcount = 0;
                LitJson.JsonData gameDataJson = bro.FlattenRows();

                foreach (LitJson.JsonData friendJson in gameDataJson)
                {
                    string nickName = friendJson["planet_name"].ToString();

                    if (friend_dic.ContainsKey(nickName) || TCP_Client_Manager.instance.my_player.object_id == nickName || nickName==null) {
                        continue;
                    }

                    var n_bro = Backend.Social.GetUserInfoByNickName(nickName);
                    if (!n_bro.IsSuccess() || !n_bro.HasReturnValue()) {
                        continue;
                    }
                    //example
                    string inDate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

                    GameObject list = Instantiate(Recommend_Friend_prefab, recommend_location.transform);

                    name = list.GetComponentInChildren<TMP_Text>();
                    name.text = nickName;

                    int ind = numcount;
                    Button[] buttons = list.GetComponentsInChildren<Button>();
                    if (buttons.Length >= 1)
                    {
                        buttons[0].onClick.AddListener(() => Backend.Friend.RequestFriend(inDate));
                        buttons[0].onClick.AddListener(() => Destroy(list));
                        buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
                        //  buttons[1].onClick.AddListener(() => BackendFriend_JDG.Instance.reject_friend_request(ind));
                        //  buttons[1].onClick.AddListener(() => MyFriend(list));
                    }
                    numcount++;
                }

            }
        }

       
    }
  
}
