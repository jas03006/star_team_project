using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager_YG : MonoBehaviour
{
    public static TutorialManager_YG instance;

    [SerializeField] private int cur_stage; //현재 스테이지
    [SerializeField] private int stop_sec; //멈추는 시간

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
        
    }

    private void Go_tuto()
    {
        
    }
}