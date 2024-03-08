using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Supporter : MonoBehaviour
{
    public void hide_UI_ob(GameObject go)
    {
        go.SetActive(false);
    }
    public void show_UI_ob(GameObject go)
    {
        go.SetActive(true);
    }
}
