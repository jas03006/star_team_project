using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    public GameObject[] levels;
    [SerializeField]private GameObject ResultUI;
    [SerializeField]private GameObject NextResultUI;

    [SerializeField] private SceneNames nextScene;
    private void Start()
    {
        ResultUI.SetActive(false);
        NextResultUI.SetActive(false);
        levels[LevelSelectMenuManager_JGD.currLevel].SetActive(true);

    }

    public void StgageSelect_Btn()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }

    public void nextResult()
    {
        ResultUI.SetActive(false);
        NextResultUI.SetActive(true);
    }
    public void LevelComplete(int starsAquired)  //게임결과
    {
        if (LevelSelectMenuManager_JGD.currLevel == LevelSelectMenuManager_JGD.UnlockedLevels)
        {
            LevelSelectMenuManager_JGD.UnlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelSelectMenuManager_JGD.UnlockedLevels);
        }
        if (starsAquired > PlayerPrefs.GetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(),0))
        {
            PlayerPrefs.SetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(), starsAquired);
        }
        SceneManager.LoadScene("Stage");
    }
}
