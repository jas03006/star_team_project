using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginBase_JGD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMessage;

    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }

    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }

    protected void GuideForIncorrenctltEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }

    protected bool IsFieldDateEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForIncorrenctltEnteredData(image, $"\"{result}\"필드를 채워주세요.");

            return true;
        }
        return false;
    }


}