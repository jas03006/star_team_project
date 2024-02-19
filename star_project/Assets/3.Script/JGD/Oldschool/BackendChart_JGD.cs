using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartData
{
    public List<Character> character_list = new List<Character>();
    public List<Item> item_list = new List<Item>();
    public List<Character_amount> Characteramount_list = new List<Character_amount>();
}

public class BackendChart_JGD : MonoBehaviour
{
    private static BackendChart_JGD instance = null;
    public static ChartData chartData;

    public static BackendChart_JGD Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BackendChart_JGD();
            }
            return instance;
        }
    }

    public void ChartDataGet()
    {
        chartData = new ChartData();

        Character("108885");
        Item("108827");
        Character_amount("108882");
    }


    public void Character(string chartId)
    {
        //��Ʈ ���� ����
        foreach (JsonData gameData in Get_data(chartId).FlattenRows())
        {
           chartData.character_list.Add(new Character(gameData));
        }
    }

    public void Item(string chartId)
    {
        //��Ʈ ���� ����
        foreach (JsonData gameData in Get_data(chartId).FlattenRows())
        {
            chartData.item_list.Add(new Item(gameData));
        }
    }

    public void Character_amount(string chartid)
    {
        //��Ʈ ���� ����
        foreach (JsonData gameData in Get_data(chartid).FlattenRows())
        {
            chartData.Characteramount_list.Add(new Character_amount(gameData));
        }
    }

    public BackendReturnObject Get_data(string str)
    {
        //��Ʈ ���� ��������
        Debug.Log($"{str}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(str);

        //��Ʈ ȣ�� �������� Ȯ��
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{str}�� ��Ʈ�� �ҷ����� ��, ������ �߻��Ͽ����ϴ�.: " + bro);
            return null;
        }
        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);
        return bro;
    }
    #region ����
    public void ChartGet(string chartId) //����
    {
        //��Ʈ ���� ��������
        Debug.Log($"{chartId}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(chartId);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}�� ��Ʈ�� �ҷ����� ��, ������ �߻��Ͽ����ϴ�.: " + bro);
            return;
        }

        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);

        #region ��Ʈ ���� ��¹��
        /*
    foreach (JsonData gameData in bro.FlattenRows())
    {
    StringBuilder content = new StringBuilder();

    content.AppendLine("itemID : " + int.Parse(gameData["itemId"].ToString()));
    content.AppendLine("itemName : " + gameData["itemName"].ToString());
    content.AppendLine("itemType : " + gameData["itemType"].ToString());
    content.AppendLine("itemID : " + long.Parse(gameData["itemPower"].ToString()));
    content.AppendLine("itemInfo : " + gameData["itemInfo"].ToString());

    Debug.Log(content.ToString());
}
 */
        #endregion

    }
    #endregion
}

