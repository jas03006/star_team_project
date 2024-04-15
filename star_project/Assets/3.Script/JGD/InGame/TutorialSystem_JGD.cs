using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum State //튜토리얼 진행상태
{
    Start,
    Item,
    Magnet,
    End
}

public class TutorialSystem_JGD : MonoBehaviour
{
    bool Key_Up = false;
    public bool UI_On = false;
    public int progress = 0;

    //다음으로 넘어가는 시간
    private float NextMentTimmer = 0.7f;
    //================================

    [SerializeField] Tuto_Player_Controll Player;
    [Header("StartUI")]//패널들
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject TouchPanel;
    [SerializeField] GameObject HPPanel;
    [SerializeField] GameObject ItemPanel;
    [SerializeField] GameObject StarPanel;
    [SerializeField] GameObject StarcountPanel;
    [SerializeField] GameObject ResultPanel;
    [SerializeField] GameObject RewardPanel;
    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject englishRewardPanel;
    [SerializeField] GameObject StarinfoPanel;
    [SerializeField] GameObject StudyPanel;
    [SerializeField] GameObject SoundPanel;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject NextResult;

    [Header("ETC")]//멘트 모음 
    [SerializeField] GameObject NextMent_Alarm;
    [SerializeField] List<GameObject> MentList = new List<GameObject>();
    [SerializeField] List<GameObject> Fingers = new List<GameObject>();
    [SerializeField] public Button Itembtn;
    [SerializeField] public Button NextResultBtn;
    [SerializeField] public Button MainBtn;
    State state;

    [SerializeField] GameObject MagnetItem;
    [Header("EndUI")]//보상창
    [SerializeField] GameObject Results_screen;
    [SerializeField] GameObject Results_screen2;
    Press_Any_Key press_any_key;
    Press_Any_Key FingerPress;
    private void Awake()
    {
        press_any_key = NextMent_Alarm.GetComponent<Press_Any_Key>(); //깜빡이는거
    }
    private void Start()
    {
        Player.isUp = false;
        Itembtn.interactable = false;
        NextResultBtn.interactable = false;
        state = State.Start;
        for (int i = 0; i < MentList.Count; i++)
        {
            MentList[i].SetActive(false);
        }
        for (int i = 1; i < Fingers.Count; i++)
        {
            Fingers[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (!Key_Up && UI_On)
        {
            getKeyDown_();
        }
        else if (Key_Up && UI_On)
        {
            getKeyUp_();
        }

    }



    public void GameStart()
    {
        switch (this.state)
        {
            case State.Start:
                Tutorial_Start();
                break;
            case State.Item:
                Tutorial_Item_Part();
                break;
            case State.Magnet:
                Tutorial_Magent_Part();
                break;
            case State.End:
                Tutorial_End_Part();
                break;

        }
    }


    private void Tutorial_Start() //튜토리얼 초반부
    {
        switch (progress)
        {
            case 0:
                Time.timeScale = 0;
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(0,0));
                break;
            case 1:
                Delete_Last_Ment(0,0);
                Panel.SetActive(false);
                FirstPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(1, 0));
                break;
            case 2:                                        //캐릭터 상승패널     (추후 손가락 애니메이션
                Delete_Last_Ment(1, 0);
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(2,1));
                break;
            case 3:                                        //캐릭터 상승
                Delete_Last_Ment(2,1);
                Player.isUp = true;
                TouchPanel.SetActive(false);
                Time.timeScale = 1;
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 4:
                Time.timeScale = 0;
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(3, 0));
                break;
            case 5:                                        //캐릭터 하강패널     (추후 손가락 애니메이션
                Delete_Last_Ment(3, 0);
                Key_Up = true;
                Panel.SetActive(false);
                TouchPanel.SetActive(true);
                MentList[4].SetActive(true);
                StartCoroutine(Fuzz_This_Time());
                break;
            case 6:                                        //캐릭터 하강
                UI_On = false;
                Key_Up = false;
                Time.timeScale = 1;
                TouchPanel.SetActive(false);
                MentList[4].SetActive(false);
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:
                UI_On = true;
                Key_Up = false;
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(5,0));
                Time.timeScale = 0;
                break;
            case 8:
                Delete_Last_Ment(5, 0);
                StartCoroutine(MentBox_Timmer(6, 0));
                break;
            case 9:
                Delete_Last_Ment(6, 0);
                Panel.SetActive(false);
                Time.timeScale = 1;
                StateReset(State.Item);
                break;
            default:
                break;
        }
    }
    private void Tutorial_Item_Part() //튜토리얼 아이템 획득부분
    {
        switch (progress)
        {
            case 0:
                progress++;
                Invoke("NextLevel_",2f);
                break;
            case 1:
                Time.timeScale = 0;
                Key_Up = false;
                HPPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(7, 0));
                //MentList[4].SetActive(true);
                break;
            case 2:
                Delete_Last_Ment(7, 0);
                StartCoroutine(MentBox_Timmer(8, 0));
                break;
            case 3:
                Delete_Last_Ment(8, 0);
                StartCoroutine(MentBox_Timmer(9, 0));
                break;
            case 4:
                Delete_Last_Ment(9, 0);
                StartCoroutine(MentBox_Timmer(10, 0));
                break;
            case 5:
                Delete_Last_Ment(10, 0);
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                //MentList[4].SetActive(false);
                progress++;
                break;
            case 6:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:
                Key_Up = false;
                Time.timeScale = 0;
                HPPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(11,0));
                //MentList[5].SetActive(true);
                break;
            case 8:
                Delete_Last_Ment(11, 0);
                StartCoroutine(MentBox_Timmer(12, 0));
                break;
            case 9:
                Delete_Last_Ment(12, 0);
                StartCoroutine(MentBox_Timmer(13, 0));
                break;
            case 10:
                Delete_Last_Ment(13,0);
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                //MentList[5].SetActive(false);
                progress++;
                break;
            case 11:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 12:
                Key_Up = false;
                Time.timeScale = 0;
                ItemPanel.SetActive(true);
                StartCoroutine (MentBox_Timmer(14,0));
                //MentList[6].SetActive(true);
                break;
            case 13:
                Delete_Last_Ment(14, 0);
                StartCoroutine(MentBox_Timmer(15,0));
                Key_Up = true;
                break;
            case 14:                                 //자석 아이템 사용     (추후 손가락 애니메이션
                Delete_Last_Ment(15,0);
                Key_Up = false;
                MentList[16].SetActive(true);
                Fingers[2].SetActive(true);
                Fingers[2].GetComponent<Press_Any_Key>().StartAnyKeyco();
                //StartCoroutine(MentBox_Timmer(7, 2));
                Itembtn.interactable = true;
                break;
            case 15:
                UI_On = false;
                Key_Up = false;
                //Kill_The_Child(7,2);
                MentList[16].SetActive(false);
                Fingers[2].SetActive(false);
                ItemPanel.SetActive(false);
                Time.timeScale = 1;
                StateReset(State.Magnet);
                break;
            default:
                break;
        }
    }
    private void Tutorial_Magent_Part() //튜토리얼 자석아이템 부분
    {
        switch (progress)
        {
            case 0:
                Time.timeScale = 0;
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(17,0));
                break;
            case 1:
                Delete_Last_Ment(17, 0);
                StartCoroutine(MentBox_Timmer(18, 0));
                break;
            case 2:
                Delete_Last_Ment(18, 0);
                StartCoroutine(MentBox_Timmer(19, 0));
                break;
            case 3:
                Delete_Last_Ment(19, 0);
                StartCoroutine(MentBox_Timmer(20, 0));
                break;
            case 4:
                Delete_Last_Ment(20, 0);
                StartCoroutine(MentBox_Timmer(21, 0));
                break;
            case 5:
                Delete_Last_Ment(21, 0);
                Panel.SetActive(false);
                Time.timeScale = 1;
                progress++;
                break;
            case 6:
                Time.timeScale = 0;
                StarPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(22, 0));
                break;
            case 7:
                Delete_Last_Ment(22, 0);
                StartCoroutine(MentBox_Timmer(23, 0));
                break;
            case 8:
                Delete_Last_Ment(23, 0);
                StartCoroutine(MentBox_Timmer(24, 0));
                break;
            case 9:
                Delete_Last_Ment(24, 0);
                Time.timeScale = 1;
                StarPanel.SetActive(false);
                progress++;
                break;
            case 10:
                Invoke("NextLevel_", 2f);
                break;
            case 11:
                Time.timeScale = 0;
                StarcountPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(25, 0));
                break;
            case 12:
                Delete_Last_Ment(25, 0);
                StartCoroutine(MentBox_Timmer(26, 0));
                break;
            case 13:
                Delete_Last_Ment(26, 0);
                StarcountPanel.SetActive(false);
                StartCoroutine(MentBox_Timmer(27, 0));
                break;
            case 14:
                Delete_Last_Ment(27, 0);
                UI_On = false;
                Results_screen.SetActive(true);
                StartCoroutine(Fuzz_This_Time());
                break;
            case 15:
                Delete_Last_Ment(27, 0);
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(28, 0));
                break;
            case 16:
                Delete_Last_Ment(28, 0);
                StartCoroutine(MentBox_Timmer(29, 0));
                break;
            case 17:
                Delete_Last_Ment(29, 0);
                Panel.SetActive(false);
                ResultPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(30, 0));
                break;
            case 18:
                Delete_Last_Ment(30, 0);
                ResultPanel.SetActive(false);
                RewardPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(31, 0));
                break;
            case 19:
                Delete_Last_Ment(31, 0);
                StartCoroutine(MentBox_Timmer(32, 0));
                break;
            case 20:
                Delete_Last_Ment(32, 0);
                RewardPanel.SetActive(false);
                MissionPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(33, 0));
                break;
            case 21:
                Delete_Last_Ment(33, 0);
                StartCoroutine(MentBox_Timmer(34, 0));
                break;
            case 22:
                Delete_Last_Ment(34, 0);
                StartCoroutine(MentBox_Timmer(35, 0));
                break;
            case 23:
                Delete_Last_Ment(35, 0);
                StartCoroutine(MentBox_Timmer(36, 0));
                break;
            case 24:
                Delete_Last_Ment(36, 0);
                MissionPanel.SetActive(false);
                Key_Up = false;
                NextResult.SetActive(true);
                StateReset(State.End);
                MentList[37].SetActive(true);
                Fingers[3].SetActive(true);
                Fingers[3].GetComponent<Press_Any_Key>().StartAnyKeyco();
                NextResultBtn.interactable = true;
                break;
            default:
                break;
        }
    }
    private void Tutorial_End_Part() //튜토리얼 마지막부분
    {
        switch(progress)
        {
            case 0:
                NextResult.SetActive(false);
                englishRewardPanel.SetActive(true);
                StartCoroutine(Fuzz_This_Time());
                break;
            case 1:
                StartCoroutine(MentBox_Timmer(38, 0));
                break;
            case 2:
                Delete_Last_Ment(38, 0);
                StartCoroutine(MentBox_Timmer(39, 0));
                break;
            case 3:
                Delete_Last_Ment(39, 0);
                StartCoroutine(MentBox_Timmer(40, 0));
                break;
            case 4:
                Delete_Last_Ment(40, 0);
                StartCoroutine(MentBox_Timmer(41, 0));
                break;
            case 5:
                Delete_Last_Ment(41, 0);
                StartCoroutine(MentBox_Timmer(42, 0));
                break;
            case 6:
                Delete_Last_Ment(42, 0);
                StartCoroutine(MentBox_Timmer(43, 4));
                break;
            case 7:
                Delete_Last_Ment(43, 4);
                englishRewardPanel.SetActive(false);
                StartCoroutine(Fuzz_This_Time());
                break;
            case 8:
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(44, 0));
                break;
            case 9:
                Delete_Last_Ment(44, 0);
                Panel.SetActive(false);
                StarinfoPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(45, 0));
                break;
            case 10:
                Delete_Last_Ment(45, 0);
                StartCoroutine(MentBox_Timmer(46, 0));
                break;
            case 11:
                Delete_Last_Ment(46, 0);
                StarinfoPanel.SetActive(false);
                StudyPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(47, 0));
                break;
            case 12:
                Delete_Last_Ment(47, 0);
                StudyPanel.SetActive(false);
                SoundPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(48, 0));
                break;
            case 13:
                Delete_Last_Ment(48, 0);
                SoundPanel.SetActive(false);
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(49, 0));
                break;
            case 14:
                Delete_Last_Ment(49, 0);
                Panel.SetActive(false);
                MainPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(50, 0));
                break;
            case 15:
                MentList[50].SetActive(true);
                Fingers[5].SetActive(true);
                Fingers[5].GetComponent<Press_Any_Key>().StartAnyKeyco();
                MainBtn.interactable = true;
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Player")) //닿았을경우 튜토리얼 시작
        {
            GameStart();
        }
    }


    private IEnumerator MentBox_Timmer(int ListCount, int Finger) 
    {
        UI_On = false;
        MentList[ListCount].SetActive(true);
        yield return new WaitForSecondsRealtime(NextMentTimmer); //일정시간 뒤 다음으로 넘어가는 버튼 활성화
        NextMent_Alarm.SetActive(true);
        press_any_key.StartAnyKeyco();
        UI_On = true;
        if (Finger != 0)
        {
            FingerPress = Fingers[Finger].GetComponent<Press_Any_Key>();
            Fingers[Finger].SetActive(true);
            FingerPress.StartAnyKeyco();
        }

    }
    private void Delete_Last_Ment(int ListCount, int Finger) //이전에 사용한 멘트 삭제
    {
        UI_On = false;
        MentList[ListCount].SetActive(false);
        NextMent_Alarm.SetActive(false);
        if (Finger != 0)
        {
            FingerPress = Fingers[Finger].GetComponent<Press_Any_Key>();
            Fingers[Finger].SetActive(false);

        }
    }




    private void  getKeyDown_()
    {
        if (Input.GetMouseButtonDown(0))
        {
            progress++;
            GameStart();
        }
    }
    private void getKeyUp_()
    {
        if (Input.GetMouseButtonUp(0))
        {
            progress++;
            GameStart();
        }
    }
    private void NextLevel_()
    {
        GameStart();
    }
    private void StateReset(State st) 
    {
        state = st;
        progress = 0;
        Key_Up = false;
        UI_On = false;

    }
    private IEnumerator Fuzz_This_Time() //일정시간 뒤 넘어가기
    {
        yield return new WaitForSecondsRealtime(NextMentTimmer);
        progress++;
        GameStart();
    }
    public void Nextpage()
    {
        Results_screen.SetActive(false);
        Results_screen2.SetActive(true);
        MainBtn.interactable = false;
        MentList[37].SetActive(false);
        Fingers[3].SetActive(false);
        GameStart();
    }
    public void EndTuto()//다음 튜토리얼로 이동
    {
        Time.timeScale = 1;
        BackendGameData_JGD.userData.tutorial_Info.state = Tutorial_state.myplanet;
        BackendGameData_JGD.Instance.GameDataUpdate();
        TCP_Client_Manager.instance.go_myplanet();
    }
}
