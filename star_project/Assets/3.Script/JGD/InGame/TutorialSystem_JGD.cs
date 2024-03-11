using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum State
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
    private float NextMentTimmer = 0.3f;
    //================================

    [SerializeField] Tuto_Player_Controll Player;
    [Header("StartUI")]
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject TouchPanel;
    [SerializeField] GameObject HPPanel;
    [SerializeField] GameObject ItemPanel;
    [SerializeField] GameObject StarPanel;
    [SerializeField] GameObject StarcountPanel;

    [SerializeField] GameObject NextMent_Alarm;
    [SerializeField] List<GameObject> MentList = new List<GameObject>();
    [SerializeField] List<GameObject> Fingers = new List<GameObject>();
    [SerializeField] public Button Itembtn;
    State state;

    [SerializeField] GameObject MagnetItem;
    [Header("EndUI")]
    [SerializeField] GameObject Results_screen;
    [SerializeField] GameObject Results_screen2;
    Press_Any_Key press_any_key;
    Press_Any_Key FingerPress;
    private void Awake()
    {
        press_any_key = NextMent_Alarm.GetComponent<Press_Any_Key>();
    }
    private void Start()
    {
        Player.isUp = false;
        Itembtn.interactable = false;
        state = State.Start;
        for (int i = 0; i < MentList.Count; i++)
        {
            MentList[i].SetActive(false);
            //MentList[i].transform.GetChild(0).gameObject.SetActive(false);
            //Kill_The_Child(i);
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

                break;

        }
    }


    private void Tutorial_Start()
    {
        switch (progress)
        {
            case 0:
                Time.timeScale = 0;
                Panel.SetActive(true);
                //FirstPanel.SetActive(true);
                //MentList[0].SetActive(true);
                StartCoroutine(MentBox_Timmer(0,0));
                break;
            case 1:
                Kill_The_Child(0,0);
                Panel.SetActive(false);
                FirstPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(1, 0));
                break;
            case 2:                                        //캐릭터 상승패널     (추후 손가락 애니메이션
                Kill_The_Child(1, 0);
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                //MentList[0].SetActive(false);
                //MentList[1].SetActive(true);

                StartCoroutine(MentBox_Timmer(2,1));
                break;
            case 3:                                        //캐릭터 상승
                Kill_The_Child(2,1);
                //Key_Up = true;
                Player.isUp = true;
                TouchPanel.SetActive(false);
                //MentList[1].SetActive(false);
                //this.gameObject.SetActive(false);
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
                Kill_The_Child(3, 0);
                Key_Up = true;
                Panel.SetActive(false);
                TouchPanel.SetActive(true);
                MentList[4].SetActive(true);
                StartCoroutine(TimmuStoopu());
                //Invoke("NextLevel_", 3f);
                //StartCoroutine(MentBox_Timmer(3, 2));
                //progress++;
                //GameStart();
                break;
            case 6:                                        //캐릭터 하강
                UI_On = false;
                Key_Up = false;
                Time.timeScale = 1;
                TouchPanel.SetActive(false);
                MentList[4].SetActive(false);
                //StartCoroutine(MentBox_Timmer(3, 2));
                //Kill_The_Child(2, 0);
                //UI_On = true;
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:
                UI_On = true;
                Key_Up = false;
                Panel.SetActive(true);
                //MentList[3].SetActive(true); 
                StartCoroutine(MentBox_Timmer(5,0));
                Time.timeScale = 0;
                break;
            case 8:
                Kill_The_Child(5, 0);
                //MentList[3].SetActive(false);
                StartCoroutine(MentBox_Timmer(6, 0));
                break;
            case 9:
                Kill_The_Child(6, 0);
                Panel.SetActive(false);
                Time.timeScale = 1;
                StateReset(State.Item);
                break;
            default:
                break;
        }
    }
    private void Tutorial_Item_Part()
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
                Kill_The_Child(7, 0);
                StartCoroutine(MentBox_Timmer(8, 0));
                break;
            case 3:
                Kill_The_Child(8, 0);
                StartCoroutine(MentBox_Timmer(9, 0));
                break;
            case 4:
                Kill_The_Child(9, 0);
                StartCoroutine(MentBox_Timmer(10, 0));
                break;
            case 5:
                Kill_The_Child(10, 0);
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
                Kill_The_Child(11, 0);
                StartCoroutine(MentBox_Timmer(12, 0));
                break;
            case 9:
                Kill_The_Child(12, 0);
                StartCoroutine(MentBox_Timmer(13, 0));
                break;
            case 10:
                Kill_The_Child(13,0);
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
                Kill_The_Child(14, 0);
                StartCoroutine(MentBox_Timmer(15,0));
                Key_Up = true;
                break;
            case 14:                                 //자석 아이템 사용     (추후 손가락 애니메이션
                Kill_The_Child(15,0);
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
    private void Tutorial_Magent_Part()
    {
        switch (progress)
        {
            case 0:
                Time.timeScale = 0;
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(17,0));
                break;
            case 1:
                Kill_The_Child(17, 0);
                StartCoroutine(MentBox_Timmer(18, 0));
                break;
            case 2:
                Kill_The_Child(18, 0);
                StartCoroutine(MentBox_Timmer(19, 0));
                break;
            case 3:
                Kill_The_Child(19, 0);
                StartCoroutine(MentBox_Timmer(20, 0));
                break;
            case 4:
                Kill_The_Child(20, 0);
                StartCoroutine(MentBox_Timmer(21, 0));
                break;
            case 5:
                Kill_The_Child(21, 0);
                Panel.SetActive(false);
                //MagnetItem.gameObject.SetActive(false);
                Time.timeScale = 1;
                progress++;
                break;
            case 6:
                Time.timeScale = 0;
                StarPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(22, 0));
                break;
            case 7:
                Kill_The_Child(22, 0);
                StartCoroutine(MentBox_Timmer(23, 0));
                break;
            case 8:
                Kill_The_Child(23, 0);
                StartCoroutine(MentBox_Timmer(24, 0));
                break;
            case 9:
                Kill_The_Child(24, 0);
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
                StartCoroutine(MentBox_Timmer(24, 0));
                break;
            case 12:
                Kill_The_Child(24, 0);
                StartCoroutine(MentBox_Timmer(25, 0));
                break;
            case 13:
                Kill_The_Child(25, 0);
                StarcountPanel.SetActive(false);
                Time.timeScale = 1;
                StartCoroutine(MentBox_Timmer(26, 0));
                break;
            case 14:
                Kill_The_Child(26, 0);
                Time.timeScale = 0;
                StartCoroutine(MentBox_Timmer(27, 0));
                break;
            case 15:
                Kill_The_Child(27, 0);
                Results_screen.SetActive(true);
                Panel.SetActive(true);
                StartCoroutine(MentBox_Timmer(28, 0));
                break;
            case 16:
                Kill_The_Child(28, 0);
                StartCoroutine(MentBox_Timmer(29, 0));
                break;
            case 17:

                break;
            default:
                break;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Player"))
        {
            GameStart();
        }
    }


    private IEnumerator MentBox_Timmer(int ListCount, int Finger)  //안대면 이거임 ㅇㅇ///////////////////////////////////////////////////////////////
    {
        UI_On = false;
        MentList[ListCount].SetActive(true);
        yield return new WaitForSecondsRealtime(NextMentTimmer);
        NextMent_Alarm.SetActive(true);
        press_any_key.StartAnyKeyco();
        UI_On = true;
        if (Finger != 0)
        {
            FingerPress = Fingers[Finger].GetComponent<Press_Any_Key>();
            Fingers[Finger].SetActive(true); //코루틴 실행해야함
            FingerPress.StartAnyKeyco();
        }

    }
    private void Kill_The_Child(int ListCount, int Finger)
    {
        UI_On = false;
        MentList[ListCount].SetActive(false);
        NextMent_Alarm.SetActive(false);
        //MentList[ListCount].transform.GetChild(0).gameObject.SetActive(false);  //추후 변경
        if (Finger != 0)
        {
            FingerPress = Fingers[Finger].GetComponent<Press_Any_Key>();
            //FingerPress.StopCoroutine(FingerPress.PressNext);
            Fingers[Finger].SetActive(false);

        }
        //if (press_any_key.PressNext != null)
        //{
        //    press_any_key.StopCoroutine(press_any_key.PressNext);
        //}

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
    private IEnumerator TimmuStoopu()
    {
        yield return new WaitForSecondsRealtime(NextMentTimmer);
        progress++;
        GameStart();
    }
    public void Nextpage()
    {
        Results_screen.SetActive(false);
        Results_screen2.SetActive(true) ;
    }
}
