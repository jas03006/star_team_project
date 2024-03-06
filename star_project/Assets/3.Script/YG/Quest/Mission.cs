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
    public List<Mission_userdata> userdata = new List<Mission_userdata>();//�̼ǰ��� ������

    //challenge

    public Quest_info_YG() { }
    public Quest_info_YG(JsonData json) //������ ������
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
            Debug.Log("������ ����");
        }
    }

    public void Init_info_data() //ȸ������
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
    //��Ʈ
    public int mission_id;
    public MissionType mission_type;
    public CriterionType criterion_type;
    public DateTime reset_time;
    public int reward_ark;
    public int reward_gold;

    //���� ������
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
public enum CriterionType
{
    none = -1,
    stage_clear,//�������� Ŭ���� Ƚ��
    proxy_harvesting, //�븮��Ȯ Ƚ��
    redstar,//�������� �� ȹ�� Ƚ��
    galaxy_clear, //���� Ŭ���� Ƚ��
    character_levelup, //ĳ���� ������ Ƚ��
    alphabet, //���ĺ� �ϼ�Ƚ��(= �Ͽ�¡������Ʈ ȹ��)
    starnest,//������ ���׷��̵� Ƚ��
    friend //ģ�� �ο� ��
}

public class Mission_userdata
{
    public int mission_id;
    public MissionType mission_type;
    public CriterionType criterion_type;

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
        //�����Ϳ� �ֱ�
        Param param = new Param();
        param.Add("quest_Info", BackendGameData_JGD.userData.quest_Info);

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
