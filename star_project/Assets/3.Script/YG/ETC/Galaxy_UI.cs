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
    public int collect_point;

    [SerializeField] private TMP_Text collect_text;

    [SerializeField] private List<Image> mission_image = new List<Image>();
    [SerializeField] private List<Image> mission_btn = new List<Image>();
    [SerializeField] private List<Image> check = new List<Image>();
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
        Update_MissionState(collect_point, data.mission_state);
        Update_Starinfo(data.star_Info_list);
    }

    public void Collect_update() //collect 설정
    {
        collect_point = 0;

        for (int i = 0; i < data.star_Info_list.Count; i++)
        {
            collect_point += data.star_Info_list[i].star;
        }


        collect_text.text = collect_point.ToString();

        //if (data.collect_point != collect)
        //{
        //    data.collect_point = collect;

        //    Data_update();
        //}
    }

    private void Update_MissionState(int collect, List<Galaxy_state> state)
    {
        //스프라이트 바꾸고 버튼클릭 바꾸기
        Check_collect(collect, 5);
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

    private void Check_collect(int collect, int interval)
    {
        List<Galaxy_state> mission_state = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[galaxy_index].mission_state;

        for (int i = 0; i < check.Count; i++)
        {
            check[i].enabled = false;
        }

        //btn
        int tmp = collect;
        for (int i = 0; i < mission_btn.Count; i++)
        {
            if (tmp >= interval)
            {
                mission_btn[i].sprite = state_O;
                if (mission_state[i] == Galaxy_state.incomplete)
                {
                    mission_state[i] = Galaxy_state.can_reward;
                    BackendGameData_JGD.userData.catchingstar_info.Data_update();
                    statebutton[i].interactable = true;
                }
                else if (mission_state[i] == Galaxy_state.complete)
                {
                    check[i].enabled = true;
                }
            }
            else
            {
                mission_btn[i].sprite = state_X;
                statebutton[i].interactable = false;
            }
            tmp = -interval;
        }

        //image
        int tmp2 = collect;
        for (int i = 0; i < mission_image.Count; i++)
        {
            if (tmp2 > 0)
            {
                mission_image[i].sprite = state_O_bar;
            }

            else
            {
                mission_image[i].sprite = state_X_bar;
            }

            tmp2 = -interval;
        }
    }

    //public void Data_update()
    //{
    //    //데이터에 넣기
    //    Param param = new Param();
    //    param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

    //    BackendReturnObject bro = null;

    //    if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
    //    {
    //        Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

    //        bro = Backend.GameData.Update("USER_DATA", new Where(), param);
    //    }

    //    else
    //    {
    //        Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

    //        bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
    //    }

    //    if (bro.IsSuccess())
    //    {
    //        Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
    //    }
    //    else
    //    {
    //        Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
    //    }
    //}
    public void Send_Galaxylevel() //스테이지 선택 버튼
    {
        LevelSelectMenuManager_JGD.GalaxyLevel = galaxy_index;
    }
    public void Get_reward(int money) //미션 보상버튼
    {
        MoneyManager.instance.Get_Money((Money)money, 100);
    }
    public void Statechange_btn(int index)
    {
        statebutton[index].interactable = false;
        BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[galaxy_index].mission_state[index] = Galaxy_state.complete;
        Update_data_UI();
    }
}
