using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using BackEnd;
using UnityEngine.SceneManagement;

public class TestBtnAction : MonoBehaviour
{
    [SerializeField] private SceneNames nextScene;
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

    public void exit_room() {
        TCP_Client_Manager.instance.exit_room_btn();
    }
}
