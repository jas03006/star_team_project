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
    //������
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

    [SerializeField] List<Canvas> Canvas_list = new List<Canvas>();//������Ʈ
    [SerializeField] List<GameObject> stage_case = new List<GameObject>();
    [SerializeField] Canvas tutorial_canvas;//������Ʈ
    [SerializeField] List<Galaxy_UI> Galaxy_UI_list = new List<Galaxy_UI>();//��ũ��Ʈ
    public static int currLevel; //���� �������� �������� ����
    public static int GalaxyLevel; //���� �������� ���� ����
    public Sprite Clearscore;

    [Header("unlock")]
    private int[] unlock_conditions = { 12, 24, 36, 48 };
    [SerializeField] private GameObject unlock_object;
    [SerializeField] private Image character_image;
    [SerializeField] private Unlock_UI unlock_ui;

    [SerializeField] private SceneNames nextScene;  //���� ���� ������ �ƴ� ���Ӿ��� ������ ����ٸ� ����

    private void OnEnable()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.clear)
        {
            tutorial_canvas.enabled = true;

            for (int i = 0; i < Canvas_list.Count; i++)
            {
                Canvas_list[i].enabled = false;
            }

            stage_case[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "������ ����";
            for (int i = 1; i < stage_case.Count; i++)
            {
                stage_case[i].SetActive(false);
            }

            return;
        }

        stage_case[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "�峭�� ����";
        for (int i = 1; i < stage_case.Count; i++)
        {
            stage_case[i].SetActive(true);
        }

        galaxy = galaxy.toy;
        pre_galaxy = galaxy.toy;
        //StartCoroutine(Galaxy_unlock());
    }
    #region ������
    public void Select_galaxy_btn(int index)
    {
        pre_galaxy = galaxy;
        galaxy = (galaxy)index;
    }

    public void Go_pregalaxy()
    {
        galaxy = pre_galaxy;
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

        //�رݿ��� üũ
        if (galaxy == galaxy.toy)
            return;

        if (!BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy-1].is_clear)
        {
            //�ر� ����
            if (all_collect >= unlock_conditions[(int)galaxy-1])
            {
                unlock_ui.Can_unlock(all_collect, unlock_conditions[(int)galaxy-1]);
                QuestManager.instance.Check_mission(Criterion_type.galaxy_clear);

                CharacterInfo_YG info = BackendGameData_JGD.userData.character_info;
                character_image.sprite = SpriteManager.instance.Num2Sprite(BackendChart_JGD.chartData.character_list[(int)galaxy].sprite);

                info.character_list[(int)galaxy].level = 1;
                info.Change_dic((int)galaxy, 1);

                BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy-1].is_clear = true;
                BackendGameData_JGD.Instance.GameDataUpdate();
            }

            //�ر� �Ұ���
            else
            {
                unlock_ui.Cannot_unlock(all_collect, unlock_conditions[(int)galaxy - 1]);
            }
            unlock_object.SetActive(true);
            return;
        }

        Debug.Log("�̹� �رݵż� �ѱ�");
        unlock_object.SetActive(false);
    }



    public IEnumerator Galaxy_unlock()
    {
        yield return null;
        yield return null;

        Debug.Log($"{!BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy].is_clear} / {Galaxy_UI_list[(int)galaxy].collect_point >= unlock_conditions[(int)galaxy]}");
        if (!BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy].is_clear && Galaxy_UI_list[(int)galaxy].collect_point >= unlock_conditions[(int)galaxy])
        {
            Debug.Log("�ر�!");
            QuestManager.instance.Check_mission(Criterion_type.galaxy_clear);
            unlock_object.SetActive(true);

            CharacterInfo_YG info = BackendGameData_JGD.userData.character_info;
            character_image.sprite = SpriteManager.instance.Num2Sprite(BackendChart_JGD.chartData.character_list[(int)galaxy + 1].sprite);
            
            info.character_list[(int)galaxy + 1].level = 1;
            info.Change_dic((int)galaxy + 1, 1);
            BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy].is_clear = true;
            BackendGameData_JGD.Instance.GameDataUpdate();
        }
        else
        {
            unlock_object.SetActive(false);
            Debug.Log("�������� �ر�X");
        }
    }


    #endregion

    public void OnClickLevel(int levelNum) //�������� ���� ��ư
    {
        currLevel = levelNum;
        SceneManager.LoadScene(nextScene.ToString());//���� ���Ӿ��� ������ ����ٸ� ����//////////////
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }

    public void Go_home()
    {
        TCP_Client_Manager.instance.go_myplanet();
    }
}
