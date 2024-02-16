using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
