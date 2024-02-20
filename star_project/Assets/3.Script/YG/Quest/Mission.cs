using System;

public class Mission : Quest
{
    public MissionData data;
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

    private void Reset_mission()
    {
        TimeSpan difference = DateTime.Now.Date.Subtract(data.reset_time.Date);
        switch (data.type)
        {
            case MissionType.daily:
                if (difference.Days >= 1 || (DateTime.Now.Hour ==0 && DateTime.Now.Minute == 0)) //현재 - 초기화일자가 1일 이상 || 자정 12시 
                {
                    data.Reset();
                }
                break;

            case MissionType.week:
                if (difference.Days >= 7)
                {
                    data.Reset();
                }
                break;
            case MissionType.month:
                if (true)//월간퀘 초기화 어케하냐 ㄹㅇ
                {
                    data.Reset();
                }
                break;
            default:
                break;
        }
    }
}

public enum MissionType
{
    none = -1,
    daily,
    week,
    month
}

public class MissionData
{
    public MissionType type;
    public DateTime reset_time;
    public bool is_clear;
    public bool is_accept;
    public int criterion
    {
        get
        {
            return criterion_;
        }
        set
        {
            if (is_accept)
            {
                criterion_ = value;
                //return value;
            }
        }
    }//현재
    private int criterion_;
    public int goal; //목표

    public int reward_ark;
    public int reward_gold;

    //UI
    public string title;
    public string contents;

    public void Reset()
    {
        is_clear = false;
        reset_time = DateTime.Now;
    }

    public MissionData()
    {
        
    }
}
