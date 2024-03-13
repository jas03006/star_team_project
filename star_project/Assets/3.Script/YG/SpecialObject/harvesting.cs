using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using BackEnd;

public enum harvest_state { 
    None = -1,
    ready = 0,
    processing,
    complete
}

public class Harvesting : Net_Housing_Object//, IObject
{
    [SerializeField] private RectTransform root;
    [SerializeField] private GameObject ready_model;
    [SerializeField] private GameObject processing_model;
    [SerializeField] private GameObject complete_model;

    [SerializeField] private GameObject ready_UI;
    [SerializeField] private GameObject processing_UI;
    [SerializeField] private TMP_Text processing_timer;
    [SerializeField] private GameObject complete_UI;
    [SerializeField] private GameObject result_UI;
    [SerializeField] private TMP_Text result_text;
    [SerializeField] private GameObject select_UI;
    [SerializeField] private GameObject select_UI_Right;

    private harvest_state state = harvest_state.None;

    public DateTime start_time;//버튼 클릭 시간
    private DateTime end_time = DateTime.MaxValue; //수확 가능 시간

    private int[] btn_min = { 1, 30, 120, 720, 1440 };
    private int[] ark_reward = { 1, 10, 30, 100, 200 };
    public int selection = -1;
    private HousingObjectInfo info = null;

    private void Update()
    {
        root.position = Camera.main.WorldToScreenPoint( transform.position) + Vector3.up * (62.5f + 450f/Camera.main.orthographicSize );
        update_state();
    }

    public override void interact(string player_id, int interaction_id = 0, int param = 0)
    {
        base.interact(player_id, interaction_id, param);
        if (state == harvest_state.ready && TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id)
        {
            if (select_UI.activeSelf || select_UI_Right.activeSelf)
            {
                hide_select_UI();
            }
            else {
                show_select_UI();
            }
        }
        else if (state == harvest_state.complete)
        {
            get_result();
            show_result_UI();            
        }
        AudioManager.instance.SFX_hearvest_start_and_get();        
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
                // processing_timer.text  = $"{(remain_time.TotalMinutes < 10 ? "0" : "")}{(int)remain_time.TotalMinutes}:{(remain_time.Seconds < 10 ? "0" : "")}{remain_time.Seconds}";
                if (remain_time.TotalMinutes < 1)
                {
                    processing_timer.text = $"<color=#FF7C44>{remain_time.Seconds}초</color>";
                }
                else {
                    processing_timer.text = $"{(remain_time.TotalHours < 10 ? "0" : "")}{(int)remain_time.TotalHours}:{(remain_time.Minutes < 10 ? "0" : "")}{remain_time.Minutes}";
                }
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

                MoneyManager.instance.Get_Money(Money.ark,(int) (reward*0.3f));
                QuestManager.instance.Check_mission(Criterion_type.proxy_harvesting);
                QuestManager.instance.Check_challenge(Clear_type.proxy_harvesting);

                string separator = "%^";
                //TODO: 우편 보내기
                
                string[] select_temp = { "info" };
                var n_bro = Backend.Social.GetUserInfoByNickName(TCP_Client_Manager.instance.now_room_id);
                string gamer_indate = n_bro.GetReturnValuetoJSON()["row"]["inDate"].ToString();
                               

                PostItem postItem = new PostItem();

                postItem.Title = "대리 수확 보상";
                postItem.Content = $"{TCP_Client_Manager.instance.my_player.object_id}님이 수확해주었습니다!" +
                $"{separator}{(int)Money.ark}:{(int)(reward*1.2f)}";
                postItem.TableName = "USER_DATA";
               if (BackendGameData_JGD.Instance.gameDataRowInDate == string.Empty)
               {
                    
                   //var bro_ = Backend.Social.GetUserInfoByNickName(Backend.UserNickName);
                   //
                   //string gamerIndate = bro_.GetReturnValuetoJSON()["row"]["inDate"].ToString();

                    Where where = new Where();
                    where.Equal("owner_inDate", Backend.UserInDate);// gamerIndate);

                    var bro__ = Backend.GameData.Get("USER_DATA", where);
                    Debug.Log(bro__.FlattenRows()[0]["inDate"].ToString());
                    BackendGameData_JGD.Instance.gameDataRowInDate = bro__.FlattenRows()[0]["inDate"].ToString(); //Backend.GameData.GetMyData("USER_DATA", new Where()).FlattenRows()[0]["inDate"].ToString();
                   
                    postItem.RowInDate = bro__.FlattenRows()[0]["inDate"].ToString();
               }
               else {
                   postItem.RowInDate = BackendGameData_JGD.Instance.gameDataRowInDate;
               }
                
                postItem.Column = "level";

                Debug.Log(gamer_indate);
                Debug.Log(postItem.RowInDate);

                var bro = Backend.UPost.SendUserPost(gamer_indate, postItem);
                if (bro.IsSuccess())
                {
                    Debug.Log("우편 발송에 성공했습니다." + bro);
                    BackendGameData_JGD.userData.level = 1;
                    string[] selection_ = { "level"};
                    BackendGameData_JGD.Instance.GameDataUpdate(selection_);
                }
                else
                {
                    Debug.LogError("우편 발송에 실패했습니다." + bro);
                }
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
        if (Screen.width-Camera.main.WorldToScreenPoint(transform.position).x < 500 ) {
            select_UI_Right.SetActive(true);
            select_UI.SetActive(false);
        }
        else {
            select_UI.SetActive(true);
            select_UI_Right.SetActive(false);
        }
    }
    public void hide_select_UI()
    {
        select_UI.SetActive(false);
        select_UI_Right.SetActive(false);
    }

    public void show_ready_UI()
    {
        ready_UI.SetActive(true);
        processing_UI.SetActive(false);
        complete_UI.SetActive(false);

        ready_model.SetActive(true);
        processing_model.SetActive(false);
        complete_model.SetActive(false);
    }
    public void show_processing_UI()
    {
        ready_UI.SetActive(false);
        processing_UI.SetActive(true);
        complete_UI.SetActive(false);

        ready_model.SetActive(false);
        processing_model.SetActive(true);
        complete_model.SetActive(false);
    }
    public void show_complete_UI()
    {
        ready_UI.SetActive(false);
        processing_UI.SetActive(false);
        complete_UI.SetActive(true);

        ready_model.SetActive(false);
        processing_model.SetActive(false);
        complete_model.SetActive(true);
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

}
