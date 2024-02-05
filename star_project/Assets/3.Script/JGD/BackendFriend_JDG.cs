using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BackEnd;

public class BackendFriend_JDG : MonoBehaviour
{
    private static BackendFriend_JDG instance = null;
    public static BackendFriend_JDG Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendFriend_JDG();
            }
            return instance;
        }
        
    }


    private List<Tuple<string, string>> _requestFriendList = new List<Tuple<string, string>>();

    public void SendFriendRequest(string nickName)
    {
        //친구 요청 보내기
        var inDateBro = Backend.Social.GetUserInfoByNickName(nickName);

        if (inDateBro.IsSuccess() == false)
        {
            Debug.LogError("유저 이름 검색 도중 에러가 발생했습니다.");
            return;
        }

        string inDate = inDateBro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        Debug.Log($"{nickName}의 inDate값은 {inDate} 입니다");

        var friendBro = Backend.Friend.RequestFriend(inDate);

        if (friendBro.IsSuccess() == false)
        {
            Debug.LogError($"{inDate} 친구 요청 도중 에러가 발생했습니다.");
            return;
        }
        Debug.Log("친구 요청에 성공했습니다." + friendBro);
    }

    public void GetReceivedRequestFriend()
    {
        //친구요청 불러오기
        var bro = Backend.Friend.GetReceivedRequestList();

        if (bro.IsSuccess()==false)
        {
            Debug.Log("친구 요청 받은 리스트를 불러오는 중 에러가 발생했습니다.");
            return;
        }
        if (bro.FlattenRows().Count <= 0)
        {
            Debug.LogError("친구 요청이 온 내역이 존재하지 않습니다.");
            return;
        }
        Debug.Log("친구 요청 받은 리스트 불러오기에 성공했습니다." +bro);

        int index = 0;
        foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            _requestFriendList.Add(new Tuple<string, string>(nickName, inDate));

            Debug.Log($"{index}. {nickName} - {inDate}");
            index++;
        }

    }

    public void ApplyFriend(int index)
    {
        //친구요청 수락하기 
        if (_requestFriendList.Count <=0)
        {
            Debug.LogError("요청이 온 친구가 존재하지 않습니다.");
            return;
        }
        if (index >= _requestFriendList.Capacity)
        {
            Debug.LogError($"요청한 친구 요청 리스트의 범위를 벗어났습니다.선택 : {index} / 리스트 최대 : {_requestFriendList.Count}");
            return;
        }

        var bro = Backend.Friend.AcceptFriend(_requestFriendList[index].Item2);

        if (bro.IsSuccess()==false)
        {
            Debug.LogError("친구 수락 중 에러가 발생했습니다. : " + bro);
            return;
        }

        Debug.Log($"{_requestFriendList[index].Item1}이(가) 친구가 되었습니다. : " + bro);
    }

    public void GetFriendList()
    {
        //친구 리스트 불러오기
        var bro = Backend.Friend.GetFriendList();

        if (bro.IsSuccess()==false)
        {
            Debug.LogError("친구 목록 불러오기 중에러가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("친구 목록 불러오기에 성공했습니다. : " + bro);

        if (bro.FlattenRows().Count <= 0)
        {
            Debug.Log("친구가 존재하지 않습니다.");
            return;
        }
        int index = 0;
        string friendListString = "친구목록\n";

        foreach(LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            friendListString += $"{index}. {nickName} - {inDate}\n";
            index++;
        }

        Debug.Log(friendListString);
    }
}