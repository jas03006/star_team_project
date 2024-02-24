using BackEnd;
using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : Quest
{
    public Challenge_userdata userdata;

    //차트
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
        //완료O, 보상수령X상태가 아니면 return
        if (userdata.state != challenge_state.can_reward)
            return;

        //보상 지급
        BackendGameData_JGD.userData.CP += CP;
        Data_update();

        //상태변경
        userdata.state = challenge_state.complete;
    }

    public void Data_update()
    {
        Param param = new Param();
        param.Add("CP", BackendGameData_JGD.userData.CP);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
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
    public int criterion; //현재 수치
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
        //데이터에 넣기
        Param param = new Param();
        param.Add("mission_Userdatas", BackendGameData_JGD.userData.mission_Userdatas);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
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
