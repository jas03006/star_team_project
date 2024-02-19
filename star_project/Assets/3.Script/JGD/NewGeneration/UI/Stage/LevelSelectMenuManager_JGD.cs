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

    [SerializeField] private SceneNames nextScene;  //만약 기존 구조가 아닌 게임씬을 여러개 만든다면 수정

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
       UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);    ////////////////////스테이지 클리어 정보 추후 DB로 변화
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
        SceneManager.LoadScene(nextScene.ToString());//만약 게임씬을 여러개 만든다면 수정//////////////
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }



}
