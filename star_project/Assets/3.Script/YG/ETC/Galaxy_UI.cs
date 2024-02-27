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

    [Header("Star_info")] //�ʹ� ����� + �ߺ��̶� ���� Ŭ������ ��
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

    public void Collect_update() //collect ����
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
        //collect check - Sprite �� �ٲٱ�
        for (int i = 0; i < mission_image.Count; i++)
        {
            Check_collect(collect, i, 3);
        }

        //state check - ��ư ���� �ٲٱ�
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
        // collect�� index���� �۰ų� ���� ���
        if (collect >= index)
        {
            // collect�� 0�̰� index�� 0�� ���
            if (collect == 0 && index == 0)
            {
                mission_image[index].sprite = state_X_bar;
            }
            else
            {
                // index�� ¦���� ���
                if (index % 2 == 0)
                {
                    mission_image[index].sprite = state_O_bar;
                }
                else // index�� Ȧ���� ���
                {
                    mission_image[index].sprite = state_O;
                }
            }
        }
        else // collect�� index���� ū ���
        {
            // index�� ¦���� ���
            if (index % 2 == 0)
            {
                mission_image[index].sprite = state_X_bar;
            }
            else // index�� Ȧ���� ���
            {
                mission_image[index].sprite = state_X;
            }
        }
    }

    //private void Check_collect(int collect, int index, int interval)//interval = ����
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
        //�����Ϳ� �ֱ�
        Param param = new Param();
        param.Add("catchingstar_info", BackendGameData_JGD.userData.catchingstar_info);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }
}
