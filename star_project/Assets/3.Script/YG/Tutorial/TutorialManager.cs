using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private int cur_stage; //���� ��������
    [SerializeField] private int stop_sec; //���ߴ� �ð�

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
        //Ʃ�丮�� �ܰ� ��������

        
    }

    private void GoToStage()
    {
        
    }

    private void GoToCatchingstar()
    {
    
    }
}