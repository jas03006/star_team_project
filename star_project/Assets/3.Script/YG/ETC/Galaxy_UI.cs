using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// stage씬에서 각 은하 캔버스가 가지고 있음.
/// 각각 은하별 정보를 UI로 출력해주는 클래스. 
/// </summary>
public class Galaxy_UI : MonoBehaviour
{
    public Galaxy_info data; //은하 별 데이터
    public int galaxy_index; //0~4까지의 값을 가짐. ex) 0의 경우 첫번째 은하의 정보를 출력함


    public int collect_point; //은하 별 가진 레드스타 포인트. 다음 스테이지 해금 및 스테이지 별 미션에서 활용함.
    private int interval = 5; //미션 클리어 간격

    [SerializeField] private TMP_Text collect_text; //collect_point 출력

    [Header("Mission State")]//미션 진행 상태에 따라 변경될 이미지 컴포넌트.
    [SerializeField] private List<Image> mission_image = new List<Image>();
    [SerializeField] private List<Image> mission_btn = new List<Image>();
    [SerializeField] private List<Image> check = new List<Image>();
    [SerializeField] private List<Button> statebutton = new List<Button>();

    [Header("Sprite")]//컴포넌트에 들어갈 스프라이트.
    [SerializeField] private Sprite state_X;
    [SerializeField] private Sprite state_X_bar;
    [SerializeField] private Sprite state_O;
    [SerializeField] private Sprite state_O_bar;

    [Header("Star_info")] //스테이지 별 정보
    [SerializeField] private List<Star_UI> Star_UI_list = new List<Star_UI>();

    private void Awake()
    {
        //데이터 받아오기
        data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[galaxy_index];
    }
    

    public void Update_data_UI()
    {
        Check_collect();
        Update_Starinfo(data.star_Info_list);

        collect_text.text = collect_point.ToString();
    }

    public void Collect_update() //collect 설정
    {
        collect_point = 0;

        for (int i = 0; i < data.star_Info_list.Count; i++)
        {
            collect_point += data.star_Info_list[i].star;
        }

        collect_text.text = collect_point.ToString();
    }
    private void Update_Starinfo(List<Star_info> star_info) //각 스테이지 UI 업데이트
    {
        bool pre = true;

        for (int i = 0; i < star_info.Count; i++)
        {
            Star_UI_list[i].pre_clear = pre;
            Star_UI_list[i].data = star_info[i];

            if (BackendChart_JGD.chartData.StageClear_list[galaxy_index * 5 + i].RewardType == 0)
            {
                Star_UI_list[i].get_housing.sprite = SpriteManager.instance.Num2Sprite(BackendChart_JGD.chartData.StageClear_list[galaxy_index*5 + i].HousingItmeID);
            }

            else
            {
                Star_UI_list[i].get_housing.sprite = SpriteManager.instance.Num2emozi(BackendChart_JGD.chartData.StageClear_list[galaxy_index * 5 + i].Emoticon);
            }

            pre = Star_UI_list[i].data.is_clear;
        }
    }

    private void Check_collect() //현재 미션 UI 업데이트
    {
        List<Galaxy_state> mission_state = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[galaxy_index].mission_state;

        for (int i = 0; i < check.Count; i++)
        {
            check[i].enabled = false;
        }

        //btn
        int tmp = collect_point;
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

            if (mission_state[i] == Galaxy_state.can_reward)
            {
                statebutton[i].interactable = true;
            }
            tmp -= interval;
        }

        //image
        int tmp2 = collect_point;
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

            tmp2 -= interval;
        }
    }

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
