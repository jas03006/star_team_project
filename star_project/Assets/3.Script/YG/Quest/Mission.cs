using BackEnd;
using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum MissionType
{
    none = -1,
    daily,
    week,
    month
}

public class Quest_info_YG
{
    //mission
    public List<int> missions = new List<int>();
    public List<Mission_userdata> userdata = new List<Mission_userdata>();//미션관련 데이터

    //challenge

    public Quest_info_YG() { }
    public Quest_info_YG(JsonData json) //데이터 있을때
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["userdata"])
            {
                Mission_userdata newdata = new Mission_userdata(data);
                missions.Add(newdata.mission_id);
                userdata.Add(newdata);
            }
        }
        else
        {
            Debug.Log("데이터 없음");
        }
    }

    public void Init_info_data() //회원가입
    {
        for (int i = 0; i < 3; i++)
        {
            Random(MissionType.daily);
        }

        for (int i = 0; i < 2; i++)
        {
            Random(MissionType.week);
        }

        for (int i = 0; i < 1; i++)
        {
            Random(MissionType.month);
        }
    }

    private void Random(MissionType type)
    {
        Mission_userdata data = new Mission_userdata(type);

        int min = 0;
        int max = 0;

        switch (type)
        {
            case MissionType.daily:
                min = 1;
                max = 13;
                break;
            case MissionType.week:
                min = 13;
                max = 21;
                break;
            case MissionType.month:
                min = 21;
                max = 25;
                break;
            default:
                break;
        }

        while (true)
        {
            data.mission_id = UnityEngine.Random.Range(min, max);

            if (!missions.Contains(data.mission_id))
            {
                userdata.Add(data);
                missions.Add(data.mission_id);
                break;
            }
        }
    }
}


public class Mission : Quest
{
    //차트
    public int mission_id;
    public MissionType mission_type;
    public CriterionType criterion_type;
    public DateTime reset_time;
    public int reward_ark;
    public int reward_gold;

    //유저 데이터
    //public Mission_userdata userdata;

    public Mission(JsonData jsonData)
    {
        mission_id = int.Parse(jsonData["mission_id"].ToString());
        mission_type = (MissionType)int.Parse(jsonData["type"].ToString());
        criterion_type = (CriterionType)int.Parse(jsonData["type"].ToString());

        goal = int.Parse(jsonData["goal"].ToString());
        reward_ark = int.Parse(jsonData["reward_ark"].ToString());
        reward_gold = int.Parse(jsonData["reward_gold"].ToString());

        //userdata = BackendGameData_JGD.userData.mission_Userdatas[index];

        title = jsonData["title"].ToString();
        contents = jsonData["contents"].ToString();
    }

    public void Reset()
    {
        //userdata.is_clear = false;
        reset_time = DateTime.Now;
    }

    //public bool Check_clear()
    //{
    //    if (userdata.criterion >= goal)
    //    {
    //        userdata.is_clear = true;
    //        return true;
    //    }
    //    return false;
    //}

    private void Reset_mission()
    {
        TimeSpan difference = DateTime.Now.Date.Subtract(reset_time.Date);
        switch (mission_type)
        {
            case MissionType.daily:
                if (difference.Days >= 1 || (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)) //현재 - 초기화일자가 1일 이상 || 자정 12시 
                {
                    Reset();
                }
                break;

            case MissionType.week:
                if (difference.Days >= 7)
                {
                    Reset();
                }
                break;
            case MissionType.month:
                if (true)//월간퀘 초기화 어케하냐 ㄹㅇ
                {
                    Reset();
                }
                break;
            default:
                break;
        }
    }
    /*
    private void Reset_mission() //일간,주간,월간 미션은 자정,매주,매월 첫 월요일 초기화
    {
        switch (data.type)
        {
            case MissionType.daily:
                if (Check_midnight())//자정 12시인지 확인
                {
                    data.is_clear = false;//is_clear 초기화
                }
                break;
            case MissionType.week:
                if (Check_monday() && Check_midnight())//월요일인지,12시인지 확인
                {
                    data.is_clear = false;//is_clear 초기화
                }
                break;
            case MissionType.month:
                if (Check_monday() && Check_midnight() && Check_firstweek())//오늘이 월요일인지, 자정 12시인지, 첫째주인지 확인
                {
                    data.is_clear = false;//is_clear 초기화
                }
                break;
            default:
                break;
        }
    }
    private bool Check_monday()
    {
        if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
        {
            return true;
        }
        return false;
    }

    private bool Check_midnight()
    {
        if (DateTime.Today.Hour == 0 && DateTime.Today.Minute == 0)
        {
            return true;
        }
        return false;
    }

    private bool Check_firstweek()
    {
        DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        DayOfWeek firstDayOfWeek = DayOfWeek.Monday; // 월요일로 설정

        int diff = (7 + (firstDayOfMonth.DayOfWeek - firstDayOfWeek)) % 7;
        DateTime firstMonday = firstDayOfMonth.AddDays(-1 * diff);
        int currentWeekOfMonth = (DateTime.Today.Day + firstMonday.Day - 1) / 7 + 1;

        if (currentWeekOfMonth == 1) // 이번 달의 첫 번째 주
        {
            return true;
        }
        return false;
    }
     */

}
public enum CriterionType
{
    none = -1,
    stage_clear,//스테이지 클리어 횟수
    proxy_harvesting, //대리수확 횟수
    redstar,//스테이지 별 획득 횟수
    galaxy_clear, //은하 클리어 횟수
    character_levelup, //캐릭터 레벨업 횟수
    alphabet, //알파벳 완성횟수(= 하우징오브젝트 획득)
    starnest,//별둥지 업그레이드 횟수
    friend //친구 인원 수
}

public class Mission_userdata
{
    public int mission_id;
    public MissionType mission_type;
    public CriterionType criterion_type;

    public DateTime reset_time;

    public bool is_clear;
    public bool get_rewarded;
    public bool is_accept;//수락했는지 안했는지
    public int criterion //현재 수치
    {
        get
        {
            return criterion_;
        }

        set
        {
            criterion_ = value;
            if (!is_accept)
            {
                criterion_ = 0;
            }
            Debug.Log(criterion_);
        }
    }
    private int criterion_;

    public Mission_userdata(JsonData jsonData)
    {
        mission_id = int.Parse(jsonData["mission_id"].ToString());
        mission_type = (MissionType)int.Parse(jsonData["mission_type"].ToString());
        criterion_type = (CriterionType)int.Parse(jsonData["criterion_type"].ToString());

        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        is_accept = bool.Parse(jsonData["is_accept"].ToString());
        get_rewarded = bool.Parse(jsonData["get_rewarded"].ToString());

        reset_time = Convert.ToDateTime(jsonData["reset_time"].ToString());
        criterion_ = int.Parse(jsonData["criterion"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());
    }

    public Mission_userdata()
    {
        is_clear = false;
        is_accept = false;
        get_rewarded = false;

        reset_time = DateTime.Now;
        criterion = 0;
        criterion_ = 0;
    }

    public Mission_userdata(MissionType type)
    {
        mission_type = type;
        criterion_type = CriterionType.none;

        is_clear = false;
        is_accept = false;
        get_rewarded = false;

        reset_time = DateTime.Now;
        criterion = 0;
        criterion_ = 0;
    }


    public void Data_update()
    {
        //데이터에 넣기
        Param param = new Param();
        param.Add("quest_Info", BackendGameData_JGD.userData.quest_Info);

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
