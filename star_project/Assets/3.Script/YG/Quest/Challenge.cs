using BackEnd;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : Quest
{
    //���� ������
    Challenge_userdata userdata;
    public housing_itemID housing_ItemID; //�������� ���� �Ͽ�¡ ������ id
}

public class Challenge_userdata
{
    public bool is_clear;
    public int CP; //challenge point
    public bool get_rewarded;
    public int criterion; //���� ��ġ
    public Dictionary<int, Challenge_info> data_dic;

    public Challenge_userdata(JsonData jsonData)
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        CP = int.Parse(jsonData["CP"].ToString());
        get_rewarded = bool.Parse(jsonData["get_rewarded"].ToString());
        criterion = int.Parse(jsonData["criterion"].ToString());

        for (int i = 0; i < 9; i++) //9 = ���� ����
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

        for (int i = 0; i < 9; i++) //9 = ���� ����
        {
            data_dic.Add(i,new Challenge_info());
        }
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
