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
        //차트 정보 가져오기
        Debug.Log($"{chartId}의 차트 불러오기를 요청합니다.");
        var bro = Backend.Chart.GetChartContents(chartId);

        //차트 호출 성공여부 확인
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}의 차트를 불러오는 중, 에러가 발생하였습니다.: " + bro);
            return;
        }
        Debug.Log("차트 불러오기에 성공했습니다. : " + bro);

        //차트 내용 저장

        chartData = new ChartData();

        foreach (JsonData gameData in bro.FlattenRows())
        {
           chartData.character_list.Add(new Character(gameData));
        }
    }

    public void Item(string chartId)
    {
        //차트 정보 가져오기
        Debug.Log($"{chartId}의 차트 불러오기를 요청합니다.");
        var bro = Backend.Chart.GetChartContents(chartId);

        //차트 호출 성공여부 확인
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}의 차트를 불러오는 중, 에러가 발생하였습니다.: " + bro);
            return;
        }
        Debug.Log("차트 불러오기에 성공했습니다. : " + bro);

        //차트 내용 저장
        foreach (JsonData gameData in bro.FlattenRows())
        {
            chartData.item_list.Add(new Item(gameData));
        }
    }
    #region 예시
    public void ChartGet(string chartId) //예시
    {
        //차트 정보 가져오기
        Debug.Log($"{chartId}의 차트 불러오기를 요청합니다.");
        var bro = Backend.Chart.GetChartContents(chartId);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{chartId}의 차트를 불러오는 중, 에러가 발생하였습니다.: " + bro);
            return;
        }

        Debug.Log("차트 불러오기에 성공했습니다. : " + bro);

        #region 차트 내용 출력방법
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

