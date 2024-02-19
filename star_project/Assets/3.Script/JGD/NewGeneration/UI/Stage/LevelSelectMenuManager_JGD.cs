using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenuManager_JGD : MonoBehaviour
{
    public Level_progress_JGD[] levelProgresses;
    public static int currLevel;
    public static int UnlockedLevels;

    public Sprite Clearscore;

    [SerializeField] private SceneNames nextScene;  //���� ���� ������ �ƴ� ���Ӿ��� ������ ����ٸ� ����

    private void Awake()
    {
        //currLevel = 0;
        //UnlockedLevels = 0;
        //Debug.Log(currLevel);
        //Debug.Log(UnlockedLevels);
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
       UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);    ////////////////////�������� Ŭ���� ���� ���� DB�� ��ȭ
        for (int i = 0; i < levelProgresses.Length; i++)
        {
            if (UnlockedLevels >= i)
            {
                levelProgresses[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(),0);
                for (int j = 0; j < stars; j++)
                {
                    levelProgresses[i].stars[j].sprite = Clearscore;
                }
            }
        }
    }



    public void OnClickLevel(int levelNum)
    {
        currLevel = levelNum;
        SceneManager.LoadScene(nextScene.ToString());//���� ���Ӿ��� ������ ����ٸ� ����//////////////
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }



}
