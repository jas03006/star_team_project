using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchingstar_info
{
    public List<Galaxy_info> galaxy_Info_list = new List<Galaxy_info>();

    public Catchingstar_info()
    {
        for (int i = 0; i < 5; i++)
        {
            galaxy_Info_list.Add(new Galaxy_info());
        }
    }

    public Catchingstar_info(JsonData jsonData)
    {
        foreach (JsonData json in jsonData["galaxy_Info_list"])
        {
            galaxy_Info_list.Add(new Galaxy_info(json));
        }
    }

    public void Data_update()
    {
        //데이터에 넣기
        Param param = new Param();
        param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

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
}

public enum Galaxy_state //은하별 미션 완료상태
{
    incomplete = 0,
    can_reward,
    complete
}

public class Galaxy_info
{
    //public int collect_point;//해당 스테이지에서 모은 별의 갯수
    public List<Star_info> star_Info_list = new List<Star_info>();
    public List<Galaxy_state> mission_state = new List<Galaxy_state>();

    public Galaxy_info()
    {
        //star_Info_list
        for (int i = 0; i < 5; i++)
        {
            star_Info_list.Add(new Star_info());
        }

        //galaxy_state
        for (int i = 0; i < 3; i++)
        {
            mission_state.Add(Galaxy_state.incomplete);
        }

        //collect
        //collect_point = 0;
    }

    public Galaxy_info(JsonData jsonData)
    {
        //star_Info_list
        foreach (JsonData json in jsonData["star_Info_list"])
        {
            star_Info_list.Add(new Star_info(json));
        }

        //galaxy_state
        foreach (JsonData json in jsonData["mission_state"])
        {
            mission_state.Add((Galaxy_state)int.Parse(json.ToString()));
        }

        //collect
        //collect_point = int.Parse(jsonData["collect_point"].ToString());
    }
}

public class Star_info
{
    public bool is_clear;
    public int star;
    public bool get_housing;

    public Star_info()
    {
        is_clear = false;
        star = 0;
        get_housing = false;
    }

    public Star_info(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        star = int.Parse(jsonData["star"].ToString());
        get_housing = bool.Parse(jsonData["get_housing"].ToString());
    }
}
