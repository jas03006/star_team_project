using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendRequest_JGD : MonoBehaviour
{
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject location;
    TMP_Text name;
    public int numcount;



    public void GetFriendRequestList()
    {
        //GetReceivedRequestFriend()
        //foreach _requestFriendList
        //

        BackendFriend_JDG.Instance.GetReceivedRequestFriend();
        numcount = 0;
        foreach  (Tuple<string, string> request in BackendFriend_JDG.Instance._requestFriendList)
        {
            string nickName = request.Item1;

            Debug.Log($"{nickName}");
            int ind = numcount;

            GameObject list = Instantiate(Friend, location.transform);
            name = list.GetComponentInChildren<TMP_Text>();
            name.text = nickName;
            list.GetComponentInChildren<Button>().onClick.AddListener(() => BackendFriend_JDG.Instance.ApplyFriend(ind));
            
            numcount++;
        }

        

        //var bro = Backend.Friend.GetReceivedRequestList();
        //
        //foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        //{
        //    string nickName = friendJson["nickname"]?.ToString();
        //
        //    Debug.Log($"{nickName}");
        //    index++;
        //
        //    GameObject list = Instantiate(Friend, location.transform);
        //    name = list.GetComponentInChildren<TMP_Text>();
        //    name.text = nickName;
        //    list.GetComponentInChildren<Button>().onClick.AddListener(() => BackendFriend_JDG.Instance.ApplyFriend(index));
        //
        //}
    }



}
