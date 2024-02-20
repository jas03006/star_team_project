using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public enum Mission_state
{
    daily,
    week,
    month
}

public class MissionManager : MonoBehaviour
{
    Mission_state state = Mission_state.daily;
    List<Mission> missions_daily = new List<Mission>();
    List<Mission> missions_week = new List<Mission>();
    List<Mission> missions_month = new List<Mission>();
    Mission cur_mission;

    [Header("Left_UI")]
    [SerializeField] private TMP_Text s_title;
    [SerializeField] private TMP_Text contents;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private Button accept_btn;

    [Header("Right_UI")]
    [SerializeField] private List<TMP_Text> mission_names;

    private void Start()
    {
        Setting();
    }

    private void Setting()
    {
        //�̼� ������ �ҷ�����
        state = Mission_state.daily;
    }

    private void UI_updateR()
    {
        List<Mission> missions = new List<Mission>();

        switch (state)
        {
            case Mission_state.daily:
                missions = missions_daily;
                break;
            case Mission_state.week:
                missions = missions_week;
                break;
            case Mission_state.month:
                missions = missions_month;
                break;
            default:
                break;
        }

        for (int i = 0; i < missions.Count; i++)
        {
            mission_names[i].text = missions[i].title;
        }
    }

    private void U_updateL()
    {
        s_title.text = cur_mission.title;
        contents.text = cur_mission.contents;
        reward.text = $"���� : <color = yellow>��</color> x {cur_mission.reward_gold} <color = red>��</color> x {cur_mission.reward_ark}";
        accept_btn.enabled = !cur_mission.userdata.is_accept;
    }

    public void Accept_btn() //������ư Ŭ�� �� ȣ��
    {
        cur_mission.userdata.is_accept = true;
    }

    public void criterionUp_btn() //����ġ��� ��ư Ŭ���� ȣ�� - ����
    {
        cur_mission.userdata.criterion++;
    }

    public void Reset_btn() // ���¹�ư Ŭ�� �� ȣ��
    {
        U_updateL();
        UI_updateR();
    }
}
