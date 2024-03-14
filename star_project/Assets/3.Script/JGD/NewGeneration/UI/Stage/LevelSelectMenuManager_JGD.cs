using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum galaxy
{
    tutorial = -1,
    toy,
    kitchen,
    play_ground,
    school,
    emotion
}
public class LevelSelectMenuManager_JGD : MonoBehaviour
{
    //성유경
    public galaxy galaxy
    {
        get { return galaxy_; }
        set
        {
            galaxy_ = value;
            Update_canvas();
        }
    }

    private galaxy galaxy_;
    private galaxy pre_galaxy;

    [SerializeField] List<Canvas> Canvas_list = new List<Canvas>();//오브젝트
    [SerializeField] List<GameObject> stage_case = new List<GameObject>();
    [SerializeField] Canvas tutorial_canvas;//오브젝트
    [SerializeField] List<Galaxy_UI> Galaxy_UI_list = new List<Galaxy_UI>();//스크립트
    public static int currLevel; //현재 진행중인 스테이지 레벨
    public static int GalaxyLevel = 0; //현재 진행중인 은하 레벨
    public Sprite Clearscore;

    [Header("unlock")]
    private int[] unlock_conditions = { 12, 24, 36, 48 };
    [SerializeField] private GameObject unlock_object;
    [SerializeField] private Image character_image;
    [SerializeField] private Unlock_UI unlock_ui;

    [SerializeField] private SceneNames nextScene;  //만약 기존 구조가 아닌 게임씬을 여러개 만든다면 수정

    [SerializeField] private List<Image> case_sprite = new List<Image>();
    [SerializeField] private List<TMP_Text> case_text = new List<TMP_Text>();
    [SerializeField] private Sprite curindexO_sprite;
    [SerializeField] private Sprite curindexX_sprite;
    [SerializeField] private Color color_O;
    [SerializeField] private Color color_X;

    private void OnEnable()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.clear)
        {
            tutorial_canvas.enabled = true;

            for (int i = 0; i < Canvas_list.Count; i++)
            {
                Canvas_list[i].enabled = false;
            }

            stage_case[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "별둥지 은하";
            for (int i = 1; i < stage_case.Count; i++)
            {
                stage_case[i].SetActive(false);
            }

            return;
        }

        stage_case[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "장난감 은하";
        for (int i = 1; i < stage_case.Count; i++)
        {
            stage_case[i].SetActive(true);
        }

        galaxy = (galaxy)GalaxyLevel;
        pre_galaxy = galaxy.toy;
    }
    #region 성유경
    public void Select_galaxy_btn(int index)
    {
        pre_galaxy = galaxy;
        galaxy = (galaxy)index;

        for (int i = 0; i < case_sprite.Count; i++)
        {
            if (i == index)
            {
                case_sprite[i].sprite = curindexO_sprite;
                case_text[i].color = color_O;
            }

            else
            {
                case_sprite[i].sprite = curindexX_sprite;
                case_text[i].color = color_X;
            }
        }
    }

    public void Go_pregalaxy()
    {
        galaxy = pre_galaxy;
        unlock_object.SetActive(false);
    }

    public void Update_canvas()
    {
        Debug.Log((int)galaxy);
        for (int i = 0; i < Canvas_list.Count; i++)
        {
            if (i == (int)galaxy)
            {
                Galaxy_UI_list[i].data = BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[i];
                Galaxy_UI_list[i].Update_data_UI();
                Canvas_list[i].enabled = true;
            }
            else
            {
                Canvas_list[i].enabled = false;
            }
        }

        int all_collect = 0;
        for (int i = 0; i < Galaxy_UI_list.Count; i++)
        {
            all_collect += Galaxy_UI_list[i].collect_point;
        }

        //해금여부 체크
        if (galaxy == galaxy.toy)
            return;

        if (!BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy-1].is_clear)
        {
            //해금 가능
            if (all_collect >= unlock_conditions[(int)galaxy-1])
            {
                unlock_ui.Can_unlock(all_collect, unlock_conditions[(int)galaxy-1]);
                QuestManager.instance.Check_mission(Criterion_type.galaxy_clear);

                CharacterInfo_YG info = BackendGameData_JGD.userData.character_info;
                character_image.sprite = SpriteManager.instance.Num2Sprite(BackendChart_JGD.chartData.character_list[(int)galaxy].sprite);
                
                Debug.Log(info.character_list[(int)galaxy].pet_id) ;
                info.character_list[(int)galaxy].level = 1;
                info.Change_dic((int)galaxy, 1);

                BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy-1].is_clear = true;
                BackendGameData_JGD.Instance.GameDataUpdate();
            }

            //해금 불가능
            else
            {
                unlock_ui.Cannot_unlock(all_collect, unlock_conditions[(int)galaxy - 1]);
            }
            unlock_object.SetActive(true);
            return;
        }

        Debug.Log("이미 해금돼서 넘김");
        unlock_object.SetActive(false);
    }

    #endregion

    public void OnClickLevel(int levelNum) //스테이지 선택 버튼
    {
        currLevel = levelNum;
        SceneManager.LoadScene(nextScene.ToString());//만약 게임씬을 여러개 만든다면 수정//////////////
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }

    public void Go_home()
    {
        TCP_Client_Manager.instance.go_myplanet();
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
