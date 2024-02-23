using LitJson;
using System;
using UnityEngine;
[Serializable]
public class StageClear : MonoBehaviour
{
    public int Stage;
    public int Star_2 = 0; // ���ھ ���� ������
    public int Star_3 = 0;
    public int Allstar = 0;
    public string Kword;
    public string Sentence;
    public string StageWord = string.Empty;  // �������� �ܾ�
    public StageClear()
    {

    }
    public StageClear(JsonData gameData)
    {
        Stage = int.Parse(gameData["Stage"]?.ToString());
        Star_2 = int.Parse(gameData["Star_2"]?.ToString());
        Star_3 = int.Parse(gameData["Star_3"]?.ToString());
        Allstar = int.Parse(gameData["Allstar"].ToString());
        StageWord = gameData["StageWord"]?.ToString();
        Kword = gameData["Kword"]?.ToString();
        Sentence = gameData["Sentence"]?.ToString();
    }

}