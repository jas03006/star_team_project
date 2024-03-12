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
    public void StageMove(int a, int b)
    {
        LevelSelectMenuManager_JGD.GalaxyLevel = a;
        LevelSelectMenuManager_JGD.currLevel = b;
        SceneManager.LoadScene("Game");
    }
}
