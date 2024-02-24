using BackEnd;
using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : Quest
{
    public Challenge_userdata userdata;

    //��Ʈ
    public challenge_id id;
    public int CP; //reward
    public string contents2;

    public Challenge(JsonData jsonData, int index)
    {
        id = (challenge_id)int.Parse(jsonData["challenge_id"].ToString());
        goal = int.Parse(jsonData["goal"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());

        userdata = BackendGameData_JGD.userData.challenge_Userdatas[index];

        title = jsonData["title"].ToString();
        contents = jsonData["contents"].ToString();
        contents2 = jsonData["contents2"].ToString();
    }

    public void Get_reward()
    {
        //�Ϸ�O, �������X���°� �ƴϸ� return
        if (userdata.state != challenge_state.can_reward)
            return;

        //���� ����
        BackendGameData_JGD.userData.CP += CP;
        Data_update();

        //���º���
        userdata.state = challenge_state.complete;
    }

    public void Data_update()
    {
        Param param = new Param();
        param.Add("CP", BackendGameData_JGD.userData.CP);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }

    public bool Check_clear()
    {
        if (userdata.criterion >= goal && userdata.state == challenge_state.incomplete)
        {
            userdata.is_clear = true;
            userdata.state = challenge_state.can_reward;
            return true;
        }
        return false;
    }
}

public class Challenge_userdata
{
    public bool is_clear;
    public int CP; //challenge point
    public bool get_rewarded;
    public int criterion; //���� ��ġ
    public challenge_state state;

    public Challenge_userdata(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());
        get_rewarded = bool.Parse(jsonData["get_rewarded"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());
        state = (challenge_state)int.Parse(jsonData["state"].ToString());
    }

    public Challenge_userdata()
    {
        is_clear = false;
        CP = 0;
        get_rewarded = false;
        criterion = 0;
        state = challenge_state.incomplete;
    }

    public void Data_update()
    {
        //�����Ϳ� �ֱ�
        Param param = new Param();
        param.Add("mission_Userdatas", BackendGameData_JGD.userData.mission_Userdatas);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }

    public class Challenge_info
    {
        public bool is_clear;
        public bool get_rewarded;

        public Challenge_info() 
        {
            is_clear = false;
            get_rewarded = false;
        }

        public Challenge_info(JsonData jsondata)
        {
            is_clear = bool.Parse(jsondata["is_clear"].ToString());
            get_rewarded = bool.Parse(jsondata["get_rewarded"].ToString());
        }
    }
}
