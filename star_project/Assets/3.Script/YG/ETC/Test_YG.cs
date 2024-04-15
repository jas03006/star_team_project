using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test_YG : MonoBehaviour
{
    public Image image;
    public void Set_level()
    {
        BackendGameData_JGD.userData.level++;
        Debug.Log(BackendGameData_JGD.userData.level);
    }
    public void Send_level()
    {
        BackendGameData_JGD.Instance.Send_level();
    }

    public void Change_Scene(string str)
    {
        SceneManager.LoadScene(str);
    }

    private void Awake()
    {
        GoogleHashKey();
    }

    public void Click()
    {
        AudioManager.instance.SFX_Click();
    }
    public void BGM_catchingstar()
    {
        AudioManager.instance.BGM_catchingstar();
    }
    public void BGM_myplanet()
    {
        AudioManager.instance.BGM_myplanet();
    }

    public void GoogleHashKey()
    {
       // Debug.Log("GoogleHashKey 확인 디버그:" + Backend.Utils.GetGoogleHash());
    }
}
