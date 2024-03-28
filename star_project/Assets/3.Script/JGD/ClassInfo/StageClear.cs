using LitJson;
using System;
using UnityEngine;
[Serializable]
public class StageClear : MonoBehaviour
{
    public int Stage;
    public int Theme;
    public int Star_2 = 0; // 스코어에 따른 별갯수
    public int Star_3 = 0;
    public int Allstar = 0;
    public int Emoticon = 0;
    public int RewardType = 0;

    public string Kword;
    public string Sentence;
    public string StageWord = string.Empty;  // 스테이지 단어
    public int NounTitle;
    public int AdjectiveTitle;
    public housing_itemID HousingItmeID = housing_itemID.none;

    public StageClear()
    {

    }
    public StageClear(JsonData gameData)
    {
        Stage = int.Parse(gameData["Stage"]?.ToString());
        Theme = int.Parse(gameData["Theme"]?.ToString());
        Star_2 = int.Parse(gameData["Star_2"]?.ToString());
        Star_3 = int.Parse(gameData["Star_3"]?.ToString());
        Allstar = int.Parse(gameData["Allstar"]?.ToString());
        //RewardType = int.Parse(gameData["RewardType"]?.ToString());
        StageWord = gameData["StageWord"]?.ToString();
        Kword = gameData["Kword"]?.ToString();
        Sentence = gameData["Sentence"]?.ToString();
        HousingItmeID = (housing_itemID) int.Parse(gameData["HousingItmeID"]?.ToString());
        if (Theme < 5)
        {
            NounTitle = int.Parse(gameData["NounTitle"]?.ToString());
        }
        else if (Theme >= 5)
        {
            AdjectiveTitle = int.Parse(gameData["AdjectiveTitle"]?.ToString());
            Emoticon = int.Parse(gameData["Emoticon"]?.ToString());
        }
    }

}