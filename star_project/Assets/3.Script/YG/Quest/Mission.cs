using BackEnd;
using LitJson;
using System;
using UnityEngine;


public enum MissionType
{
    none = -1,
    daily,
    week,
    month
}

public class Mission
{
    //��Ʈ
    public MissionType type;
    public DateTime reset_time;
    public int goal; //��ǥ ��ġ
    public int reward_ark;
    public int reward_gold;

    //���� ������
    public Mission_userdata userdata;

    //UI
    public string title;
    public string contents;

    public Mission(JsonData jsonData, int index)
    {
        type = (MissionType)int.Parse(jsonData["type"].ToString());

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

    public bool Check_clear()
    {
        if (userdata.criterion >= goal)
        {
            userdata.is_clear = true;
            return true;
        }
        return false;
    }

    private void Reset_mission()
    {
        TimeSpan difference = DateTime.Now.Date.Subtract(reset_time.Date);
        switch (type)
        {
            case MissionType.daily:
                if (difference.Days >= 1 || (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)) //���� - �ʱ�ȭ���ڰ� 1�� �̻� || ���� 12�� 
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
                if (true)//������ �ʱ�ȭ �����ϳ� ����
                {
                    Reset();
                }
                break;
            default:
                break;
        }
    }
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

}

public class Mission_userdata
{
    public DateTime reset_time;
    public bool is_clear;
    public bool get_rewarded;
    public bool is_accept;//�����ߴ��� ���ߴ���
    public int criterion //���� ��ġ
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
}
