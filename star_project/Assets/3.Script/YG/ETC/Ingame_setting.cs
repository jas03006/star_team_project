using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingame_setting : MonoBehaviour
{

    [Header("BTN")]
    private List<Image> btn_img = new List<Image>();
    private Sprite on_sprite;
    private Sprite off_sprite;

    private bool on_bgm = true;
    private bool on_sfx = true;
    private bool on_haptic = true;

    private void Click_btn(int index)
    {

    }

    private void BGM_change(bool state)
    {
        
    }
}
