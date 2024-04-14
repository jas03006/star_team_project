using BackEnd;
using TMPro;
using UnityEngine;
/// <summary>
/// Game 씬에서 환경설정을 진행하고 UI를 출력하는 클래스.
/// 싱글턴이며 
/// </summary>
public enum Money//게임 내 재화
{
    ark = 0, gold, ruby
}
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; //싱글턴

    [Header("Text")] //각 재화를 출력해주는 텍스트 컴포넌트
    [SerializeField] private TMP_Text ark_text;
    [SerializeField] private TMP_Text gold_text;
    [SerializeField] private TMP_Text ruby_text;

    //각 재화별로 최소 0으로 제한하고 변경 시 UI 업데이트
    public int ark
    {
        get
        {
            return ark_;
        }
        set
        {
            ark_ = value;
            if (ark_ < 0)
            {
                ark_ = 0;
            }
            if (ark_text != null)
            {
                ark_text.text = ark_.ToString();
            }
        }
    }
    int ark_;

    public int gold
    {
        get
        {
            return gold_;
        }
        set
        {

            gold_ = value;
            if (gold_ < 0)
            {
                gold_ = 0;
            }
            if (gold_text != null)
            {
                gold_text.text = gold_.ToString();
            }
        }
    }
    int gold_;

    public int ruby
    {
        get
        {
            return ruby_;
        }
        set
        {

            ruby_ = value;
            if (ruby_ < 0)
            {
                ruby_ = 0;
            }
            if (ruby_text != null)
            {
                ruby_text.text = ruby_.ToString();
            }
        }
    }
    int ruby_;

    private void Awake()//싱글턴
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //데이터 받아오기
        ark = BackendGameData_JGD.userData.ark;
        gold = BackendGameData_JGD.userData.gold;
        ruby = BackendGameData_JGD.userData.ruby;
    }

    public void Get_Money(Money money, int num)//재화 추가시 실행됨
    {
        //매개변수 : money = 재화 종류, num = 액수
        switch (money)
        {
            case Money.ark:
                ark += num;
                break;
            case Money.gold:
                gold += num;
                break;
            case Money.ruby:
                ruby += num;
                break;
            default:
                break;
        }
        Data_update();
    }
    public void Get_Money(int gold_ = 0, int ark_ = 0, int ruby_ = 0)//재화 추가시 실행됨2
    {
        if (gold_ == 0 && ark_ == 0 && ruby_ == 0)
        {
            return;
        }
        ark += ark_;
        gold += gold_;
        ruby += ruby_;

        Data_update();
    }
    public void Spend_Money(Money money, int num)//재화 사용시 실행됨
    {
        switch (money)
        {
            case Money.ark:
                ark -= num;
                break;
            case Money.gold:
                gold -= num;
                break;
            case Money.ruby:
                ruby -= num;
                break;
            default:
                break;
        }
        Data_update();
    }
    public void Spend_Money(int gold_ = 0, int ark_ = 0, int ruby_ = 0)//재화 사용시 실행됨2
    {
        if (gold_ == 0 && ark_ == 0 && ruby_ == 0)
        {
            return;
        }
        ark -= ark_;
        gold -= gold_;
        ruby -= ruby_;

        Data_update();
    }

    public int Check_Money(Money money)//가진 재화 확인 시 사용
    {
        switch (money)
        {
            case Money.ark:
                return ark;
            case Money.gold:
                return gold;
            case Money.ruby:
                return ruby;
            default:
                return 0;
        }
    }
    public void Data_update() //변경된 재화 DB에 전송
    {
        //데이터에 넣기
        BackendGameData_JGD.userData.ark = ark;
        BackendGameData_JGD.userData.gold = gold;
        BackendGameData_JGD.userData.ruby = ruby;

        Param param = new Param();
        param.Add("ark", BackendGameData_JGD.userData.ark);
        param.Add("gold", BackendGameData_JGD.userData.gold);
        param.Add("ruby", BackendGameData_JGD.userData.ruby);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(BackendGameData_JGD.Instance.gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}