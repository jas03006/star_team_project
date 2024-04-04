using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 대부분의 UI 요소의 공통된 기능 (출력/숨김)을 담당하는 클래스
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
