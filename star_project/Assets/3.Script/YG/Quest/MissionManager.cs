using BackEnd;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum mission_state
{
    daily,
    week,
    month
}

public class MissionManager : MonoBehaviour
{
    mission_state state
    {
        get { return state_; }
        set
        {
            state_ = value;
            Reset_btn();
        }
    }
    private mission_state state_;
    List<Mission> missions_daily = new List<Mission>();
    List<Mission> missions_week = new List<Mission>();
    List<Mission> missions_month = new List<Mission>();
    Mission cur_mission;

    [Header("Left_UI")]
    [SerializeField] private TMP_Text s_title;
    [SerializeField] private TMP_Text contents;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private Button accept_btn;
    [SerializeField] private Button reward_btn;

    [Header("Right_UI")]
    [SerializeField] private List<TMP_Text> mission_names;
    [SerializeField] private List<Image> images;
    private int index;

    private void Start()
    {
        Setting();
        Reset_btn();
    }

    private void Setting()//�̼� ������ �ҷ�����
    {
        List<Mission> missions = BackendChart_JGD.chartData.mission_list;

        foreach (var mission in missions)
        {
            switch (mission.type)
            {
                case MissionType.daily:
                    missions_daily.Add(mission);
                    break;
                case MissionType.week:
                    missions_week.Add(mission);
                    break;
                case MissionType.month:
                    missions_month.Add(mission);
                    break;
                default:
                    break;
            }
        }
        state = mission_state.daily;
        reward_btn.enabled = false;
    }

    private void UI_updateR()
    {
        List<Mission> missions = new List<Mission>();

        switch (state)
        {
            case mission_state.daily:
                missions = missions_daily;
                break;
            case mission_state.week:
                missions = missions_week;
                break;
            case mission_state.month:
                missions = missions_month;
                break;
            default:
                break;
        }

        for (int i = 0; i < missions.Count; i++)
        {
            mission_names[i].text = missions[i].title;
            //images[i].enabled = missions[i].userdata.is_clear;
        }
    }

    private void UI_updateL()
    {
        switch (state)
        {
            case mission_state.daily:
                cur_mission = missions_daily[index];
                break;
            case mission_state.week:
                cur_mission = missions_week[index];
                break;
            case mission_state.month:
                cur_mission = missions_month[index];
                break;
            default:
                break;
        }
        s_title.text = cur_mission.title;
        contents.text = cur_mission.contents;
        reward.text = $"���� : �� x {cur_mission.reward_gold} �� x {cur_mission.reward_ark}";
        //accept_btn.enabled = !cur_mission.userdata.is_accept;
    }

    #region btn

    public void Set_index(int i)
    {
        index = i;
        Debug.Log(index);
        UI_updateL();
    }
    //public void Accept_btn() //������ư Ŭ�� �� ȣ��
    //{
    //    cur_mission.userdata.is_accept = true;
    //}

    //public void criterionUp_btn() //����ġ��� ��ư Ŭ���� ȣ�� - ����
    //{
    //    cur_mission.userdata.criterion++;
    //    reward_btn.enabled = cur_mission.Check_clear();
    //    Reset_btn();
    //    cur_mission.userdata.Data_update();
    //}

    public void Reset_btn() // ���¹�ư Ŭ�� �� ȣ��
    {
        UI_updateL();
        UI_updateR();
    }

    public void Change_state(int i) //����,�ְ�,���� ���� Ŭ�� �� ȣ��
    {
        state = (mission_state)i;
        Debug.Log(state);
        Reset_btn();
    }

    //public void Get_reward() //����ȹ�� ��ư
    //{
    //    MoneyManager.instance.Get_Money(Money.gold, cur_mission.reward_gold);
    //    MoneyManager.instance.Get_Money(Money.ark, cur_mission.reward_ark);
    //    cur_mission.userdata.get_rewarded = true;
    //    reward_btn.enabled = false;
    //    cur_mission.userdata.Data_update();
    //}
}
#endregion
