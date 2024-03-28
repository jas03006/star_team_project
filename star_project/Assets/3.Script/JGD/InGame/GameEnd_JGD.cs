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
    public StageClear data;
    [SerializeField] public List<string> ClearData = new List<string>();
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
    [SerializeField] private GameObject Reward;
    [SerializeField] private Image Reward_Image;
    [SerializeField] private TMP_Text RewardTxt;

    [Header("ResultUI_2")]
    [SerializeField] private GameObject NextClearUI;
    [SerializeField] private Image Reward_Image2;
    [SerializeField] private TMP_Text E_word;
    [SerializeField] private TMP_Text K_word;
    [SerializeField] private TMP_Text Sentence;
    [SerializeField] List<GameObject> Stageword = new List<GameObject>();
    public int StarCount = 0;
    private List<AudioClip> StageEnd = new List<AudioClip>();
    private List<AudioClip> WordList = new List<AudioClip>();
    Star_info stage_data;
    bool getting_house_ob = false;
    int GoldSave;

    [SerializeField] private ObjectsDatabaseSO databaseSO;
    private void Awake()
    {
        Stage = LevelSelectMenuManager_JGD.currLevel + (LevelSelectMenuManager_JGD.GalaxyLevel * 5);
        data = BackendChart_JGD.chartData.StageClear_list[Stage];
        ClearData.Clear();
        string[] Clearwords = data.StageWord.Split(',');
        for (int i = 0; i < Clearwords.Length; i++)
        {
            ClearData.Add(Clearwords[i]);
        }
        Debug.Log("아아");
    }
    private void Start()
    {
        Score = 0;
        Stage = data.Stage;/////
        StageClearUI.SetActive(false);
        NextClearUI.SetActive(false);
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
        switch (data.Stage - ((data.Theme - 1)*5))
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
            QuestManager.instance.Check_mission(Criterion_type.stage_clear);
            AudioManager.instance.SFX_stage_clear();
            stage_data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[LevelSelectMenuManager_JGD.GalaxyLevel].star_Info_list[LevelSelectMenuManager_JGD.currLevel];
            //클리어 데이터 전송
            stage_data.is_clear = true;

            var Player = collision.GetComponent<Player_Controll_JGD>();
            getting_house_ob = false;
            if (ClearData.Count == Player.Player_Alphabet_Count)
            {
                QuestManager.instance.Check_challenge(Clear_type.make_word);

                if (stage_data.get_housing == false)
                {
                    getting_house_ob = true;
                    //추후 바뀌면 수정
                    //BackendGameData_JGD.userData.house_inventory.Add(data.HousingItmeID, 1);
                    if (data.Theme < 5)
                    {
                        BackendGameData_JGD.userData.house_inventory.Add(data.HousingItmeID, 1);
                        BackendGameData_JGD.userData.Noun_ID_List.Add((noun)data.NounTitle);
                    }
                    else
                    {
                        BackendGameData_JGD.userData.Adjective_ID_List.Add((adjective)data.AdjectiveTitle);
                        BackendGameData_JGD.userData.emozi_List.Add(data.Emoticon);
                    }

                    stage_data.get_housing = true;
                    Debug.Log("아이템 지급완료");
                }

            }
            //TXT UI적용
            Debug.Log("와난");
            StageClearUI.SetActive(true);
            StageStar.text = data.Allstar.ToString();
            MyStar.text = Player.PlayerScore.ToString();
            Star_2.text = $"X {data.Star_2.ToString()}";
            Star_3.text = $"X {data.Star_3.ToString()}";
            for (int i = 0; i < Player.Alphabet.Count; i++)  //알파벳 추가
            {
                if (Player.Alphabet[i] != -1)
                {
                    Stageword[i].gameObject.GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(Player.Alphabet[i] + 4000);
                }
            }

            QuestManager.instance.Check_challenge(Clear_type.get_star, Player.PlayerScore);

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
            GoldSave = (int)(((int)Player.currentHp + Player.PlayerScore) * StarCount * (1 + 0.2f * data.Theme));
            Gold.text = $"+ {GoldSave} ";
            MoneyManager.instance.Get_Money(gold_: GoldSave);
            QuestManager.instance.Check_mission(Criterion_type.redstar);
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
    public void RewardBtn()
    {
        Reward.SetActive(false);
    }
    public void NextClearScene()
    {
        if ((int)data.RewardType == 0)    //차트에 감정인지 아이템인지 체크 like 0과 1로 1은 감정 0은 사물
        {
            Reward_Image2.sprite = SpriteManager.instance.Num2Sprite(data.HousingItmeID);

        }
        else if (data.RewardType == 1)
        {
            Reward_Image2.sprite = SpriteManager.instance.Num2emozi(data.Emoticon);

        }
        StageClearUI.SetActive(false);
        NextClearUI.SetActive(true);
        string nono = string.Join("", ClearData);
        E_word.text = nono;
        K_word.text = data.Kword;
        Sentence.text = data.Sentence;
        if (getting_house_ob)
        {
            Reward_Image.sprite = Reward_Image2.sprite;
            Reward.SetActive(true);
            RewardTxt.text = nono;
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
