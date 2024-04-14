using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Game ������ ȯ�漳���� �����ϰ� UI�� ����ϴ� Ŭ����.
/// </summary>
public class Ingame_setting : MonoBehaviour
{
    //ȯ�漳�� ���¿� ���� ����� �̹��� ������Ʈ.
    [SerializeField] private List<Image> btn_img = new List<Image>();

    //������Ʈ�� �� ��������Ʈ
    [SerializeField] private Sprite on_sprite;
    [SerializeField] private Sprite off_sprite;

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

        sprite = AudioManager.instance.playing_vibration ? on_sprite : off_sprite;
        btn_img[2].sprite = sprite;
    }

    public void Click_btn(int index) //ȯ�漳�� on,off��ư Ŭ�� �� ����
    {
        switch (index)
        {
            case 0://BGM
                Sound_change(true);
                break;
            case 1://SFX
                Sound_change(false);
                break;
            case 2://����
                AudioManager.instance.Switchmode_vibration();
                break;
        }
    }

    private void Sound_change(bool isBGM) 
    {
        int index;
        bool playing;

        if (isBGM)
        {   //BGM ����
            playing = AudioManager.instance.Switchmode_bgm();
            AudioManager.instance.BGM_play();
            index = 0;
        }
        else
        {   //SFX ����
            playing = AudioManager.instance.Switchmode_sfx();
            index = 1;
        }

        //��������Ʈ ��ü
        Sprite sprite = playing ? on_sprite : off_sprite;
        btn_img[index].sprite = sprite;
    }
}
