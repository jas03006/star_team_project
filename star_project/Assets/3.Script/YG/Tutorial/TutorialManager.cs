using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Tutorial_state
{
    catchingstar_chapter = 0,
    catchingstar_play = 1,
    myplanet = 2,
    clear
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    //[SerializeField] private int cur_stage; //���� ��������
    //[SerializeField] private int stop_sec; //���ߴ� �ð�

    [SerializeField] public Tutorial_YG tutorial_YG;
    [SerializeField] private Tutorial_TG tutorial_TG;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded -= OnSceneLoaded;

        pannel_image.alphaHitTestMinimumThreshold = 0.5f;

        //Ʃ�丮�� �ܰ� ��������
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
            tutorial_YG.Count_up();
        }
    }
    private void GoToStage()
    {
        Debug.Log("GoToStage");
        pannel.SetActive(true);
        SceneManager.LoadScene("Stage");

        if (BackendGameData_JGD.userData.tutorial_Info.state == Tutorial_state.catchingstar_play)
        {
            GoToCatchingstar();
        }
    }

    private void GoToCatchingstar()
    {
        Debug.Log("GoToCatchingstar");
        pannel.GetComponent<Image>().sprite = ingame_sprite;
        pannel.SetActive(true);
    }
}