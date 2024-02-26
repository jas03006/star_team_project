using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum galaxy 
{
    none = -1,
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
    [SerializeField] List<Galaxy_UI> Galaxy_UI_list = new List<Galaxy_UI>();//��ũ��Ʈ


    //��Ե�
    public Level_progress_JGD[] levelProgresses;
    public static int currLevel;
    public static int UnlockedLevels;
    public static int Tema;

    public Sprite Clearscore;

    [SerializeField] private SceneNames nextScene;  //���� ���� ������ �ƴ� ���Ӿ��� ������ ����ٸ� ����

    private void Awake()
    {
        //currLevel = 0;
        //UnlockedLevels = 0;
        //Debug.Log(currLevel);
        //Debug.Log(UnlockedLevels);
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
       UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);    ////////////////////�������� Ŭ���� ���� ���� DB�� ��ȭ
        for (int i = 0; i < levelProgresses.Length; i++)
        {
            if (UnlockedLevels >= i)
            {
                levelProgresses[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(),0);
                for (int j = 0; j < stars; j++)
                {
                    levelProgresses[i].stars[j].sprite = Clearscore;
                }
            }
        }
    }

    #region ������

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

    public void Setting_Galaxy()
    {
        
    }
}
