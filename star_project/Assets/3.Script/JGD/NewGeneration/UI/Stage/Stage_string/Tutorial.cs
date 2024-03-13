using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        //StageItemInfo_JGD.Instance.ReadStage(tutorial);
    }
    public void S2__GalaxyLevel__S2(int a)
    {
        LevelSelectMenuManager_JGD.GalaxyLevel = a;
    }
    public void S2__currLevel__S2(int b)
    {
        LevelSelectMenuManager_JGD.currLevel = b;
        SceneManager.LoadScene("Game");
    }
}
