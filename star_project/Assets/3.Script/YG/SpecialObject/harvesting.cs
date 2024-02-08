using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harvesting : MonoBehaviour, IObject
{
    private int ark = 0; //재화
    private DateTime start_time = DateTime.Now; //버튼 클릭 시간
    private DateTime end_time = DateTime.Now.AddYears(1); //수확 가능 시간
    private bool can_harvest//수확 가능 여부
    {
        get { return Can_harvest; }
        set
        {
            Can_harvest = value;
            if (can_harvest)
            {
                reward_btn.enabled = true;
            }
        }
    }
    private bool Can_harvest = false;
    private int reward;//수확 시 보상

    [Header("UI")]
    [SerializeField] private GameObject end_btn;//시간 설정 버튼

    [SerializeField] private TMP_Text ark_text;
    [SerializeField] private TMP_Text click_time;
    [SerializeField] private TMP_Text select_time;
    [SerializeField] private TMP_Text timer;

    [SerializeField] private Button reward_btn;

    public Harvesting(int ark, DateTime start_time, DateTime end_time, bool can_harvest)
    {
        this.ark = ark;
        this.start_time = start_time;
        this.end_time = end_time;
        this.can_harvest = can_harvest;

        if (end_time <= start_time)
        {
            can_harvest = true;
        }
    }

    public void Interactive()
    {
        end_btn.SetActive(true);
    }

    public void Set_endTime(int num) //버튼에 자기 끄고 함수실행하고 setting_view키기
    {
        //end_time
        end_time = start_time.AddMinutes(num);

        //click_time + UIupdate
        click_time.text = $"{start_time}";


        //set reward + select_time + UIupdate(DB랑 연동하기전 박아넣기)
        string btn_input = string.Empty;
        switch (num)
        {
            case 1:
                reward = 100;
                 btn_input = "1min";
                break;
            case 5:
                reward = 200;
                 btn_input = "5min";
                break;
            case 30:
                reward = 1000;
                 btn_input = "30min";
                break;
            case 1440:
                reward = 10000;
                 btn_input = "1day";
                break;
            default:
                return;
        }
        select_time.text = btn_input;
        StartCoroutine(Timer_update(btn_input));
    }

    IEnumerator Timer_update(string btn_input)
    {
        TimeSpan time = end_time - start_time;
        timer.text = $"{time}";

        if (time < TimeSpan.Zero)
        {
            can_harvest = true;
        }
        yield return new WaitForSeconds(1f);
    }

    public void Get_reward()
    {
        ark += reward;
        ark_text.text = $"{ark}";
    }
}
