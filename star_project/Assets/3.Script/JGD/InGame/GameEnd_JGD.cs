using BackEnd;
using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] List<GameObject> Stageword = new List<GameObject>();
    public int StarCount = 0;


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
        string[] Clearwords = data.StageWord.Split(',');
        for (int i = 0; i < Clearwords.Length; i++)
        {
            ClearData.Add(Clearwords[i]);
        }
        Time.timeScale = 1.0f;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.SFX_stage_clear();
            Star_info stage_data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[LevelSelectMenuManager_JGD.GalaxyLevel].star_Info_list[LevelSelectMenuManager_JGD.currLevel];
            //클리어 데이터 전송
            stage_data.is_clear = true;

            //int count = 0;
            var Player = collision.GetComponent<Player_Controll_JGD>();
            //for (int i = 0; i < ClearData.Count; i++)
            //{
            //    if (ClearData[i] == Player.Alphabet[i] && Player.Alphabet[i] != null)
            //    {
            //        count++;
            //    }
            //}
            //if (count == ClearData.Count)
            //{
            //    // 아이템 지급
            //    Debug.Log("아이템 지급");
            //}
            if (ClearData.Count == Player.Alphabet.Count)
            {
                if (stage_data.get_housing == false)
                {
                    BackendGameData_JGD.userData.house_inventory.Add(data.HousingItmeID, 1);
                    stage_data.get_housing = true;
                    Debug.Log("아이템 지급완료");
                }
            }
            MoneyManager.instance.Get_Money(gold_: Player.PlayerScore * 10);

            Debug.Log("와난");
            StageClearUI.SetActive(true);
            StageStar.text = data.Allstar.ToString();
            MyStar.text = Player.PlayerScore.ToString();
            Star_2.text = $"X {data.Star_2.ToString()}";
            Star_3.text = $"X {data.Star_3.ToString()}";
            Gold.text = $"+ {Player.PlayerScore * 10} ";


            PlayerStar[0].sprite = ClearStar;
            MissionClearStar[0].sprite = ClearStar;
            StarCount = 1;
            if (data.Star_2 <= Player.PlayerScore)
            {
                PlayerStar[1].sprite = ClearStar;
                MissionClearStar[1].sprite = ClearStar;
                StarCount = 2;
            }
            if (data.Star_3 < Player.PlayerScore)
            {
                PlayerStar[2].sprite = ClearStar;
                MissionClearStar[2].sprite = ClearStar;
                StarCount = 3;
            }
            stage_data.star = StarCount;
            Time.timeScale = 0;
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
            Stageword[i].GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(4000 + Sprite);
        }


    }

    public void Data_update()
    {
        //데이터에 넣기
        Param param = new Param();
        param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

}
