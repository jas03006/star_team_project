using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI_TG : MonoBehaviour
{
    [SerializeField] private Toggle bgm_toggle;
    [SerializeField] private Toggle sfx_toggle;
    [SerializeField] private Toggle haptic_toggle;
    // Start is called before the first frame update
    void Start()
    {
        update_UI();
    }

    public void update_UI()
    {
        if (AudioManager.instance.playing_bgm)
        {
            bgm_toggle.isOn = true;
        }
        else {
            bgm_toggle.isOn = false;
        }

        if (AudioManager.instance.playing_sfx)
        {
            sfx_toggle.isOn = true;

        }
        else
        {
            sfx_toggle.isOn = false;
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
                Debug.Log("진동 기능 준비중");
                break;
        }
    }

    private void Sound_change(bool isBGM)
    {
        if (isBGM)
        {
            bgm_toggle.isOn =  AudioManager.instance.Switchmode_bgm();
            AudioManager.instance.BGM_play();
        }
        else
        {
            sfx_toggle.isOn = AudioManager.instance.Switchmode_sfx();
        }

    }

}
