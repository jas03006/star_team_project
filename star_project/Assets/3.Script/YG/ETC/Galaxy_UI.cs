using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

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

    public void Collect_update(Galaxy_info info)
    {
        int collect = 0;

        for (int i = 0; i < info.star_Info_list.Count; i++)
        {
            collect += info.star_Info_list[i].star;
        }

        if (info.collect_point != collect)
        {
            info.collect_point = collect;
            Data_update();
        }
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

    public void Data_update()
    {
        //데이터에 넣기
        Param param = new Param();
        param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}
