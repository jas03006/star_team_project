using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Challenge_prefab : MonoBehaviour
{
    [SerializeField] TMP_Text name_text;

    public void UI_update(string str)
    {
        name_text.text = str;
    }
}
