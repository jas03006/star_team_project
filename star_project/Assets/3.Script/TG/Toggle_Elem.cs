using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//리스트 요소에 토글 기능을 편하게 만들 수 있도록 하는 클래스
public class Toggle_Elem : MonoBehaviour
{
    public int value;
    public TMP_Text text;
    public Toggle toggle;

    public void deactive() {
        toggle.isOn = false;
        toggle.interactable = false;
    }

    public void active() {
        toggle.interactable = true;
    }

    public void select()
    {
        toggle.isOn = true;
    }

    public void deselect() {
        toggle.isOn = false;
    }
}
