using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager_YG : MonoBehaviour
{
    public static TutorialManager_YG instance;

    [SerializeField] private int cur_stage; //���� ��������
    [SerializeField] private int stop_sec; //���ߴ� �ð�

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

    private void Go_tuto()
    {
        
    }
}