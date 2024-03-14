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
    public List<Mission> mission_list = new List<Mission>();
    public List<Challenge> challenge_list = new List<Challenge>();
    public List<StageClear> StageClear_list = new List<StageClear>();
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

        Character("110719");
        Item("110518");
        Character_amount("108882");
        Mission("111372");
        StageClearInfo("111665");
        Challenge("111476");
    }

    public void StageClearInfo(string chartId)
    {
        JsonData data = Get_data(chartId).FlattenRows();
        //차트 내용 저장
        foreach (JsonData gameData in data)
        {
            chartData.StageClear_list.Add(new StageClear(gameData));
        }
    }

    public void Character(string chartId)
    {
        JsonData data = Get_data(chartId).FlattenRows();
        //차트 내용 저장
        foreach (JsonData gameData in data)
        {
           chartData.character_list.Add(new Character(gameData));
        }
    }

    public void Item(string chartId)
    {
        JsonData data = Get_data(chartId).FlattenRows();
        //차트 내용 저장
        foreach (JsonData gameData in data)
        {
            chartData.item_list.Add(new Item(gameData));
        }
    }
    public void Character_amount(string chartid)
    {
        JsonData data = Get_data(chartid).FlattenRows();
        //차트 내용 저장
        foreach (JsonData gameData in data)
        {
            chartData.Characteramount_list.Add(new Character_amount(gameData));
        }
    }

    public void Mission(string chartid)
    {
        JsonData data = Get_data(chartid).FlattenRows();
        //차트 내용 저장

        foreach (JsonData gameData in data)
        {
            chartData.mission_list.Add(new Mission(gameData));
        }
        Debug.Log("debug");
    }

    public void Challenge(string chartid)
    {
        JsonData data = Get_data(chartid).FlattenRows();
        //차트 내용 저장
        for (int i = 0; i < data.Count; i++)
        {
            chartData.challenge_list.Add(new Challenge(data[i], i));
        }
    }

    public BackendReturnObject Get_data(string str)
    {
        //차트 정보 가져오기
        Debug.Log($"{str}의 차트 불러오기를 요청합니다.");
        var bro = Backend.Chart.GetChartContents(str);

        //차트 호출 성공여부 확인
        if (bro.IsSuccess() == false)
        {
            Debug.LogError($"{str}의 차트를 불러오는 중, 에러가 발생하였습니다.: " + bro);
            return null;
        }
        Debug.Log("차트 불러오기에 성공했습니다. : " + bro);
        return bro;
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

