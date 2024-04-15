using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 튜토리얼 첫번째 분기파트를 진행시키는 클래스
/// </summary>
public class Tutorial_YG : MonoBehaviour
{
    [SerializeField] GameObject gameobject_50;//터치입력 받음
    [SerializeField] Image image_50;
    [SerializeField] Button btn_50;
    [SerializeField] GameObject not_click;

    [SerializeField] GameObject image_100;
    [SerializeField] GameObject image_100_btn;
    [SerializeField] Button btn_100;
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

    [SerializeField] int[] click = { 6, 18 };
    [SerializeField] int[] solo_timings = { 7 }; //터치입력 안받는 오브젝트만 등장 시 카운트 타이밍
    [SerializeField] int[] together_timings = { 4, 5, 8, 9, 10, 11, 12, 16, 17 }; //둘다 등장 시 카운트 타이밍


    private void Start()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.catchingstar_chapter)
        {
            is_tutorial = false;
            return;
        }
        //초기세팅

        image_50 = gameobject_50.GetComponent<Image>();
        image_50.alphaHitTestMinimumThreshold = 0.5f;

        touch.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        gameobject_50.SetActive(true);

        image_100.SetActive(false);
        touch.SetActive(false);
        stage_mission.SetActive(false);

        select_character.onClick.AddListener(Progress);
        select_character.onClick.AddListener(Stop_blink_btn);

        x_btn.onClick.AddListener(Count_up);
        x_btn.onClick.AddListener(Progress);
        x_btn.onClick.AddListener(Stop_blink_btn);

        is_blinking = false;
        Count_up();
    }

    private IEnumerator Timecheck_touch_co(bool together) //btn_50
    {
        image_50.sprite = sprites[count];
        btn_50.interactable = false;

        yield return new WaitForSeconds(click_time);

        StartCoroutine(Blink_co());

        btn_50.interactable = true;
        btn_100.interactable = true;
    }

    private IEnumerator Timecheck_nottouch_co(bool together) //btn_100
    {
        yield return new WaitForSeconds(click_time);

        if (!together)
        {
            Count_up();
            Progress();
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
        //Debug.Log("++");
        if (!is_tutorial)
        { return; }

        count++;
    }

    public void Progress()
    {
        btn_100.interactable = false;
        btn_50.interactable = false;

        Debug.Log(count);

        if (!is_tutorial)
        {
            return;
        }

        not_click.SetActive(!click.Contains(count));

        if (count >= sprites.Count)
        {
            Tuto_clear();
        }

        Stop_blink_btn();
        //Debug.Log("카운트" + count);
        image_100_btn.SetActive(false);

        if (solo_timings.Contains(count))//터치X
        {
            Debug.Log("solo_timings");

            image_100.SetActive(true);
            gameobject_50.SetActive(false);

            StartCoroutine(Timecheck_nottouch_co(false));
        }

        else if (together_timings.Contains(count))//터치
        {
            Debug.Log("together_timings");
            image_100_btn.SetActive(true);
            gameobject_50.SetActive(true);

            StartCoroutine(Timecheck_touch_co(true));
        }

        else//터치
        {
            Debug.Log("해당X");
            image_100.SetActive(false);
            gameobject_50.SetActive(true);
            StartCoroutine(Timecheck_touch_co(false));
        }
    }

    public void Tuto_clear()//클리어 시 활성화
    {
        Debug.Log("Tuto_clear");
        BackendGameData_JGD.userData.tutorial_Info.state++;
        BackendGameData_JGD.Instance.GameDataUpdate();
        SceneManager.LoadScene("Tutorial");
    }

}
