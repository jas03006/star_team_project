using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStage : MonoBehaviour
{
    private bool Timestop = false;
    public GameObject[] levels;
    [SerializeField]private GameEnd_JGD Player;
    [SerializeField] private SceneNames nextScene;
    [SerializeField] GameObject Pause;
    [SerializeField] TMP_Text Timmer;
    [Header("Btn")]
    [SerializeField] Button IngameUI;
    [SerializeField] Button UsingItemBtn;
    private void Awake()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }
    private void Start()
    {
        levels[LevelSelectMenuManager_JGD.GalaxyLevel].SetActive(true);
        IngameUI.interactable = false;
        UsingItemBtn.interactable = false;
        StartCoroutine(StageStart_controller());
    }
    private IEnumerator StageStart_controller()
    {
        Time.timeScale = 0;
        float Timer = 0;
        float MaxTimer = 4f;
        while (Timer <= MaxTimer)
        {
            Timer += Time.unscaledDeltaTime;
            Timmer.text = ((MaxTimer - Timer)+1).ToString("F0");
            yield return null;
        }
        Timmer.text = "";
        IngameUI.interactable = true;
        UsingItemBtn.interactable = true;
        Time.timeScale = 1;
    }
    public void ComeBakeHome()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene("My_Planet_TG");
        TCP_Client_Manager.instance.go_myplanet();
    }
    public void StgageSelect_Btn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene.ToString());
    }
    public void StageSelect(string stage)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(stage);
    }
    public void NextSceneSelect()
    {
        Time.timeScale = 1;
        LevelSelectMenuManager_JGD.currLevel++;
        if (LevelSelectMenuManager_JGD.currLevel < 5)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("Stage");
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    public void TimeControll()
    {
        if (Timestop)
        {
            Pause.SetActive(false);
            Time.timeScale = 1;
            Timestop = false;
        }
        else
        {
            Pause.SetActive(true);
            Time.timeScale = 0;
            Timestop = true;
        }
    }
    public void Btn_Click_Sound()
    {
        AudioManager.instance.SFX_Click();
    }
    //public void LevelComplete()  //게임결과
    //{
    //    int starsAquired = 0;
    //    starsAquired = Player.StarCount;
    //    if (LevelSelectMenuManager_JGD.currLevel == LevelSelectMenuManager_JGD.UnlockedLevels)
    //    {
    //        LevelSelectMenuManager_JGD.UnlockedLevels++;
    //        PlayerPrefs.SetInt("UnlockedLevels", LevelSelectMenuManager_JGD.UnlockedLevels);
    //    }
    //    if (starsAquired > PlayerPrefs.GetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(),0))
    //    {
    //        PlayerPrefs.SetInt("stars" + LevelSelectMenuManager_JGD.currLevel.ToString(), starsAquired);
    //    }
    //}
    
}
