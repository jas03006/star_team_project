using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 별로 스테이지 진행 상황을 저장하기 위해 만든 클래스.
/// 5개의 은하가 있으며 각 은하별로 5개의 스테이지가 있음.
/// 상속구조 : Catchingstar_info(전체를 감싸고 있는 클래스) -> Galaxy_info(각 은하별 정보를 담고있는 클래스) -> starinfo(각 스테이지 별 정보를 담고있는 클래스)
/// </summary>
public class Catchingstar_info
{
    public List<Galaxy_info> galaxy_Info_list = new List<Galaxy_info>();//각 은하 별 진행 상황 저장

    public Catchingstar_info() //신규 회원 - 데이터 생성
    {
        for (int i = 0; i < 5; i++)
        {
            galaxy_Info_list.Add(new Galaxy_info());
        }
    }

    public Catchingstar_info(JsonData jsonData) //기존 회원 - 데이터 불러오기
    {
        foreach (JsonData json in jsonData["galaxy_Info_list"])
        {
            galaxy_Info_list.Add(new Galaxy_info(json));
        }
    }

    public void Data_update() //캐칭스타 데이터 업데이트
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

    public int[] Check_stage_progress() //클리어 한 마지막 스테이지를 확인
    {
        int[] result = {1,1};
        for (int i = 0; i < galaxy_Info_list.Count; i++)
        {
            for (int j = 0; j < galaxy_Info_list[i].star_Info_list.Count; j++)
            {
                if (galaxy_Info_list[i].star_Info_list[j].is_clear == false)
                {
                    result[0] = i+1;
                    result[1] = j+1;
                    return result;
                }
            }
        }
        return result;
    }
}

public enum Galaxy_state //은하별 미션상태
{
    incomplete = 0,
    can_reward,
    complete
}

public class Galaxy_info //은하 정보를 담은 클래스 
{
    public bool is_clear = false; //은하 클리어 여부
    public List<Star_info> star_Info_list = new List<Star_info>(); //각 스테이지 정보 저장
    public List<Galaxy_state> mission_state = new List<Galaxy_state>(); //은하 미션 진행도

    public Galaxy_info()//신규 회원 - 데이터 생성
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

        is_clear = false;
    }

    public Galaxy_info(JsonData jsonData) //기존 회원 - 데이터 불러오기
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
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
    }
}

public class Star_info //스테이지 정보를 담은 클래스 
{
    public bool is_clear; //클리어 여부
    public int star; //레드스타 갯수
    public bool get_housing; //하우징 아이템 획득 여부

    public Star_info()//신규 회원 - 데이터 생성
    {
        is_clear = false;
        star = 0;
        get_housing = false;
    }

    public Star_info(JsonData jsonData)//기존 회원 - 데이터 불러오기
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        star = int.Parse(jsonData["star"].ToString());
        get_housing = bool.Parse(jsonData["get_housing"].ToString());
    }
}
