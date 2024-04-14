using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScrean : MonoBehaviour
{
    //이미지 투명화된 곳 터치할수있게 하기
    public Image image;
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
}
