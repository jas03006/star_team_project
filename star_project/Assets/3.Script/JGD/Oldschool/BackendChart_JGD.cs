using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BackEnd;
using System.Text;
public class BackendChart_JGD : MonoBehaviour
{
    private static BackendChart_JGD instance= null;

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

    public void ChartGet(string chartId)
    {
        //��Ʈ ���� ��������
        Debug.Log($"{chartId}�� ��Ʈ �ҷ����⸦ ��û�մϴ�.");
        var bro = Backend.Chart.GetChartContents(chartId);

        if(bro.IsSuccess() == false )
        {
            Debug.LogError($"{chartId}�� ��Ʈ�� �ҷ����� ��, ������ �߻��Ͽ����ϴ�.: " + bro);
            return;
        }
        Debug.Log("��Ʈ �ҷ����⿡ �����߽��ϴ�. : " + bro);
        foreach (LitJson.JsonData gameData in bro.FlattenRows())
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("itemID : " + int.Parse(gameData["itemId"].ToString()));
            content.AppendLine("itemName : " + gameData["itemName"].ToString());
            content.AppendLine("itemType : " + gameData["itemType"].ToString());
            content.AppendLine("itemID : " + long.Parse(gameData["itemPower"].ToString()));
            content.AppendLine("itemInfo : " + gameData["itemInfo"].ToString());

            Debug.Log(content.ToString());
        }



    }
}