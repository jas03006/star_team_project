using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_JGD : MonoBehaviour
{
    [SerializeField] GameObject FriendScreen;
    [SerializeField] GameObject FriendList;
    [SerializeField] GameObject FriendList2;
    [SerializeField] GameObject FriendRecommnedList;

    [SerializeField] GameObject FriendMiniScreen1;
    [SerializeField] GameObject FriendMiniScreen2;
    [SerializeField] GameObject FriendRecommendMiniScreen;

    [SerializeField] Button friend_list_btn;
    [SerializeField] Button request_list_btn;
    [SerializeField] Button recommend_list_btn;

    [SerializeField] Button reject_btn;
    [SerializeField] Button show_profile_btn;
    [SerializeField] Button accept_btn;

    [SerializeField] TMP_InputField friend_request_input;

    public int now_selection = 0;
    public void OpenFriend() //친구창 열기
    {
        FriendScreen.SetActive(true);
        FriendminiscreenOpen();
        friend_request_input.text = "";
        //BackendFriend_JDG.Instance.GetFriendList();
    }
    public void CloseFriend() //친구창 닫기
    {
        
        FriendScreen.SetActive(false);
    }
    public void DeletList() // 리스트 삭제
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
        now_selection = 2;

        friend_list_btn.interactable = true;
        request_list_btn.interactable = false;
        recommend_list_btn.interactable = true;

        FriendMiniScreen1.SetActive(false);
        FriendMiniScreen2.SetActive(true);
        FriendRecommendMiniScreen.SetActive(false);

        reject_btn.gameObject.SetActive(true);
        accept_btn.gameObject.SetActive(true);
        reject_btn.interactable = false;
        accept_btn.interactable = false;
    }

    public void FriendminiscreenOpen() 
    {
        now_selection = 0;

        friend_list_btn.interactable = false;
        request_list_btn.interactable = true;
        recommend_list_btn.interactable = true;

        FriendMiniScreen1.SetActive(true);
        FriendMiniScreen2.SetActive(false);
        FriendRecommendMiniScreen.SetActive(false);

        reject_btn.gameObject.SetActive(false);
        accept_btn.gameObject.SetActive(false);
    }
    public void RecommendminiscreenOpen()
    {
        now_selection = 1;

        friend_list_btn.interactable = true;
        request_list_btn.interactable = true;
        recommend_list_btn.interactable = false;

        FriendMiniScreen1.SetActive(false);
        FriendMiniScreen2.SetActive(false);
        FriendRecommendMiniScreen.SetActive(true);

        reject_btn.gameObject.SetActive(false);
        accept_btn.gameObject.SetActive(false);
    }
    public void activate_btn_after_select() {
        reject_btn.interactable = true;
        accept_btn.interactable = true;
        show_profile_btn.interactable = true;
    }
    public void deactivate_btn_after_unselect()
    {
        reject_btn.interactable = false;
        accept_btn.interactable = false;
        show_profile_btn.interactable = false;
    }
}
