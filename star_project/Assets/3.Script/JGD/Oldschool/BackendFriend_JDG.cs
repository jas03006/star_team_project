using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BackEnd;
using TMPro;

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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this);
            return;
        }
        GetSentRequestFriend();
    }


    public List<Tuple<string, string>> _requestFriendList = new List<Tuple<string, string>>();
    public static List<string> _sentRequestList = new List<string>();

    public void SendFriendRequest(string nickName)
    {
        //ģ�� ��û ������
        var inDateBro = Backend.Social.GetUserInfoByNickName(nickName);

        if (inDateBro.IsSuccess() == false)
        {
            Debug.LogError("���� �̸� �˻� ���� ������ �߻��߽��ϴ�.");
            return;
        }

        string inDate = inDateBro.GetReturnValuetoJSON()["row"]["inDate"].ToString();

        Debug.Log($"{nickName}�� inDate���� {inDate} �Դϴ�");

        var friendBro = Backend.Friend.RequestFriend(inDate);

        if (friendBro.IsSuccess() == false)
        {
            Debug.LogError($"{inDate} ģ�� ��û ���� ������ �߻��߽��ϴ�."+ friendBro);
            return;
        }
        Debug.Log("ģ�� ��û�� �����߽��ϴ�." + friendBro);
        _sentRequestList.Add(nickName);
        QuestManager.instance.Check_mission(Criterion_type.friend);
    }

    public static bool is_requested(string nickname) { 
        return _sentRequestList.Contains(nickname);
    }
    
    public void GetReceivedRequestFriend()
    {
        //ģ����û �ҷ�����
        var bro = Backend.Friend.GetReceivedRequestList();

        if (bro.IsSuccess()==false)
        {
            Debug.Log("ģ�� ��û ���� ����Ʈ�� �ҷ����� �� ������ �߻��߽��ϴ�.");
            return;
        }
        if (bro.FlattenRows().Count <= 0)
        {
            _requestFriendList.Clear();
            Debug.LogError("ģ�� ��û�� �� ������ �������� �ʽ��ϴ�.");
            return;
        }
        Debug.Log("ģ�� ��û ���� ����Ʈ �ҷ����⿡ �����߽��ϴ�." +bro);

        int index = 0;
        _requestFriendList.Clear();
        foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            _requestFriendList.Add(new Tuple<string, string>(nickName, inDate));

            Debug.Log($"{index}. {nickName} - {inDate}");
            index++;
        }

    }
    public string get_indate(int index) {
        if (_requestFriendList.Count <= 0)
        {
            Debug.LogError("��û�� �� ģ���� �������� �ʽ��ϴ�.");
            return "";
        }
        if (index >= _requestFriendList.Capacity)
        {
            Debug.LogError($"��û�� ģ�� ��û ����Ʈ�� ������ ������ϴ�.���� : {index} / ����Ʈ �ִ� : {_requestFriendList.Count}");
            return "";
        }
        return _requestFriendList[index].Item2;
    }
    public void reject_friend_request(int index) {
        //ģ����û rjwjf�ϱ� 
        if (_requestFriendList.Count <= 0)
        {
            Debug.LogError("��û�� �� ģ���� �������� �ʽ��ϴ�.");
            return;
        }
        if (index >= _requestFriendList.Capacity)
        {
            Debug.LogError($"��û�� ģ�� ��û ����Ʈ�� ������ ������ϴ�.���� : {index} / ����Ʈ �ִ� : {_requestFriendList.Count}");
            return;
        }
        var bro = Backend.Friend.RejectFriend(_requestFriendList[index].Item2);
        if (bro.IsSuccess() == false)
        {
            Debug.LogError("ģ�� ���� �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log($"{_requestFriendList[index].Item1}�� ģ����û�� �����߽��ϴ�. : " + bro);
        
    }
    public void ApplyFriend(int index)
    {
        //ģ����û �����ϱ� 
        if (_requestFriendList.Count <=0)
        {
            Debug.LogError("��û�� �� ģ���� �������� �ʽ��ϴ�.");
            return;
        }
        if (index >= _requestFriendList.Capacity)
        {
            Debug.LogError($"��û�� ģ�� ��û ����Ʈ�� ������ ������ϴ�.���� : {index} / ����Ʈ �ִ� : {_requestFriendList.Count}");
            return;
        }



        var bro = Backend.Friend.AcceptFriend(_requestFriendList[index].Item2);

        if (bro.IsSuccess()==false)
        {
            Debug.LogError("ģ�� ���� �� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        Debug.Log($"{_requestFriendList[index].Item1}��(��) ģ���� �Ǿ����ϴ�. : " + bro);
    }

    public void GetFriendList()
    {
        //ģ�� ����Ʈ �ҷ�����
        var bro = Backend.Friend.GetFriendList();

        if (bro.IsSuccess()==false)
        {
            Debug.LogError("ģ�� ��� �ҷ����� �߿����� �߻��߽��ϴ�. : " + bro);
            return;
        }
        Debug.Log("ģ�� ��� �ҷ����⿡ �����߽��ϴ�. : " + bro);

        if (bro.FlattenRows().Count <= 0)
        {
            Debug.Log("ģ���� �������� �ʽ��ϴ�.");
            return;
        }
        int index = 0;
        string friendListString = "ģ�����\n";


        foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            friendListString += $"{index}. {nickName} - {inDate}\n";
            index++;
            Debug.Log(friendListString);
        }

        //Debug.Log(friendListString);

    }
    public static void GetSentRequestFriend()
    {
        
        //���� ģ����û �ҷ�����
        var bro = Backend.Friend.GetSentRequestList();

        if (bro.IsSuccess() == false)
        {
            Debug.Log("ģ�� ��û ���� ����Ʈ�� �ҷ����� �� ������ �߻��߽��ϴ�.");
            return;
        }
        if (bro.FlattenRows().Count <= 0)
        {
            _sentRequestList.Clear();
            Debug.LogError("ģ�� ��û�� ���� ������ �������� �ʽ��ϴ�.");
            return;
        }
        Debug.Log("ģ�� ��û ���� ����Ʈ �ҷ����⿡ �����߽��ϴ�." + bro);

        int index = 0;
        _sentRequestList.Clear();
        foreach (LitJson.JsonData friendJson in bro.FlattenRows())
        {
            string nickName = friendJson["nickname"]?.ToString();
            string inDate = friendJson["inDate"].ToString();

            _sentRequestList.Add(nickName);

            //Debug.Log($"{index}. {nickName} - {inDate}");
            index++;
        }

    }

}