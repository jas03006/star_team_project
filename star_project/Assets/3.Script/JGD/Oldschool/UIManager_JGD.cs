using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager_JGD : MonoBehaviour
{
    [SerializeField] GameObject FriendScreen;
    [SerializeField] GameObject FriendList;
    [SerializeField] GameObject FriendList2;

    [SerializeField] GameObject FriendMiniScreen1;
    [SerializeField] GameObject FriendMiniScreen2;


    public void OpenFriend()
    {
        FriendScreen.SetActive(true);
        FriendminiscreenOpen();
        BackendFriend_JDG.Instance.GetFriendList();
    }
    public void CloseFriend()
    {
        
        FriendScreen.SetActive(false);
    }
    public void DeletList()
    {
        for (int i = 0; i < FriendList.transform.childCount; i++)
        {
            Destroy(FriendList.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < FriendList2.transform.childCount; i++)
        {
            Destroy(FriendList2.transform.GetChild(i).gameObject);
        }
    }
    public void RequestMiniScene()
    {
        FriendMiniScreen1.SetActive(false);
        FriendMiniScreen2.SetActive(true);
    }

    public void FriendminiscreenOpen()
    {
        FriendMiniScreen1.SetActive(true);
        FriendMiniScreen2.SetActive(false);
    }

}
