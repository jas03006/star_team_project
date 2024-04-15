using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Ʃ�丮�� ���� �� ������ ����ϴ� Ŭ����
/// </summary>
public enum Tutorial_state//Ʃ�丮�� ����. �б⺰�� ����.
{
    catchingstar_chapter = 0,//tutorial_YG���� ����
    catchingstar_play = 1,//tutorial ������ ����
    myplanet = 2,//tutorial_TG���� ����
    clear
}
//
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    //[SerializeField] private int cur_stage; //���� ��������
    //[SerializeField] private int stop_sec; //���ߴ� �ð�

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

            //Debug.Log("����"+BackendGameData_JGD.userData.tutorial_Info.state);
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