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

    [SerializeField] FriendList_JGD friendlist;
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
            
            Button[] buttons = list.GetComponentsInChildren<Button>();
            if (buttons.Length >= 2)
            {
                buttons[0].onClick.AddListener(() => BackendFriend_JDG.Instance.ApplyFriend(ind));
                buttons[0].onClick.AddListener(() => MyFriend(list));
                buttons[1].onClick.AddListener(() => BackendFriend_JDG.Instance.reject_friend_request(ind));
                buttons[1].onClick.AddListener(() => MyFriend(list));
            }
            else {
                buttons[0].onClick.AddListener(() => friendlist.select_element(nickName, BackendFriend_JDG.Instance.get_indate(ind), list));
                buttons[0].onClick.AddListener(() => AudioManager.instance.SFX_Click());
            }
            numcount++;
        }              
             
    }
    public void MyFriend(GameObject list)
    {
        Debug.Log($"{Friend} Ãß°¡");
        Destroy(list);
    }


}
