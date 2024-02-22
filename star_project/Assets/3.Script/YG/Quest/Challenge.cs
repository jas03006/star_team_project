using BackEnd;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : Quest
{
    //유저 데이터
    Challenge_userdata userdata;
    public housing_itemID housing_ItemID; //보상으로 받을 하우징 아이템 id
}

public class Challenge_userdata
{
    public bool is_clear;
    public int CP; //challenge point
    public bool get_rewarded;
    public int criterion; //현재 수치
    public Dictionary<int, Challenge_info> data_dic;

    public Challenge_userdata(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());
        get_rewarded = bool.Parse(jsonData["get_rewarded"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());

        for (int i = 0; i < 9; i++) //9 = 업적 갯수
        {
            data_dic.Add(i, new Challenge_info(jsonData["data_dic"].ToString()));
        }
    }

    public Challenge_userdata()
    {
        is_clear = false;
        CP = 0;
        get_rewarded = false;
        criterion = 0;

        for (int i = 0; i < 9; i++) //9 = 업적 갯수
        {
            data_dic.Add(i,new Challenge_info());
        }
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
