using BackEnd;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Galaxy_UI : MonoBehaviour
{
    public Galaxy_info data;
    public int galaxy_index;

    [Header("Mission State")]
    [SerializeField] private Sprite state_X;
    [SerializeField] private Sprite state_X_bar;
    [SerializeField] private Sprite state_O;
    [SerializeField] private Sprite state_O_bar;

    [SerializeField] private TMP_Text collect_text;

    [SerializeField] private List<Image> mission_image = new List<Image>();
    [SerializeField] private List<Button> statebutton = new List<Button>();

    [Header("Star_info")] //너무 고봉밥 + 중복이라 따로 클래스로 뺌
    [SerializeField] private List<Star_UI> Star_UI_list = new List<Star_UI>();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[galaxy_index];
        Collect_update();
    }

    public void Update_data_UI()
    {
        //data
        Collect_update();

        //UI
        Update_MissionState(data.collect_point, data.mission_state);
        Update_Starinfo(data.star_Info_list);
    }

    public void Collect_update() //collect 설정
    {
        int collect = 0;

        for (int i = 0; i < data.star_Info_list.Count; i++)
        {
            collect += data.star_Info_list[i].star;
        }

        collect_text.text = collect.ToString();

        if (data.collect_point != collect)
        {
            data.collect_point = collect;

            Data_update();
        }
    }

    private void Update_MissionState(int collect, List<Galaxy_state> state)
    {
        //collect check - Sprite 색 바꾸기
        for (int i = 0; i < mission_image.Count; i++)
        {
            Check_collect(collect, i, 3);
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
        bool pre = true;

        for (int i = 0; i < star_info.Count; i++)
        {
            Star_UI_list[i].pre_clear = pre;
            Star_UI_list[i].data = star_info[i];
            pre = Star_UI_list[i].data.is_clear;
        }
    }

    private void Check_collect(int collect, int index, int interval)
    {
        // collect가 index보다 작거나 같은 경우
        if (collect >= index)
        {
            // collect가 0이고 index가 0인 경우
            if (collect == 0 && index == 0)
            {
                mission_image[index].sprite = state_X_bar;
            }
            else
            {
                // index가 짝수인 경우
                if (index % 2 == 0)
                {
                    mission_image[index].sprite = state_O_bar;
                }
                else // index가 홀수인 경우
                {
                    mission_image[index].sprite = state_O;
                }
            }
        }
        else // collect가 index보다 큰 경우
        {
            // index가 짝수인 경우
            if (index % 2 == 0)
            {
                mission_image[index].sprite = state_X_bar;
            }
            else // index가 홀수인 경우
            {
                mission_image[index].sprite = state_X;
            }
        }
    }

    //private void Check_collect(int collect, int index, int interval)//interval = 간격
    //{
    //    if (collect >= index)
    //    {
    //        if (collect == 0 && index == 0 )
    //        {
    //            mission_image[index].sprite = state_X_bar;
    //        }
    //        else
    //        {
    //            mission_image[index].sprite = state_O;
    //        }
    //    }
    //    else if (index % 2 == 0)
    //    {
    //        mission_image[index].sprite = state_X_bar;
    //    }
    //    else
    //    {
    //        mission_image[index].sprite = state_X;
    //    }
    //}

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
