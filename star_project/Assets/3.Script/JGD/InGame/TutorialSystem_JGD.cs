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
    private float NextMentTimmer = 3;
    //================================

    [SerializeField] Tuto_Player_Controll Player;
    [Header("StartUI")]
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject TouchPanel;
    [SerializeField] GameObject HPPanel;
    [SerializeField] GameObject ItemPanel;
    [SerializeField] GameObject StarPanel;

    [SerializeField] GameObject NextMent_Alarm;
    [SerializeField] List<GameObject> MentList = new List<GameObject>();
    [SerializeField] List<GameObject> Fingers = new List<GameObject>();
    [SerializeField] public Button Itembtn;
    State state;

    [SerializeField] GameObject MagnetItem;
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
                FirstPanel.SetActive(true);
                //MentList[0].SetActive(true);
                StartCoroutine(MentBox_Timmer(0,0));
                break;
            case 1:                                        //캐릭터 상승패널     (추후 손가락 애니메이션
                Kill_The_Child(0,0);
                UI_On = false;
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                //MentList[0].SetActive(false);
                //MentList[1].SetActive(true);

                StartCoroutine(MentBox_Timmer(1,1));
                break;
            case 2:                                        //캐릭터 상승
                Kill_The_Child(1,1);
                //Key_Up = true;
                Player.isUp = true;
                TouchPanel.SetActive(false);
                //MentList[1].SetActive(false);
                //this.gameObject.SetActive(false);
                Time.timeScale = 1;
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 3:                                        //캐릭터 하강패널     (추후 손가락 애니메이션
                UI_On = false;
                Key_Up = true;
                Time.timeScale = 0;
                TouchPanel.SetActive(true);
                MentList[2].SetActive(true);
                StartCoroutine(TimmuStoopu());
                //Invoke("NextLevel_", 3f);
                //StartCoroutine(MentBox_Timmer(3, 2));
                //progress++;
                //GameStart();
                break;
            case 4:                                        //캐릭터 하강
                UI_On = false;
                Key_Up = false;
                Time.timeScale = 1;
                TouchPanel.SetActive(false);
                MentList[2].SetActive(false);
                //StartCoroutine(MentBox_Timmer(3, 2));
                //Kill_The_Child(2, 0);
                //UI_On = true;
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 5:
                UI_On = true;
                Key_Up = false;
                Panel.SetActive(true);
                //MentList[3].SetActive(true); 
                StartCoroutine(MentBox_Timmer(3,0));
                Time.timeScale = 0;
                break;
            case 6:
                Time.timeScale = 1;
                UI_On = false;
                Panel.SetActive(false);
                //MentList[3].SetActive(false);
                Kill_The_Child(3, 0);
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
                StartCoroutine(MentBox_Timmer(4, 0));
                //MentList[4].SetActive(true);
                break;
            case 2:
                Kill_The_Child(4, 0);
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                //MentList[4].SetActive(false);
                progress++;
                break;
            case 3:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 4:
                Key_Up = false;
                Time.timeScale = 0;
                HPPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(5,0));
                //MentList[5].SetActive(true);
                break;
            case 5:
                Kill_The_Child(5,0);
                Key_Up = false;
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                //MentList[5].SetActive(false);
                progress++;
                break;
            case 6:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:
                Key_Up = true;
                Time.timeScale = 0;
                ItemPanel.SetActive(true);
                StartCoroutine (MentBox_Timmer(6,0));
                //MentList[6].SetActive(true);
                break;
            case 8:                                 //자석 아이템 사용     (추후 손가락 애니메이션
                Kill_The_Child(6,0);
                Key_Up = false;
                MentList[7].SetActive(true);
                MentList[7].transform.GetChild(0).gameObject.SetActive(true);  //ㅈ같네
                Fingers[2].SetActive(true);
                //StartCoroutine(MentBox_Timmer(7, 2));
                Itembtn.interactable = true;
                break;
            case 9:
                UI_On = false;
                Key_Up = false;
                //Kill_The_Child(7,2);
                MentList[7].SetActive(false);
                Fingers[2].GetComponent<Press_Any_Key>().StopCoroutine(FingerPress.PressNext);
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
                StartCoroutine(MentBox_Timmer(8,0));
                break;
            case 1:
                Kill_The_Child(8, 0);
                StartCoroutine(MentBox_Timmer(9, 0));
                break;
            case 2:
                Kill_The_Child(9, 0);
                Panel.SetActive(false);
                //MagnetItem.gameObject.SetActive(false);
                Time.timeScale = 1;
                progress++;
                break;
            case 3:
                Time.timeScale = 0;
                StarPanel.SetActive(true);
                StartCoroutine(MentBox_Timmer(10, 0));
                break;
            case 4:
                break;
            case 5:
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
}
