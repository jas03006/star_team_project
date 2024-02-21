using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendList_JGD : MonoBehaviour
{
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject location;
    TMP_Text name;



    public void GetFriendList()
    {
        var bro = Backend.Friend.GetFriendList();

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
            if (buttons.Length >= 2) {
                buttons[0].onClick.AddListener(() => TCP_Client_Manager.instance.join(nickName));
                buttons[1].onClick.AddListener(() => TCP_Client_Manager.instance.invite(nickName));
                buttons[2].onClick.AddListener(() => KillMyFriend(inDate, list));
            }
            

            //index++;
        }
    }
    public void ClearFriendList()
    {
        int cnt= location.transform.childCount;
        for (int i =0; i < cnt; i++) {
            Destroy(location.transform.GetChild(0).gameObject);
        }
    }
    public void KillMyFriend(string Friend, GameObject list)
    {
        Backend.Friend.BreakFriend(Friend);
        Debug.Log($"{Friend} ÄÆ!!!!!!!!!!!!!!!!");
        Destroy(list);
    }

    public void go_myplanet_btn() {
        // TCP_Client_Manager.instance.go_myplanet();
        TCP_Client_Manager.instance.join(TCP_Client_Manager.instance.my_player.object_id);
    }
}
