using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
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
            case 1:
                FirstPanel.SetActive(false);
                TouchPanel.SetActive(true);
                MentList[1].SetActive(true);
                break;
            case 2:
                UI_On = false;
                //Key_Up = true;
                Player.isUp = true;
                TouchPanel.SetActive(false);
                MentList[1].SetActive(false);
                this.gameObject.SetActive(false);
                Time.timeScale = 1;
                progress++;
                break;
            case 3:
                break;
            case 4:
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


}
