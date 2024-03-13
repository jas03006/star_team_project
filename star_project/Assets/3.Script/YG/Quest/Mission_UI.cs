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

public class Mission_UI : MonoBehaviour
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
    [SerializeField] private TMP_Text sub_text;
    [SerializeField] private TMP_Text reward_gold;
    [SerializeField] private TMP_Text reward_ark;
    [SerializeField] private Button accept_btn;
    [SerializeField] private Image accept_img;
    [SerializeField] private Button reward_btn;

    [Header("Right_UI")]
    [SerializeField] private List<TMP_Text> mission_names;
    [SerializeField] private List<GameObject> missionlist;

    [SerializeField] private Sprite can_accept;
    [SerializeField] private Sprite complete_accept;
    [SerializeField] private Sprite can_reward;
    [SerializeField] private Sprite is_completed;

    [Header("index")]
    [SerializeField] private List<Image> btn_image = new List<Image>();

    private int index;

    private void Start()
    {
        Setting();
        Reset_btn();
    }

    private void Setting()
    {
        foreach (Mission mission in QuestManager.instance.missions)
        {
            switch (mission.mission_type)
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

    private void UI_updateR() //미션 리스트 업데이트
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

        for (int i = 0; i < missionlist.Count; i++)
        {
            if (i+1 <= missions.Count)
            {
                missionlist[i].SetActive(true);
                mission_names[i].text = missions[i].title;
            }

            else
            {
                missionlist[i].SetActive(false);
            }
        }
    }

    private void UI_updateL() //현재 선택한 미션 업데이트
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
        if (QuestManager.instance.Mission2data(cur_mission).criterion >= cur_mission.goal)
        {
            sub_text.text = $"{cur_mission.sub_text} <color=#43E0F7>{cur_mission.goal}</color>/{cur_mission.goal}";
        }
        else
        {
            sub_text.text = $"{cur_mission.sub_text} <color=#FF382B>{QuestManager.instance.Mission2data(cur_mission).criterion}</color>/{cur_mission.goal}";
        }
        reward_ark.text = cur_mission.reward_ark.ToString();
        reward_gold.text = cur_mission.reward_gold.ToString();

        Mission_userdata data = QuestManager.instance.Mission2data(cur_mission);

        accept_btn.onClick.RemoveAllListeners();

        if (data.is_clear)
        {
            if (data.get_rewarded)
            {
                //이미 보상 받음
                accept_btn.enabled = false;
                accept_img.sprite = is_completed;
            }

            else
            {
                //보상받을수있는 상태
                accept_btn.enabled = true;
                accept_img.sprite = can_reward;
                accept_btn.onClick.AddListener(Get_reward_btn);
            }
        }
        else
        {
            if (data.is_accept)
            {
                //수락함
                accept_btn.enabled = false;
                accept_img.sprite = complete_accept;
            }
            else
            {
                //수락안함
                accept_btn.enabled = true;
                accept_img.sprite = can_accept;
                accept_btn.onClick.AddListener(Accept_btn);
            }
        }
    }

    #region btn

    public void Set_index(int x)
    {
        index = x;
        Debug.Log(index);
        UI_updateL();
    }

    public void Accept_btn() //수락버튼 클릭 시 호출
    {
        if (QuestManager.instance.Mission2data(cur_mission).is_accept == true)
        {
            Debug.Log("이미 수락상태임");
            return;
        }

        QuestManager.instance.Mission2data(cur_mission).is_accept = true;
        QuestManager.instance.Mission2data(cur_mission).Data_update();
        QuestManager.instance.cur_missiontypes.Add(cur_mission.criterion_type);
        Debug.Log("수락완료!");

        UI_updateL();
    }

    public void Reset_btn() // 리셋버튼 클릭 시 호출
    {
        UI_updateL();
        UI_updateR();
    }

    public void Change_state(int index_) //일일,주간,월간 보상 클릭 시 호출
    {
        index = 0;
        state = (mission_state)index_;

        for (int i = 0; i < btn_image.Count; i++)
        {
            btn_image[i].enabled = i == index_;
        }

        Debug.Log(state);
        Reset_btn();
    }

    public void Get_reward_btn() //보상획득 버튼
    {

        Mission_userdata userdata = QuestManager.instance.Mission2data(cur_mission);

        if (userdata.is_clear || !userdata.is_accept)
        {
            return;
        }

        MoneyManager.instance.Get_Money(Money.gold, cur_mission.reward_gold);
        MoneyManager.instance.Get_Money(Money.ark, cur_mission.reward_ark);
        QuestManager.instance.Mission2data(cur_mission).get_rewarded = true;
        reward_btn.enabled = false;
        QuestManager.instance.Mission2data(cur_mission).Data_update();
    }
}
#endregion
