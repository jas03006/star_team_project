using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� �������� ���� ��Ȳ�� �����ϱ� ���� ���� Ŭ����.
/// 5���� ���ϰ� ������ �� ���Ϻ��� 5���� ���������� ����.
/// ��ӱ��� : Catchingstar_info(��ü�� ���ΰ� �ִ� Ŭ����) -> Galaxy_info(�� ���Ϻ� ������ ����ִ� Ŭ����) -> starinfo(�� �������� �� ������ ����ִ� Ŭ����)
/// </summary>
public class Catchingstar_info
{
    public List<Galaxy_info> galaxy_Info_list = new List<Galaxy_info>();//�� ���� �� ���� ��Ȳ ����

    public Catchingstar_info() //�ű� ȸ�� - ������ ����
    {
        for (int i = 0; i < 5; i++)
        {
            galaxy_Info_list.Add(new Galaxy_info());
        }
    }

    public Catchingstar_info(JsonData jsonData) //���� ȸ�� - ������ �ҷ�����
    {
        foreach (JsonData json in jsonData["galaxy_Info_list"])
        {
            galaxy_Info_list.Add(new Galaxy_info(json));
        }
    }

    public void Data_update() //ĳĪ��Ÿ ������ ������Ʈ
    {
        //�����Ϳ� �ֱ�
        Param param = new Param();
        param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

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

    public int[] Check_stage_progress() //Ŭ���� �� ������ ���������� Ȯ��
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

public enum Galaxy_state //���Ϻ� �̼ǻ���
{
    incomplete = 0,
    can_reward,
    complete
}

public class Galaxy_info //���� ������ ���� Ŭ���� 
{
    public bool is_clear = false; //���� Ŭ���� ����
    public List<Star_info> star_Info_list = new List<Star_info>(); //�� �������� ���� ����
    public List<Galaxy_state> mission_state = new List<Galaxy_state>(); //���� �̼� ���൵

    public Galaxy_info()//�ű� ȸ�� - ������ ����
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

    public Galaxy_info(JsonData jsonData) //���� ȸ�� - ������ �ҷ�����
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

public class Star_info //�������� ������ ���� Ŭ���� 
{
    public bool is_clear; //Ŭ���� ����
    public int star; //���彺Ÿ ����
    public bool get_housing; //�Ͽ�¡ ������ ȹ�� ����

    public Star_info()//�ű� ȸ�� - ������ ����
    {
        is_clear = false;
        star = 0;
        get_housing = false;
    }

    public Star_info(JsonData jsonData)//���� ȸ�� - ������ �ҷ�����
    {
        is_clear = bool.Parse(jsonData["is_clear"].ToString());
        star = int.Parse(jsonData["star"].ToString());
        get_housing = bool.Parse(jsonData["get_housing"].ToString());
    }
}
