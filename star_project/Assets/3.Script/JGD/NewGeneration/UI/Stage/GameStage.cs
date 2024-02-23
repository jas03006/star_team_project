using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    public GameObject[] levels;
    [SerializeField]private GameEnd_JGD Player;
    [SerializeField] private SceneNames nextScene;
    private void Start()
    {
        levels[LevelSelectMenuManager_JGD.currLevel].SetActive(true);

    }

    public void StgageSelect_Btn()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }
    public void StageSelect(string stage)
    {
        SceneManager.LoadScene(stage);
    }
    public void NextSceneSelect()
    {
        LevelSelectMenuManager_JGD.currLevel++;
        SceneManager.LoadScene("Game");
    }
    public void LevelComplete()  //게임결과
    {
        int starsAquired = 0;
        starsAquired = Player.StarCount;
        if (LevelSelectMenuManager_JGD.currLevel == LevelSelectMenuManager_JGD.UnlockedLevels)
        {
            LevelSelectMenuManager_JGD.UnlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelSelectMenuManager_JGD.UnlockedLevels);
        }
        if (starsAquired > PlayerPrefs.GetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(),0))
        {
            PlayerPrefs.SetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(), starsAquired);
        }
    }





}
