using BackEnd;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class ChartData
{
    public List<Character> character_list = new List<Character>();
    public List<Item> item_list = new List<Item>();
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

        Character("108792");
        Item("108827");
    }

    public void Character(string chartId)
    {
        //��Ʈ ���� ��������
        Debug.Log($"{chartId}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(chartId);

        //��Ʈ ȣ�� �������� Ȯ��
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}�� ��Ʈ�� �ҷ����� ��, ������ �߻��Ͽ����ϴ�.: " + bro);
            return;
        }
        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);

        //��Ʈ ���� ����

        chartData = new ChartData();

        foreach (JsonData gameData in bro.FlattenRows())
        {
           chartData.character_list.Add(new Character(gameData));
        }
    }

    public void Item(string chartId)
    {
        //��Ʈ ���� ��������
        Debug.Log($"{chartId}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(chartId);

        //��Ʈ ȣ�� �������� Ȯ��
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}�� ��Ʈ�� �ҷ����� ��, ������ �߻��Ͽ����ϴ�.: " + bro);
            return;
        }
        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);

        //��Ʈ ���� ����
        foreach (JsonData gameData in bro.FlattenRows())
        {
            chartData.item_list.Add(new Item(gameData));
        }
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

