using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//마이플래닛 설정 버튼 UI
//bgm, sfx 소리 조절, 진동 on/off, 로그아웃 기능
public class SettingUI_TG : MonoBehaviour
{
    [SerializeField] private Toggle bgm_toggle;
    [SerializeField] private Toggle sfx_toggle;
    [SerializeField] private Toggle haptic_toggle;
    [SerializeField] private GameObject bgm_image_gameobject;
    [SerializeField] private GameObject sfx_image_gameobject;
    [SerializeField] private GameObject haptic_image_gameobject;
    // Start is called before the first frame update
    void Start()
    {
       // update_UI();
    }
    
    public void update_UI()
    {
        if (AudioManager.instance.playing_bgm)
        {
            bgm_toggle.SetIsOnWithoutNotify(true);
            bgm_image_gameobject.SetActive(true);
        }
        else {
            bgm_toggle.SetIsOnWithoutNotify(false);
            bgm_image_gameobject.SetActive(false);
        }

        if (AudioManager.instance.playing_sfx)
        {
            sfx_toggle.SetIsOnWithoutNotify(true);
            sfx_image_gameobject.SetActive(true);
        }
        else
        {
            sfx_toggle.SetIsOnWithoutNotify(false);
            sfx_image_gameobject.SetActive(false);
        }

        if (AudioManager.instance.playing_vibration)
        {
            haptic_toggle.SetIsOnWithoutNotify(true);
            haptic_image_gameobject.SetActive(true);
        }
        else
        {
            haptic_toggle.SetIsOnWithoutNotify(false);  
            haptic_image_gameobject.SetActive(false);
        }
    }

    public void Click_btn(int index)
    {
        switch (index)
        {
            case 0:
                Sound_change(true);
                break;
            case 1:
                Sound_change(false);
                break;
            case 2:
                AudioManager.instance.Switchmode_vibration();
                break;
        }
    }

    private void Sound_change(bool isBGM)
    {
        if (isBGM)
        {
            //bgm_toggle.isOn =
            AudioManager.instance.Switchmode_bgm();
            AudioManager.instance.BGM_play();
        }
        else
        {
            //sfx_toggle.isOn =
            AudioManager.instance.Switchmode_sfx();
        }

    }

    public void click_logout_btn() {
        StartCoroutine(logout_co());
    }

    //로그 아웃
    private IEnumerator logout_co() { 
        yield return null;
        Destroy(TCP_Client_Manager.instance.gameObject);
        TCP_Client_Manager.instance = null;

        Destroy(UIManager_YG.Instance.gameObject);
        UIManager_YG.Instance = null;

        Destroy(MoneyManager.instance.gameObject);
        MoneyManager.instance = null;

        Backend.BMember.Logout();

        BackendGameData_JGD.userData = null;
        SceneManager.LoadScene("Logo");
    }

}
