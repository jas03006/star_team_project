using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 튜토리얼 진입 및 저장을 담당하는 클래스
/// </summary>
public enum Tutorial_state//튜토리얼 상태. 분기별로 나눔.
{
    catchingstar_chapter = 0,//tutorial_YG에서 진행
    catchingstar_play = 1,//tutorial 씬에서 진행
    myplanet = 2,//tutorial_TG에서 진행
    clear
}
//
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    //[SerializeField] private int cur_stage; //현재 스테이지
    //[SerializeField] private int stop_sec; //멈추는 시간

    [SerializeField] public Tutorial_YG tutorial_YG; 
    [SerializeField] private Tutorial_TG tutorial_TG; 
    [SerializeField] private Button catchingstar_go;

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject pannel;
    [SerializeField] private Image pannel_image;

    [SerializeField] private Sprite loading_sprite;
    [SerializeField] private Sprite ingame_sprite;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        pannel.SetActive(true);
        pannel_image.alphaHitTestMinimumThreshold = 0.5f;

        //튜토리얼 단계 입장유도
        switch (BackendGameData_JGD.userData.tutorial_Info.state)
        {
            case Tutorial_state.catchingstar_chapter:
                GoToStage();
                break;
            case Tutorial_state.catchingstar_play:
                GoToStage();
                break;
            default:
                pannel.SetActive(false);
                return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Stage")
        {
            tutorial_YG = FindObjectOfType<Tutorial_YG>();

            //Debug.Log("현재"+BackendGameData_JGD.userData.tutorial_Info.state);
            if (BackendGameData_JGD.userData.tutorial_Info.state == Tutorial_state.catchingstar_play)
            {
                GoToCatchingstar();
            }
            else
            {
                pannel.SetActive(false);
            }
        }
        
        if (scene.name == "Tutorial")
        {
            pannel.SetActive(false);
        }
    }
    private void GoToStage()
    {
        Debug.Log("GoToStage");
        pannel.SetActive(true);
        catchingstar_go.onClick.Invoke();
        SceneManager.LoadScene("Stage");
    }

    private void GoToCatchingstar()
    {
        Debug.Log("GoToCatchingstar");
        pannel_image.sprite = ingame_sprite;
        pannel.SetActive(true);
    }
}