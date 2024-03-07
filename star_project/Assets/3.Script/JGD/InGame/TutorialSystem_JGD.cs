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
    int progress = 0;

    [SerializeField] Tuto_Player_Controll Player;

    [Header("StartUI")]
    [SerializeField] GameObject TutoUI;
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject TouchPanel;
    [SerializeField] GameObject HPPanel;
    [SerializeField] GameObject ItemPanel;
    [SerializeField] List<GameObject> MentList = new List<GameObject>();


    State state;
    private void Start()
    {
        Player.isUp = false;
        state = State.Start;
        for (int i = 0; i < MentList.Count; i++)
        {
            MentList[i].SetActive(false);
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
                TutorialStart();
                break;
            case State.Item:

                break;
            case State.Magnet:

                break;
            case State.End:

                break;

        }
    }


    private void TutorialStart()
    {
        switch (progress)
        {
            case 0:
                UI_On = true;
                Time.timeScale = 0;
                TutoUI.SetActive(true);
                FirstPanel.SetActive(true);
                MentList[0].SetActive(true);
                break;
            case 1:                                        //캐릭터 상승패널
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                MentList[0].SetActive(false);
                MentList[1].SetActive(true);
                break;
            case 2:                                        //캐릭터 상승
                UI_On = false;
                //Key_Up = true;
                Player.isUp = true;
                TouchPanel.SetActive(false);
                MentList[1].SetActive(false);
                //this.gameObject.SetActive(false);
                Time.timeScale = 1;
                progress++;
                Invoke("NextLevel_", 1f);
                break;
            case 3:                                        //캐릭터 하강패널
                ReTry(0, true,2);
                //UI_On = true;
                //Key_Up = true;
                //Time.timeScale = 0;
                //TouchPanel.SetActive(true);
                //MentList[2].SetActive(true);

                break;
            case 4:                                        //캐릭터 하강
                ReTry(1, false,2);
                //UI_On = false;
                //Key_Up = false;
                //Time.timeScale = 1;
                //TouchPanel.SetActive(false);
                //MentList[2].SetActive(false);
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 5:                                        //캐릭터 상승패널
                UI_On = true;
                Time.timeScale = 0;
                TouchPanel.SetActive(true);
                MentList[1].SetActive(true);
                break;
            case 6:                                        //캐릭터 상승
                ReTry(0, false, 1);
                //UI_On = false;
                //Key_Up = false;
                //Time.timeScale = 1;
                //TouchPanel.SetActive(false);
                //MentList[1].SetActive(false);
                progress++;
                Invoke("NextLevel_", 1.5f);
                break;
            case 7:                                        //캐릭터 하강
                ReTry(0, true,2);
                //UI_On = true;
                //Key_Up = true;
                //Time.timeScale = 0;
                //TouchPanel.SetActive(true);
                //MentList[2].SetActive(true);
                break;
            case 8:
                ReTry(1, false,2);
                //UI_On = false;
                //Key_Up = false;
                //Time.timeScale = 1;
                //TouchPanel.SetActive(false);
                //MentList[2].SetActive(false);
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
    private void ReTry(int num , bool tf , int count)
    {
        UI_On = tf;
        Key_Up = tf;
        Time.timeScale = num;
        TouchPanel.SetActive(tf);
        MentList[count].SetActive(tf);
    }

}
