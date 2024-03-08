using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tutorial_YG : MonoBehaviour
{
    [SerializeField] Image image_50;//터치입력 받음
    [SerializeField] Button btn_50;

    [SerializeField] Image image_100;//터치입력 안받음
    [SerializeField] Image finger;
    [SerializeField] Image touch;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    private bool is_tutorial = true;

    [SerializeField] int click_time = 3;
    [SerializeField]
    int count
    {
        get
        {
            return count_;
        }

        set
        {
            if (is_tutorial)
            {
                count_ = value;
                Progress();
            }
            else
            {
                count_ = 0;
            }
        }
    }
    //터치카운트
    int count_;

    [SerializeField] int[] timings = {7}; //터치입력 안받는 오브젝트 등장 시 타이밍.

    private void Start()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.catchingstar_chapter)
        {
            is_tutorial = false;
            return;
        }

        image_50.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        finger.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        touch.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        image_50.enabled = true;

        image_100.enabled = false;
        finger.enabled = false;
        touch.enabled = false;
    }

    private IEnumerator Timecheck_touch() //btn_50
    {
        if (image_100.enabled)
        {
            image_100.enabled = false;
        }
        
        if (!image_50.enabled)
        {
            image_50.enabled = true;
        }

        image_50.enabled = true;
        btn_50.interactable = false;
        yield return new WaitForSeconds(click_time);

        touch.enabled = true;
        btn_50.interactable = true;
    }

    private IEnumerator Timecheck_nottouch() //btn_50
    {
        if (image_50.enabled)
        {
            image_50.enabled = false;
        }

        if (!image_100.enabled)
        {
            image_100.enabled = true;
        }

        if (image_100.color.a != 0)
        {
            Color color = new Color();
            color.a = 0;
            image_100.color = color;
        }

        yield return new WaitForSeconds(click_time);
        count++;
    }

    public void Count_up()
    {
        if (!is_tutorial)
        { return; }

        count++;
        Debug.Log(count);
    }

    private void Progress()
    {
        Debug.Log(count);

        if (timings.Contains(count))
        {
            Timecheck_nottouch();
        }
        else
        {
            image_50.sprite = sprites[count];
            Timecheck_touch();
        }
    }

}
