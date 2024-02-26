using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Purchasing;

public class Galaxy_UI : MonoBehaviour
{

    [Header("Mission State")]
    [SerializeField] private Sprite state_X;
    [SerializeField] private Sprite state_X_bar;
    [SerializeField] private Sprite state_O;

    [SerializeField] private List<Image> mission_image = new List<Image>();
    [SerializeField] private List<Button> statebutton = new List<Button>(); 

    [Header("Star_info")] //너무 고봉밥 + 중복이라 따로 클래스로 뺌
    [SerializeField] private List<Star_UI> Star_UI_list = new List<Star_UI>();

    public void Update_UI(Galaxy_info info)
    {
        Update_MissionState(info.collect_point,info.mission_state);
        Update_Starinfo(info.star_Info_list);
    }

    private void Update_MissionState(int collect, List<Galaxy_state> state)
    {
        //collect check - Sprite 색 바꾸기
        for (int i = 0; i < mission_image.Count; i++)
        {
            Check_collect(collect, i, collect * (i+1));
        }

        //state check - 버튼 상태 바꾸기
        for (int i = 0; i < state.Count; i++)
        {
            switch (state[i])
            {
                case Galaxy_state.can_reward:
                    statebutton[i].interactable = true;
                    break;
                default:
                    statebutton[i].interactable = false;
                    break;
            }
        }
    }

    private void Update_Starinfo(List<Star_info> star_info)
    {
        for (int i = 0; i < star_info.Count; i++)
        {
            Star_UI_list[i].data = star_info[i];
        }
    }

    private void Check_collect(int collect, int index, int interval)
    {
        if (collect <= interval)
        {
            mission_image[index].sprite = state_O;
        }
        else
        {
            mission_image[index].sprite = state_X;
        }
    }
}
