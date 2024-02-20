using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Harvesting : SpecialObj, IObject
{
    private int ark//재화
    {
        get { return ark_val; }
        set
        {
            ark_val = value;
            ark_text.text = $"{ark_val}";
        }
    }
    private DateTime start_time//버튼 클릭 시간
    {
        get { return start_time_val; }
        set
        {
            start_time_val = value;
            click_time.text = $"{start_time_val}";
        }
    }
    private bool can_harvest//수확 가능 여부
    {
        get { return can_harvest_val; }
        set
        {
            can_harvest_val = value;
            reward_btn.SetActive(can_harvest);
        }
    }

    private int reward;//수확 시 보상
    private DateTime end_time; //수확 가능 시간

    private int ark_val = 0;
    private DateTime start_time_val;
    private bool can_harvest_val;

    [Header("UI")]
    [SerializeField] private GameObject end_btn;//시간 설정 버튼
    [SerializeField] private GameObject setting_veiw;

    [SerializeField] private TMP_Text ark_text;
    [SerializeField] private TMP_Text click_time;
    [SerializeField] private TMP_Text select_time;
    [SerializeField] private TMP_Text timer;

    [SerializeField] private GameObject reward_btn;

    private void Start()
    {
        First_setting();
    }

    private void First_setting()
    {
        ark = 0;
    }
    public void Interactive()
    {
        if (setting_veiw.activeSelf)
        {
            return;
        }

        end_btn.SetActive(true);
    }

    public void init(DateTime starttime_, int min_)
    {
        start_time = starttime_;
        end_time = start_time.AddMinutes(min_);
    }
    public bool can_be_harvest()
    {
        TimeSpan time = end_time - DateTime.Now;

        if (time <= TimeSpan.Zero)
        {
            return true;
        }
        return false;
    }
    public void Set_startTime(DateTime starttime_)
    {
        start_time = starttime_;
    }
    public void Set_endTime(DateTime endtime_) {
        end_time = endtime_;
    }
    public void Set_endTime(int num) //시간 설정버튼 누르면 실행
    {
        //reward_btn 끄기
        can_harvest = false;

        //start_time
        start_time = DateTime.Now;

        //end_time
        end_time = start_time.AddMinutes(num);

        //click_time + UIupdate
        click_time.text = start_time.ToString("MM/dd/yyyy HH:mm:ss");

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
        StartCoroutine(Timer_update());
    }

    IEnumerator Timer_update()
    {
        while (true)
        {
            TimeSpan time = end_time - DateTime.Now;
            timer.text = $"{time}";

            if (time < TimeSpan.Zero)
            {
                can_harvest = true;
                StopCoroutine(Timer_update());
            }
            yield return new WaitForSeconds(1f);
        }
    }

  

    public void Get_reward() //보상획득 버튼 누르면 실행
    {
        ark += reward;
        Debug.Log($"{ark}");
    }
}
