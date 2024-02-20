using BackEnd;
using LitJson;
using System;


public enum MissionType
{
    none = -1,
    daily,
    week,
    month
}

public class Mission
{
    //차트
    public MissionType type;
    public DateTime reset_time;
    public int goal; //목표 수치
    public int reward_ark;
    public int reward_gold;

    //유저 데이터
    public Mission_userdata userdata;

    //UI
    public string title;
    public string contents;

    public Mission(JsonData jsonData,int index)
    {
        type = (MissionType)int.Parse(jsonData["type"].ToString());
        reset_time = Convert.ToDateTime(jsonData["reset_time"].ToString());
        goal = int.Parse(jsonData["goal"].ToString());
        reward_ark = int.Parse(jsonData["reward_ark"].ToString());
        reward_gold = int.Parse(jsonData["reward_gold"].ToString());

        userdata = BackendGameData_JGD.userData.mission_Userdatas[index];

        title = jsonData["title"].ToString();
        contents = jsonData["contents"].ToString();
    }

    public void Reset()
    {
        userdata.is_clear = false;
        reset_time = DateTime.Now;
    }

    private void Reset_mission()
    {
        TimeSpan difference = DateTime.Now.Date.Subtract(reset_time.Date);
        switch (type)
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

public class Mission_userdata
{
    public bool is_clear;
    public bool is_accept;
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
        }
    }
    private int criterion_;

    public Mission_userdata(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        is_accept = bool.Parse(jsonData["is_accept"].ToString());
        criterion_ = int.Parse(jsonData["criterion"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());
    }
    public Mission_userdata()
    {
        is_clear = false;
        is_accept = false;
        criterion = 0;
        criterion_ = 0;
    }
}
