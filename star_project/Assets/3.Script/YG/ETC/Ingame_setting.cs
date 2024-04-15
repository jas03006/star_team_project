using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Game 씬에서 환경설정을 진행하고 UI를 출력하는 클래스.
/// </summary>
public class Ingame_setting : MonoBehaviour
{
    //환경설정 상태에 따라 변경될 이미지 컴포넌트.
    [SerializeField] private List<Image> btn_img = new List<Image>();

    //컴포넌트에 들어갈 스프라이트
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

    public void Click_btn(int index) //환경설정 on,off버튼 클릭 시 실행
    {
        switch (index)
        {
            case 0://BGM
                Sound_change(true);
                break;
            case 1://SFX
                Sound_change(false);
                break;
            case 2://진동
                AudioManager.instance.Switchmode_vibration();
                break;
        }
    }

    private void Sound_change(bool isBGM) 
    {
        int index;
        bool playing;

        if (isBGM)
        {   //BGM 변경
            playing = AudioManager.instance.Switchmode_bgm();
            AudioManager.instance.BGM_play();
            index = 0;
        }
        else
        {   //SFX 변경
            playing = AudioManager.instance.Switchmode_sfx();
            index = 1;
        }

        //스프라이트 교체
        Sprite sprite = playing ? on_sprite : off_sprite;
        btn_img[index].sprite = sprite;
    }
}
