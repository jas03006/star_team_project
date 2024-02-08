using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harvesting : MonoBehaviour, IObject
{
    private int ark//��ȭ
    {
        get { return ark_val; }
        set
        {
            ark_val = value;
            ark_text.text = $"{ark_val}";
        }
    }

    private DateTime start_time//��ư Ŭ�� �ð�
    {
        get { return start_time_val; }
        set
        {
            start_time_val = value;
            click_time.text = $"{start_time_val}";
            Debug.Log(start_time_val);
        }
    }

    private bool can_harvest//��Ȯ ���� ����
    {
        get { return can_harvest_val; }
        set
        {
            can_harvest_val = value;
            if (can_harvest)
            {
                reward_btn.SetActive(true);
            }
            else
            {
                reward_btn.SetActive(false);
            }
        }
    }
    private int reward;//��Ȯ �� ����
    private DateTime end_time; //��Ȯ ���� �ð�

    private int ark_val = 0;
    private DateTime start_time_val;
    private bool can_harvest_val;



    [Header("UI")]
    [SerializeField] private GameObject end_btn;//�ð� ���� ��ư
    [SerializeField] private GameObject setting_veiw;

    [SerializeField] private TMP_Text ark_text;
    [SerializeField] private TMP_Text click_time;
    [SerializeField] private TMP_Text select_time;
    [SerializeField] private TMP_Text timer;

    [SerializeField] private GameObject reward_btn;

    public void Interactive()
    {
        if (setting_veiw.activeSelf)
        {
            return;
        }

        end_btn.SetActive(true);

        start_time = DateTime.Now;
        ark = 0;
        can_harvest = false;
    }

    public void Set_endTime(int num) //��ư�� �ڱ� ���� �Լ������ϰ� setting_viewŰ��
    {
        //end_time
        end_time = start_time.AddMinutes(num);

        //click_time + UIupdate
        click_time.text = start_time.ToString("MM/dd/yyyy HH:mm:ss");


        //set reward + select_time + UIupdate(DB�� �����ϱ��� �ھƳֱ�)
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

    public void Get_reward()
    {
        ark += reward;
    }
}
