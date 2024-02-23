using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<Image> PlayerStar;
    [SerializeField] private List<Image> MissionClearStar;
    [SerializeField] private Sprite ClearStar;
    [Header("Mission")]
    [SerializeField] private TMP_Text Star_2;
    [SerializeField] private TMP_Text Star_3;
    [Header("result")]
    [SerializeField] private TMP_Text StageStar;
    [SerializeField] private TMP_Text MyStar;
    [Header("Reward")]
    [SerializeField] private TMP_Text Gold;
    [Header("")]
    [Header("ResultUI_2")]
    [SerializeField] private GameObject NextClearUI;
    [SerializeField] private TMP_Text E_word;
    [SerializeField] private TMP_Text K_word;
    [SerializeField] private TMP_Text Sentence;
    [SerializeField]List<GameObject> Stageword = new List<GameObject>();



    private void Awake()
    {
        Stage = LevelSelectMenuManager_JGD.currLevel;
        ClearData.Clear();
        Debug.Log("아아");
    }
    private void Start()
    {
        Score = 0;
        data = BackendChart_JGD.chartData.StageClear_list[Stage];
        StageClearUI.SetActive(false);
        NextClearUI.SetActive(false);
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
            //for (int i = 0; i < ClearData.Count; i++)
            //{
            //    if (ClearData[i] == Player.Alphabet[i] && Player.Alphabet[i] != null)
            //    {
            //        count++;
            //    }
            //}
            Time.timeScale = 0;
            //if (count == ClearData.Count)
            //{
            //    // 아이템 지급
            //    Debug.Log("아이템 지급");
            //}
            if (ClearData.Count == Player.Alphabet.Count)
            {
                Debug.Log("아이템 지급");
            }
            Debug.Log("와난");
            StageClearUI.SetActive(true);
            StageStar.text = data.Allstar.ToString();
            MyStar.text = Player.PlayerScore.ToString();
            Star_2.text = $"X {data.Star_2.ToString()}";
            Star_3.text = $"X {data.Star_3.ToString()}";
            Gold.text = $"+ {Player.PlayerScore * 10} ";


            PlayerStar[0].sprite = ClearStar;
            MissionClearStar[0].sprite = ClearStar;

            if (data.Star_2 <= Player.PlayerScore)
            {
                PlayerStar[1].sprite = ClearStar;
                MissionClearStar[1].sprite = ClearStar;
            }
            if(data.Star_3 < Player.PlayerScore)
            {
                PlayerStar[2].sprite = ClearStar;
                MissionClearStar[2].sprite = ClearStar;
            }
            

        }
    }


    //빠튼
    public void NextClearScene()
    {
        StageClearUI.SetActive(false);
        NextClearUI.SetActive(true);
        string nono = string.Join("", ClearData);
        E_word.text = nono;
        K_word.text = data.Kword;
        Sentence.text = data.Sentence;
        for (int i = 0; i < ClearData.Count; i++)
        {
            int Sprite = (int)Enum.Parse(typeof(item_ID), ClearData[i]);
            Debug.Log(Sprite);
            Stageword[i].GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(4000+Sprite);
        }
        

    }

}
