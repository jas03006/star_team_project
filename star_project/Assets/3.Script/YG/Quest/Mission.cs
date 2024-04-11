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
    public List<Mission_userdata> mission_userdata = new List<Mission_userdata>();//미션관련 데이터

    //challenge
    public List<challenge_state> challenge_states = new List<challenge_state>(); //cp 보상
    public Dictionary<Clear_type, int> challenge_dic = new Dictionary<Clear_type, int>();
    //public List<Challenge_userdata> challenge_userdata = new List<Challenge_userdata>();//퀘스트 관련 데이터

    public Quest_info_YG() { }
    public Quest_info_YG(JsonData json) //데이터 있을때
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["mission_userdata"])
            {
                Mission_userdata newdata = new Mission_userdata(data);
                missions.Add(newdata.mission_id);
                mission_userdata.Add(newdata);
            }

            foreach (JsonData data in json["challenge_states"])
            {
                challenge_states.Add((challenge_state)int.Parse(data.ToString()));
            }

            foreach (string key in json["challenge_dic"].Keys)
            {
                Clear_type clearType = (Clear_type)Enum.Parse(typeof(Clear_type), key);
                int value = int.Parse(json["challenge_dic"][key].ToString());
                challenge_dic.Add(clearType, value);
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
            Random_mission(MissionType.daily);
        }

        for (int i = 0; i < 2; i++)
        {
            Random_mission(MissionType.week);
        }

        for (int i = 0; i < 1; i++)
        {
            Random_mission(MissionType.month);
        }

        //challenge
        for (int i = 0; i < 5; i++)
        {
            challenge_states.Add(challenge_state.incomplete);
        }

        foreach (Clear_type clearType in Enum.GetValues(typeof(Clear_type)))
        {
            if (clearType == Clear_type.first_connection || clearType == Clear_type.clear_tutorial)
            {
                challenge_dic.Add(clearType, 1);
            }
            else
            {
                challenge_dic.Add(clearType, 0);
            }
        }
    }

    public void Random_mission(MissionType type)
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
                mission_userdata.Add(data);
                missions.Add(data.mission_id);
                break;
            }
        }
    }
}

[Serializable]
public class Mission : Quest
{
    //차트
    public int mission_id;
    public MissionType mission_type;
    public Criterion_type criterion_type;
    public DateTime reset_time;
    public int reward_ark;
    public int reward_gold;

    //유저 데이터
    //public Mission_userdata userdata;

    public Mission(JsonData jsonData)
    {
        mission_id = int.Parse(jsonData["mission_id"].ToString());
        mission_type = (MissionType)int.Parse(jsonData["type"].ToString());
        criterion_type = (Criterion_type)int.Parse(jsonData["criterion_type"].ToString());

        goal = int.Parse(jsonData["goal"].ToString());
        reward_ark = int.Parse(jsonData["reward_ark"].ToString());
        reward_gold = int.Parse(jsonData["reward_gold"].ToString());

        //userdata = BackendGameData_JGD.userData.mission_Userdatas[index];

        title = jsonData["title"].ToString();
        contents = jsonData["contents"].ToString();
        sub_text = jsonData["sub_text"].ToString();
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
                    BackendGameData_JGD.userData.quest_Info.Random_mission(mission_type);
                    Reset();
                }
                break;

            case MissionType.week:
                if (difference.Days >= 7)
                {
                    BackendGameData_JGD.userData.quest_Info.Random_mission(mission_type);
                    Reset();
                }
                break;
            case MissionType.month:
                if (true)//월간퀘 초기화 어케하냐 ㄹㅇ
                {
                    BackendGameData_JGD.userData.quest_Info.Random_mission(mission_type);
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
public enum Criterion_type
{
    none = -1,
    stage_clear = 0,//스테이지 클리어 횟수
    proxy_harvesting = 1, //대리수확 횟수
    redstar = 2,//스테이지 별 획득 횟수
    galaxy_clear = 3, //은하 클리어 횟수
    character_levelup = 4, //캐릭터 레벨업 횟수
    alphabet = 5, //알파벳 완성횟수(= 하우징오브젝트 획득)
    starnest = 6,//별둥지 업그레이드 횟수
    friend = 7 //친구 신청 수 //고쳐야함
}

public class Mission_userdata
{
    public int mission_id;
    public MissionType mission_type;
    public Criterion_type criterion_type;

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
            if (is_clear)
            {
                return;
            }
            if (!is_accept)
            {
                criterion_ = 0;
            }
        }
    }
    private int criterion_;

    public Mission_userdata(JsonData jsonData)
    {
        mission_id = int.Parse(jsonData["mission_id"].ToString());
        mission_type = (MissionType)int.Parse(jsonData["mission_type"].ToString());

        //criterion_type = (Criterion_type)int.Parse(BackendChart_JGD.chartData.mission_list[mission_id-1].ToString());

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
        criterion_type = Criterion_type.none;

        is_clear = false;
        is_accept = false;
        get_rewarded = false;

        reset_time = DateTime.Now;
        criterion = 0;
        criterion_ = 0;
    }
}
