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
    private List<AudioClip> StageEnd = new List<AudioClip>();
    private List<AudioClip> WordList = new List<AudioClip>();


    private void Awake()
    {
        ClearData.Clear();
        Debug.Log("아아");
    }
    private void Start()
    {
        Score = 0;
        data = BackendChart_JGD.chartData.StageClear_list[Stage];
        Stage = data.Stage;/////
        StageClearUI.SetActive(false);
        NextClearUI.SetActive(false);
        string[] Clearwords = data.StageWord.Split(',');
        for (int i = 0; i < Clearwords.Length; i++)
        {
            ClearData.Add(Clearwords[i]);
        }
        //Time.timeScale = 1.0f;
        //
        switch (data.Theme) //나중에 Theme로 바꾸기
        {
            case 1:
                StageEnd = AudioManager.instance.Theme01;
                break;
            case 2:
                StageEnd = AudioManager.instance.Theme02;
                break;
            case 3:
                StageEnd = AudioManager.instance.Theme03;
                break;
            case 4:
                StageEnd = AudioManager.instance.Theme04;
                break;
            case 5:
                StageEnd = AudioManager.instance.Theme05;
                break;
            default:
                break;
        }
        switch (data.Stage)
        {
            case 1:
                WordList.Add(StageEnd[0]);
                WordList.Add(StageEnd[1]);
                WordList.Add(StageEnd[2]);
                break;
            case 2:
                WordList.Add(StageEnd[3]);
                WordList.Add(StageEnd[4]);
                WordList.Add(StageEnd[5]);
                break;
            case 3:
                WordList.Add(StageEnd[6]);
                WordList.Add(StageEnd[7]);
                WordList.Add(StageEnd[8]);
                break;
            case 4:
                WordList.Add(StageEnd[9]);
                WordList.Add(StageEnd[10]);
                WordList.Add(StageEnd[11]);
                break;
            case 5:
                WordList.Add(StageEnd[12]);
                WordList.Add(StageEnd[13]);
                WordList.Add(StageEnd[14]);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.SFX_stage_clear();
            Star_info stage_data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[LevelSelectMenuManager_JGD.GalaxyLevel].star_Info_list[LevelSelectMenuManager_JGD.currLevel];
            //클리어 데이터 전송
            stage_data.is_clear = true;

            var Player = collision.GetComponent<Player_Controll_JGD>();

            if (ClearData.Count == Player.Alphabet.Count)
            {
                if (stage_data.get_housing == false)
                {
                    BackendGameData_JGD.userData.house_inventory.Add(data.HousingItmeID, 1);
                    stage_data.get_housing = true;
                    Debug.Log("아이템 지급완료");
                    Debug.Log(data.HousingItmeID);
                }
            }
            MoneyManager.instance.Get_Money(gold_: Player.PlayerScore * 10);
            //TXT UI적용
            Debug.Log("와난");
            StageClearUI.SetActive(true);
            StageStar.text = data.Allstar.ToString();
            MyStar.text = Player.PlayerScore.ToString();
            Star_2.text = $"X {data.Star_2.ToString()}";
            Star_3.text = $"X {data.Star_3.ToString()}";
            Gold.text = $"+ {Player.PlayerScore * 10} ";

            //별 생성 
            PlayerStar[0].sprite = ClearStar;
            MissionClearStar[0].sprite = ClearStar;
            StarCount = 1;
            if (data.Star_2 <= Player.PlayerScore)
            {
                PlayerStar[1].sprite = ClearStar;
                MissionClearStar[1].sprite = ClearStar;
                StarCount = 2;
            }
            if (data.Star_3 <= Player.PlayerScore)
            {
                PlayerStar[2].sprite = ClearStar;
                MissionClearStar[2].sprite = ClearStar;
                StarCount = 3;
            }
            if (stage_data.star <= StarCount)
            {
                stage_data.star = StarCount;
            }
            Time.timeScale = 0;
        }
        Data_update();
    }

    //새로운 별 생성 애니메이션
    public void OneStar()
    {
        if (StarCount == 1)
        {
            
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
        param.Add("house_inventory", BackendGameData_JGD.userData.house_inventory);
        param.Add("mission_Userdatas", BackendGameData_JGD.userData.mission_Userdatas);
        param.Add("challenge_Userdatas", BackendGameData_JGD.userData.challenge_Userdatas);

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
    public void Speak_EnglishWord()
    {
        if (AudioManager.instance.isPlaying(false))
        {
            AudioManager.instance.SFX_AudioSource.clip = WordList[0];
            AudioManager.instance.SFX_AudioSource.Play();
        }
    }
    public void Speak_KoreanWord()
    {
        if (AudioManager.instance.isPlaying(false))
        {
            AudioManager.instance.SFX_AudioSource.clip = WordList[1];
            AudioManager.instance.SFX_AudioSource.Play();
        }
    }
    public void Speak_Sentence()
    {
        if (AudioManager.instance.isPlaying(false))
        {
            AudioManager.instance.SFX_AudioSource.clip = WordList[2];
            AudioManager.instance.SFX_AudioSource.Play();
        }
    }
}
