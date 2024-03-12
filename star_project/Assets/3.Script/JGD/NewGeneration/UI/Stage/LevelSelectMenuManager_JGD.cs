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

    [SerializeField] private SceneNames nextScene;  //���� ���� ������ �ƴ� ���Ӿ��� ������ ����ٸ� ����

    private void OnEnable()
    {
        if (BackendGameData_JGD.userData.tutorial_Info.state != Tutorial_state.clear)
        {
            Debug.Log("��");
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
        Debug.Log("����");

        stage_case[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "�峭�� ����";
        for (int i = 1; i < stage_case.Count; i++)
        {
            stage_case[i].SetActive(true);
        }

        galaxy = galaxy.toy;
        StartCoroutine(Galaxy_unlock());
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #region ������

    //private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    if (SceneManager.GetActiveScene().name == "Stage")
    //    {
            
    //    }
    //}

    public void Select_galaxy_btn(int index)
    {
        galaxy = (galaxy)index;
    }

    public void Update_canvas()
    {
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
    }

    public IEnumerator Galaxy_unlock()
    {
        yield return null;
        yield return null;
        //!BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy].is_clear &&
        if ( Galaxy_UI_list[(int)galaxy].collect_point >= unlock_conditions[(int)galaxy])
        {
            unlock_object.SetActive(true);
            CharacterInfo_YG info = BackendGameData_JGD.userData.character_info;
            character_image.sprite = SpriteManager.instance.Num2Sprite(BackendChart_JGD.chartData.character_list[(int)galaxy + 1].sprite);
            info.character_list[(int)galaxy + 1].level = 1;
            info.Change_dic((int)galaxy+1,1);
            info.Characterinfo_update();
            BackendGameData_JGD.userData.catchingstar_info.galaxy_Info_list[(int)galaxy].is_clear = true;
            
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
