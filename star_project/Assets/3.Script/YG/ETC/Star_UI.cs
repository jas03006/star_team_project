using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// stage씬에서 각 은하 캔버스 속 레벨 버튼 오브젝트가 가지고 있음.
/// 각각 스테이지별 정보를 UI로 출력해주는 클래스. 
/// </summary>
public class Star_UI : MonoBehaviour
{
    public Star_info data //스테이지 별 데이터
    {
        get
        { return data_; }

        set
        {
            data_ = value;

            //데이터가 바뀔 때마다 UI업데이트 진행.
            Update_star();
        }
    }
    public Star_info data_;

    public bool pre_clear; //직전 스테이지 클리어 여부
    [SerializeField] private GameObject clear_O;//클릭 시 stage 진입. pre_clear true일 시 활성화
    [SerializeField] private GameObject clear_X;//pre_clear false일 시 활성화
    [SerializeField] private List<Image> star_list = new List<Image>();//스테이지 진행 상태에 따라 변경될 이미지 컴포넌트.

    [Header("Sprite")]//컴포넌트에 들어갈 스프라이트.
    [SerializeField] private Sprite star_O;
    [SerializeField] private Sprite star_X;

    public Image get_housing; //스테이지 별 지급되는 하우징 오브젝트 이미지

    public void Update_star()//스테이지 진행 상태에 따라 스프라이트 변경
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
        clear_O.SetActive(pre_clear);
        clear_X.SetActive(!pre_clear);

        //하우징 오브젝트 클리어 여부 확인 후 클리어 시 하우징 이미지 띄우기
        get_housing.enabled = data.get_housing;
    }

    public void OnClickLevel(int levelNum) 
    {
        //스테이지 선택 버튼을 누를 시 실행되는 메서드
        //LevelSelectMenuManager_JGD가 클릭한 스테이지를 저장하도록 함.
       
        LevelSelectMenuManager_JGD.currLevel = levelNum;
        SceneManager.LoadScene("Game");
    }
}