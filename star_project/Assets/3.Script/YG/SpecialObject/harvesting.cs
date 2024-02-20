using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.InputManagerEntry;

public enum harvest_state { 
    None = -1,
    ready = 0,
    processing,
    complete
}

public class Harvesting : SpecialObj//, IObject
{
    [SerializeField] private RectTransform root;
    [SerializeField] private GameObject ready_UI;
    [SerializeField] private GameObject processing_UI;
    [SerializeField] private TMP_Text processing_timer;
    [SerializeField] private GameObject complete_UI;
    [SerializeField] private GameObject result_UI;
    [SerializeField] private TMP_Text result_text;
    [SerializeField] private GameObject select_UI;

    private harvest_state state = harvest_state.None;

    public DateTime start_time;//버튼 클릭 시간
    private DateTime end_time = DateTime.MaxValue; //수확 가능 시간

    private int[] btn_min = { 1, 2, 30, 120 };
    private int[] ark_reward = { 1, 2, 30, 120 };
    public int selection = -1;
    private HousingObjectInfo info = null;

    private void Update()
    {
        root.position = Camera.main.WorldToScreenPoint( transform.position) + Vector3.up * 50f;
        update_state();
    }

    public override void interact(string player_id, int interaction_id = 0, int param = 0)
    {
        base.interact(player_id, interaction_id, param);
        if (state == harvest_state.ready && TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id)
        {
            show_select_UI();
        }
        else if (state == harvest_state.complete)
        {
            get_result();
            show_result_UI();            
        }
    }
    public void init(DateTime starttime_, int selection_)
    {
        start_time = starttime_;
        selection = selection_;
        if (start_time!= DateTime.MaxValue && selection >= 0 && selection < btn_min.Length)
        {
            end_time = start_time.AddMinutes(btn_min[selection]);
        }
        else {
            end_time = start_time;
        }
       
        update_state();
    }

    public void init(int selection_)
    {
        if (selection_ < 0 && selection_ >= btn_min.Length)
        {
            return;
        }
        if (btn_min[selection_] <= 0) {
            return;
        }

        selection = selection_;
        start_time = DateTime.Now;
        end_time = start_time.AddMinutes(btn_min[selection_]);
        update_state();
    }

    public void init()
    {
        start_time = DateTime.MaxValue;
        end_time = start_time;
        selection = -1;
        state = harvest_state.ready;
    }
    public void process_state(TimeSpan remain_time) {
        switch (state)
        {
            case harvest_state.None:
                hide_all_UI();
                break;
            case harvest_state.ready:
                show_ready_UI();
                break;
            case harvest_state.processing:
                show_processing_UI();
                processing_timer.text  = $"{(int)remain_time.TotalMinutes}:{remain_time.Seconds}";
                break;
            case harvest_state.complete:
                show_complete_UI();
                break;
            default:
                break;
        }

    }
    public void update_state()
    {
        TimeSpan remain_time = end_time - DateTime.Now;
        if (start_time == DateTime.MaxValue)
        {
            state = harvest_state.ready;
        }
        else if (remain_time <= TimeSpan.Zero)
        {
            state = harvest_state.complete;
        }
        else {
            state = harvest_state.processing;
        }
        process_state(remain_time);
    }
    public void get_result() {
        if (state == harvest_state.complete) {
            int reward = ark_reward[selection];
            if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id)
            {
                //TODO: 재화 획득
                MoneyManager.instance.Get_Money(Money.ark, reward);
            }
            else {
                //TODO: 우편 보내기

            }

            result_text.text = $"Earn {reward} Ark!";
            init();
            save_info();
        }
    }

    public void click_harvest_btn(int ind) {
        init(ind);
        save_info();
    }

    public void save_info()
    {
        
        TCP_Client_Manager.instance.placement_system.save_edit(TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id);
       
        //BackendGameData_JGD.Instance.update_userdata_by_nickname();
    }




    #region UI
    public void hide_all_UI()
    {
        select_UI.SetActive(false);
        ready_UI.SetActive(false);
        processing_UI.SetActive(false);
        complete_UI.SetActive(false);
    }
    public void show_select_UI() {
        select_UI.SetActive(true);
    }
    public void hide_select_UI()
    {
        select_UI.SetActive(false);
    }

    public void show_ready_UI()
    {
        ready_UI.SetActive(true);
        processing_UI.SetActive(false);
        complete_UI.SetActive(false);
    }
    public void show_processing_UI()
    {
        ready_UI.SetActive(false);
        processing_UI.SetActive(true);
        complete_UI.SetActive(false);

    }
    public void show_complete_UI()
    {
        ready_UI.SetActive(false);
        processing_UI.SetActive(false);
        complete_UI.SetActive(true);
    }

    public void show_result_UI() {
        StartCoroutine(show_result_UI_co());
    }

    public IEnumerator show_result_UI_co() { 
        result_UI.SetActive(true);
        yield return new WaitForSeconds(2);
        result_UI.SetActive(false);
    }
    

    #endregion
/*
    private int ark//재화
    {
        get { return ark_val; }
        set
        {
            ark_val = value;
            ark_text.text = $"{ark_val}";
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
*/


/*



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
    }*/
}
