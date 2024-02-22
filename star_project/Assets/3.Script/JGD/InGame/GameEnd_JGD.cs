using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd_JGD : MonoBehaviour
{
    int Stage;
    string Word;
    private int Score;
    private StageClear data;
    [SerializeField] private List<string> ClearData = new List<string>();
    [Header("ClearUI")]
    [SerializeField] private GameObject StageClearUI;
    [SerializeField] private TMP_Text PlayerScore;
    [SerializeField] private TMP_Text PlayerStar;   //스테이지 클리어 이후 별이 띄워지는 상황이면 바꾸기

    private void Awake()
    {
        Stage = LevelSelectMenuManager_JGD.currLevel;
        ClearData.Clear();
        StageClearUI.SetActive(false);
        Debug.Log("아아");
    }
    private void Start()
    {
        Score = 0;
        data = BackendChart_JGD.chartData.StageClear_list[Stage];
        string[] Clearwords =  data.StageWord.Split(',');
        for (int i = 0; i < Clearwords.Length; i++)
        {
            ClearData.Add(Clearwords[i]);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int count = 0;
            var Player = collision.GetComponent<Player_Controll_JGD>();
            for (int i = 0; i < ClearData.Count; i++)
            {
                if (ClearData[i] == Player.Alphabet[i])
                {
                    count++;
                }
            }
            Time.timeScale = 0;
            if (count == ClearData.Count)
            {
                // 아이템 지급
                Debug.Log("아이템 지급");
            }
            Debug.Log("와난");
            StageClearUI.SetActive(true);
            PlayerScore.text = Player.PlayerScore.ToString();
            if (Player.PlayerScore <= 0)
            {
                PlayerStar.text = 0.ToString();
            }
            else if (0 < Player.PlayerScore && Player.PlayerScore < data.Star_2)  //이부분 별 띄워주는거면 내용바꾸기
            {
                PlayerStar.text = 1.ToString();
            }
            else if (data.Star_2 <= Player.PlayerScore && Player.PlayerScore < data.Star_3)
            {
                PlayerStar.text = 2.ToString();
            }
            else
            {
                PlayerStar.text = 3.ToString();
            }
            

        }
    }

}
