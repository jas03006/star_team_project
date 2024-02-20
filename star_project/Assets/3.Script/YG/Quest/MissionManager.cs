using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    List<Mission> missions_daily = new List<Mission>();
    List<Mission> missions_week = new List<Mission>();
    List<Mission> missions_month = new List<Mission>();

    [Header("Left_UI")]
    [SerializeField] private TMP_Text s_title;
    [SerializeField] private TMP_Text contents;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private Button accept_btn;

    [Header("Right_UI")]
    [SerializeField] private List<TMP_Text> mission_names;

    int index; //0 = daily, 1 = week, 2 = month

    private void UI_updateR(List<Mission> missions)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            mission_names[i].text = missions[i].data.title;
        }
    }

    private void U_updateL(Mission misson)
    {
        s_title.text = misson.data.title;
        contents.text = misson.data.contents;
        reward.text = $"보상 : <color = yellow>★</color> x {misson.data.reward_gold} <color = red>★</color> x {misson.data.reward_ark}";
        accept_btn.enabled = !misson.data.is_accept;
    }

    public void Accept_btn() //수락버튼 클릭 시 호출
    {
        
    }
}
