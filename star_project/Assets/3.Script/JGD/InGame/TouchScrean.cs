using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScrean : MonoBehaviour
{
    //�̹��� ����ȭ�� �� ��ġ�Ҽ��ְ� �ϱ�
    public Image image;
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
}
