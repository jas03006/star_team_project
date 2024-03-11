using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial_YG : MonoBehaviour
{
    [SerializeField] GameObject gameobject_50;//터치입력 받음
    [SerializeField] Image image_50;
    [SerializeField] Button btn_50;

    [SerializeField] GameObject image_100;//터치입력 안받음
    [SerializeField] GameObject touch; //깜빡거릴 게임오브젝트

    [SerializeField] Button select_character;
    [SerializeField] GameObject stage_mission;
    [SerializeField] Button x_btn;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    public bool is_tutorial = true;

    [SerializeField] float click_time = 3f;
    [SerializeField] float blink_time = 0.5f;//깜빡거리는 시간

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
    bool is_blinking = false;

    [SerializeField] int[] finger_timings = { 6, 13, 18 };
    [SerializeField] int[] solo_timings = { 7 }; //터치입력 안받는 오브젝트만 등장 시 카운트 타이밍
    [SerializeField] int[] together_timings = { 4, 5, 9, 10, 11, 12, 16, 17 }; //둘다 등장 시 카운트 타이밍


    private void Start()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.catchingstar_chapter)
        {
            is_tutorial = false;
            Debug.Log("catchingstar_chapter 튜토리얼 스킵");
            return;
        }

        Debug.Log("catchingstar_chapter 튜토리얼 시작!");

        image_50 = gameobject_50.GetComponent<Image>();
        image_50.alphaHitTestMinimumThreshold = 0.5f;

        touch.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        gameobject_50.SetActive(true);

        image_100.SetActive(false);
        touch.SetActive(false);
        stage_mission.SetActive(false);

        select_character.onClick.AddListener(Progress);
        select_character.onClick.AddListener(Stop_blink_btn);

        x_btn.onClick.AddListener(Progress);
        x_btn.onClick.AddListener(Count_up);
        x_btn.onClick.AddListener(Stop_blink_btn);

        is_blinking = false;
        Count_up();
    }

    private IEnumerator Timecheck_touch_co(bool together) //btn_50
    {
        image_50.sprite = sprites[count];

        btn_50.interactable = false;

        //Debug.Log("3초시작");
        yield return new WaitForSeconds(click_time);

        //Debug.Log("3초끝");
        StartCoroutine(Blink_co());

        if (!finger_timings.Contains(count))
        {
            btn_50.interactable = true;
        }

    }

    private IEnumerator Timecheck_nottouch_co(bool together) //btn_100
    {
        yield return new WaitForSeconds(click_time);

        if (!together)
        {
            Count_up();
        }
    }

    public void Stop_blink_btn() //btn_50
    {
        //Debug.Log("Stop_blink_btn");
        if (!is_tutorial)
            return;
        is_blinking = false;
    }

    private IEnumerator Blink_co()
    {
        // Debug.Log("Blink_co");

        is_blinking = true;

        while (is_blinking)
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
    }

    private void Progress()
    {
        if (!is_tutorial)
        {
            return;
        }

        if (count >= sprites.Count)
        {
            Tuto_clear();
        }

        Stop_blink_btn();
        //Debug.Log("카운트" + count);

        if (solo_timings.Contains(count))
        {
            image_100.SetActive(true);
            gameobject_50.SetActive(false);
            //Debug.Log("안터치");
            StartCoroutine(Timecheck_nottouch_co(false));
        }

        else if (together_timings.Contains(count))
        {
            image_100.SetActive(true);
            gameobject_50.SetActive(true);

            //Debug.Log("둘다");
            StartCoroutine(Timecheck_nottouch_co(true));
            StartCoroutine(Timecheck_touch_co(true));
        }

        else
        {
            image_100.SetActive(false);
            gameobject_50.SetActive(true);
            //Debug.Log("터치");
            StartCoroutine(Timecheck_touch_co(false));

        }
    }

    public void Tuto_clear()
    {
        Debug.Log("Tuto_clear");
        BackendGameData_JGD.userData.tutorial_Info.state++;
        BackendGameData_JGD.Instance.GameDataUpdate();
        SceneManager.LoadScene("Tutorial");
    }

}
