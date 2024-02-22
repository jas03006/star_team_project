using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Challenge_detail_prefab : MonoBehaviour
{
    [SerializeField] TMP_Text contents_text;
    [SerializeField] TMP_Text count_text;

    public void UI_update(string str1, string str2, int cur ,int max)
    {
        contents_text.text = str1;
        count_text.text = str2 + $"{cur} / {max}";
    }
}
