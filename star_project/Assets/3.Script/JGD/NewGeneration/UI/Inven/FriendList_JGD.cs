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
            name = list.GetComponentInChildren<TMP_Text>();
            name.text = nickName;
            list.GetComponentInChildren<Button>().onClick.AddListener(() => KillMyFriend(inDate, list));

            //index++;
        }
    }

    public void KillMyFriend(string Friend, GameObject list)
    {
        Backend.Friend.BreakFriend(Friend);
        Debug.Log($"{Friend} ÄÆ!!!!!!!!!!!!!!!!");
        Destroy(list);
    }
}
