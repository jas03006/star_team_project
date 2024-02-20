using System;

public class Mission : Quest
{
    public MissionData data;
    /*
    private void Reset_mission() //�ϰ�,�ְ�,���� �̼��� ����,����,�ſ� ù ������ �ʱ�ȭ
    {
        switch (data.type)
        {
            case MissionType.daily:
                if (Check_midnight())//���� 12������ Ȯ��
                {
                    data.is_clear = false;//is_clear �ʱ�ȭ
                }
                break;
            case MissionType.week:
                if (Check_monday() && Check_midnight())//����������,12������ Ȯ��
                {
                    data.is_clear = false;//is_clear �ʱ�ȭ
                }
                break;
            case MissionType.month:
                if (Check_monday() && Check_midnight() && Check_firstweek())//������ ����������, ���� 12������, ù°������ Ȯ��
                {
                    data.is_clear = false;//is_clear �ʱ�ȭ
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
        DayOfWeek firstDayOfWeek = DayOfWeek.Monday; // �����Ϸ� ����

        int diff = (7 + (firstDayOfMonth.DayOfWeek - firstDayOfWeek)) % 7;
        DateTime firstMonday = firstDayOfMonth.AddDays(-1 * diff);
        int currentWeekOfMonth = (DateTime.Today.Day + firstMonday.Day - 1) / 7 + 1;

        if (currentWeekOfMonth == 1) // �̹� ���� ù ��° ��
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
                if (difference.Days >= 1 || (DateTime.Now.Hour ==0 && DateTime.Now.Minute == 0)) //���� - �ʱ�ȭ���ڰ� 1�� �̻� || ���� 12�� 
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
                if (true)//������ �ʱ�ȭ �����ϳ� ����
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
    }//����
    private int criterion_;
    public int goal; //��ǥ

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
