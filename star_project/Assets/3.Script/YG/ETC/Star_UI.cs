using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Star_UI : MonoBehaviour
{

    //data
    public Star_info data
    {
        get
        { return data_; }

        set
        {
            data_ = value;

            //UI업데이트
            Update_star();
            Update_is_clear();
            Update_get_housing();
        }
    }
    public Star_info data_;

    //is_clear
    public bool pre_clear;
    [SerializeField] private GameObject clear_O;
    [SerializeField] private GameObject clear_X;

    //is_star
    [SerializeField] private List<Image> star_list = new List<Image>();
    [SerializeField] private Sprite star_O;
    [SerializeField] private Sprite star_X;

    //get_housing
    [SerializeField] Image get_housing;

    public void Update_star()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < data.star)
            {
                star_list[i].sprite = star_O;
            }
            else
            {
                star_list[i].sprite = star_X;
            }
        }
    }

    public void Update_is_clear()
    {
        clear_O.SetActive(pre_clear);
        clear_X.SetActive(!pre_clear);
    }

    public void Update_get_housing()
    {
        get_housing.enabled = data.get_housing;
    }

    public void OnClickLevel(int levelNum) //스테이지 선택 버튼
    {
        LevelSelectMenuManager_JGD.currLevel = levelNum;
        SceneManager.LoadScene("Game");//만약 게임씬을 여러개 만든다면 수정//////////////
    }
}