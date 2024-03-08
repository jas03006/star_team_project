using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
