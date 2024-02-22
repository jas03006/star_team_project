using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using BackEnd;
using UnityEngine.SceneManagement;

public class TestBtnAction : MonoBehaviour
{
    [SerializeField] private SceneNames nextScene;
    public void TestBTN()
    {
        BackendChart_JGD.Instance.ChartGet("107516");
        //Debug.Log("�� ����־�1");
        //BackendGameData_JGD.userData.House_Item_ID_List.Add(new House_Item_Info_JGD());
        //Debug.Log("�� ����־�2");
        //BackendGameData_JGD.userData.House_Item_ID_List.Add(new House_Item_Info_JGD());
        //Debug.Log("�� ����־�3");
        //BackendGameData_JGD.userData.House_Item_ID_List.Add(new House_Item_Info_JGD());
        //Debug.Log("�� ����־�4");
        //user_DB_update();
        //Debug.Log(BackendGameData_JGD.userData.House_Item_ID_List[0]);
    }
    async void user_DB_update()
    {
        await Task.Run(() =>
        {
            BackendGameData_JGD.Instance.GameDataUpdate();
            BackendGameData_JGD.Instance.GameDataGet();

        });
    }
    public void NextStage()
    {
        SceneManager.LoadScene(nextScene.ToString());
    }

    public void NextStage(string str)
    {
        SceneManager.LoadScene(str);
    }

    public void hide_planet_UI() {
        TCP_Client_Manager.instance.hide_planet_buttons(false);
    }
}
