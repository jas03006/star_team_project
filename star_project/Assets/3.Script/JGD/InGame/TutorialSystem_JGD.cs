using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] Tuto_Player_Controll Player;

    [Header("StartUI")]
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject TouchPanel;
    [SerializeField] GameObject HPPanel;
    [SerializeField] GameObject ItemPanel;
    [SerializeField] List<GameObject> MentList = new List<GameObject>();
    [SerializeField] List<GameObject> NextMentList = new List<GameObject>();


    State state;
    private void Start()
    {
        Player.isUp = false;
        state = State.Start;
        for (int i = 0; i < MentList.Count; i++)
        {
            MentList[i].SetActive(false);
            Kill_The_Child(i);
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
                StartCoroutine(MentBox_Timmer(3, 0));
                break;
            case 1:                                        //캐릭터 상승패널     (추후 손가락 애니메이션
                Kill_The_Child(0);
                UI_On = true;
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                //MentList[0].SetActive(false);
                //MentList[1].SetActive(true);
                StartCoroutine(MentBox_Timmer(3, 1));
                break;
            case 2:                                        //캐릭터 상승
                Kill_The_Child(1);
                UI_On = false;
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
                StartCoroutine(TimmuStoopu(3));
                //Invoke("NextLevel_", 3f);
                //StartCoroutine(MentBox_Timmer(3, 2));
                //progress++;
                //GameStart();
                break;
            case 4:                                        //캐릭터 하강
                Key_Up = false;
                Time.timeScale = 1;
                TouchPanel.SetActive(false);
                //MentList[2].SetActive(false);
                //StartCoroutine(MentBox_Timmer(3, 2));
                Kill_The_Child(2);
                //UI_On = true;
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 5:
                UI_On = true;
                Key_Up = false;
                Panel.SetActive(true);
                //MentList[3].SetActive(true); 
                StartCoroutine(MentBox_Timmer(3, 3));
                Time.timeScale = 0;
                break;
            case 6:
                Time.timeScale = 1;
                UI_On = false;
                Panel.SetActive(false);
                //MentList[3].SetActive(false);
                Kill_The_Child(3);
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
                UI_On = true;
                Key_Up = false;
                HPPanel.SetActive(true);
                MentList[4].SetActive(true);
                break;
            case 2:
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                MentList[4].SetActive(false);
                UI_On = false;
                progress++;
                break;
            case 3:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 4:
                UI_On = true;
                Key_Up = false;
                Time.timeScale = 0;
                HPPanel.SetActive(true);
                MentList[5].SetActive(true);
                break;
            case 5:
                UI_On = false;
                Key_Up = false;
                Time.timeScale = 1;
                HPPanel.SetActive(false);
                MentList[5].SetActive(false);
                break;
            case 6:
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:
                UI_On = true;
                Key_Up = true;
                Time.timeScale = 0;
                ItemPanel.SetActive(true);
                MentList[6].SetActive(true);
                break;
            case 8:                                 //자석 아이템 사용     (추후 손가락 애니메이션
                UI_On = false;
                Key_Up = false;
                progress++;
                MentList[6].SetActive(false);
                MentList[7].SetActive(true);
                break;
            case 9:
                Time.timeScale = 1;
                ItemPanel.SetActive(false);
                MentList[7].SetActive(false);
                break;
            case 10:

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


    private IEnumerator MentBox_Timmer(float num, int ListCount)
    {
        UI_On = false;
        MentList[ListCount].SetActive(true);
        yield return new WaitForSecondsRealtime(num);
        MentList[ListCount].transform.GetChild(0).gameObject.SetActive(true);
        UI_On = true;

    }
    private void Kill_The_Child(int ListCount)
    {
        UI_On = false;
        MentList[ListCount].SetActive(false);
        MentList[ListCount].transform.GetChild(0).gameObject.SetActive(false);
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
    private IEnumerator TimmuStoopu(float num)
    {
        yield return new WaitForSecondsRealtime(num);
        progress++;
        GameStart();
    }
}
