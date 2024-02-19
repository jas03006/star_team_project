using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_YG : MonoBehaviour
{
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
}
