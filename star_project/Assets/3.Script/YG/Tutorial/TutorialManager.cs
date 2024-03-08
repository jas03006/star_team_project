using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum Tutorial_state
{
    catchingstar_chapter = 0,
    catchingstar_play = 1,
    myplanet = 2,
    myplanet_housing = 3,
    myplanet_profile = 4,
    clear = 5
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private int cur_stage; //현재 스테이지
    [SerializeField] private int stop_sec; //멈추는 시간

    [SerializeField] private Tutorial_YG tutorial_YG;
    [SerializeField] private Tutorial_TG tutorial_TG;

    [SerializeField] private Canvas canvas;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //튜토리얼 단계 입장유도
        switch (BackendGameData_JGD.userData.tutorial_Info.state)
        {
            case Tutorial_state.catchingstar_chapter:
                GoToStage();
                break;
            case Tutorial_state.catchingstar_play:
                GoToCatchingstar();
                break;
            default:
                return;
        }
    }

    private void GoToStage()
    {
        
    }

    private void GoToCatchingstar()
    {
    
    }
}