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

    [SerializeField] List<Canvas> Canvas_list = new List<Canvas>();//오브젝트
    [SerializeField] List<Galaxy_UI> Galaxy_UI_list = new List<Galaxy_UI>();//스크립트


    //장규동
    public Level_progress_JGD[] levelProgresses;
    public static int currLevel;
    public static int UnlockedLevels;
    public static int Tema;

    public Sprite Clearscore;

    [SerializeField] private SceneNames nextScene;  //만약 기존 구조가 아닌 게임씬을 여러개 만든다면 수정

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
       UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);    ////////////////////스테이지 클리어 정보 추후 DB로 변화
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

    #region 성유경

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

    public void OnClickLevel(int levelNum) //스테이지 선택 버튼
    {
        currLevel = levelNum;
        SceneManager.LoadScene(nextScene.ToString());//만약 게임씬을 여러개 만든다면 수정//////////////
    }

    public void OnClickBack()
    {
        this.gameObject.SetActive(false);
    }

    public void Setting_Galaxy()
    {
        
    }
}
