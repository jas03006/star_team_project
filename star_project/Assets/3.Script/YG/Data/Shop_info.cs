using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Myplanet�� �� Stage������ ĳ���͸� �����ϴ� ��ũ��Ʈ.
/// �� ��ũ��Ʈ�� UI�� ���� ĳ���� ������ ǥ���ϰ�, ĳ���� ���� �� ���׷��̵� ���� ����� ������.
/// </summary>
public class Shop_info //userdata
{
    public List<int> index_list = new List<int>();
    public Shop_info()//�ű� ȸ�� - ������ ����
    {

    }

    public Shop_info(JsonData json)//���� ȸ�� - ������ �ҷ�����
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["index_list"])
            {
                index_list.Add(int.Parse(data.ToString()));
            }
        }
    }
}


