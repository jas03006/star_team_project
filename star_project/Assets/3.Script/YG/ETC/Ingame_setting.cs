using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame_setting : MonoBehaviour
{

    [Header("BTN")]
    [SerializeField] private List<Image> btn_img = new List<Image>();
    [SerializeField] private Sprite on_sprite;
    [SerializeField] private Sprite off_sprite;

    //추가할 예정
    private bool on_haptic = true;

    public void Start()
    {
        First_setting();
    }
    public void First_setting()
    {
        Sprite sprite = AudioManager.instance.playing_bgm ? on_sprite : off_sprite;
        btn_img[0].sprite = sprite;

        sprite = AudioManager.instance.playing_sfx ? on_sprite : off_sprite;
        btn_img[1].sprite = sprite;
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
        int index;
        bool playing;

        if (isBGM)
        {
            playing = AudioManager.instance.Switchmode_bgm();
            AudioManager.instance.BGM_play();
            index = 0;
        }
        else
        {
            playing = AudioManager.instance.Switchmode_sfx();
            index = 1;
        }

        Sprite sprite = playing ? on_sprite : off_sprite;
        btn_img[index].sprite = sprite;
    }
}
