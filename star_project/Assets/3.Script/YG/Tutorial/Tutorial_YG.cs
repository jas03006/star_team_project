using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tutorial_YG : MonoBehaviour
{
    [SerializeField] GameObject gameobject_50;//��ġ�Է� ����
    [SerializeField] Image image_50;
    [SerializeField] Button btn_50;

    [SerializeField] GameObject image_100;//��ġ�Է� �ȹ���
    [SerializeField] GameObject finger;
    [SerializeField] GameObject touch; //�����Ÿ� ���ӿ�����Ʈ

    [SerializeField] Button select_character;
    [SerializeField] GameObject stage_mission;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    private bool is_tutorial = true;
    

    [SerializeField] int click_time = 3;
    [SerializeField] int blink_time = 1;//�����Ÿ��� �ð�

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
    //��ġī��Ʈ
    int count_;
    Coroutine Blink_corutine;
    bool is_blinking;

    [SerializeField] int[] timings = {7}; //��ġ�Է� �ȹ޴� ������Ʈ ���� �� Ÿ�̹�.

    private void Start()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.catchingstar_chapter)
        {
            is_tutorial = false;
            Debug.Log("catchingstar_chapter Ʃ�丮�� ��ŵ");
            return;
        }

        Debug.Log("catchingstar_chapter Ʃ�丮�� ����!");

        image_50 = gameobject_50.GetComponent<Image>();
        image_50.alphaHitTestMinimumThreshold = 0.5f;

        finger.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        touch.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        gameobject_50.SetActive(true);

        image_100.SetActive(false);
        finger.SetActive(false);
        touch.SetActive(false);
        stage_mission.SetActive(false);

        select_character.onClick.AddListener(Progress);
        Blink_corutine = StartCoroutine(Blink_co());
    }

    private IEnumerator Timecheck_touch_co() //btn_50
    {
        if (image_100.activeSelf)
        {
            image_100.SetActive(false);
        }
        
        if (!gameobject_50.activeSelf)
        {
            gameobject_50.SetActive(true);
        }

        gameobject_50.SetActive(true);
        btn_50.interactable = false;
        yield return new WaitForSeconds(click_time);

        Blink_co();
        btn_50.interactable = true;
    }

    private IEnumerator Timecheck_nottouch_co() //btn_100
    {
        if (gameobject_50.activeSelf)
        {
            gameobject_50.SetActive(false);
        }

        if (!image_100.activeSelf)
        {
            image_100.SetActive(true);
        }

        if (image_100.GetComponent<Image>().color.a != 0)
        {
            Color color = new Color();
            color.a = 0;
            image_100.GetComponent<Image>().color = color;
        }

        yield return new WaitForSeconds(click_time);
        count++;
    }

    private void Stop_blink()
    {
        if (is_blinking)
        {
            Stop_blink();
        }
    }
    private IEnumerator Blink_co()
    {
        while (true)
        {
            touch.SetActive(true);
            yield return new WaitForSeconds(blink_time);
            touch.SetActive(false);
            yield return new WaitForSeconds(blink_time);
        }
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
            Timecheck_nottouch_co();
        }
        else
        {
            image_50.sprite = sprites[count];
            Timecheck_touch_co();
        }
    }

}
