using BackEnd;
using UnityEngine;

public enum Money
{
    ark = 0, gold, ruby
}
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
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
            Debug.Log($"현재 ark : {value}");
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
            Debug.Log($"현재 gold : {value}");
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
            Debug.Log($"현재 ruby : {value}");
        }
    }
    int ruby_;

    private void Awake()
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
        ark = BackendGameData_JGD.userData.ark;
        gold = BackendGameData_JGD.userData.gold;
        ruby = BackendGameData_JGD.userData.ruby;
    }

    public void Get_Money(Money money, int num)
    {
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

    public void Spend_Money(Money money, int num)
    {
        //아크 돈 마이너스 시키기
        //데이터 넣기
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
    public void Data_update()
    {
        //데이터에 넣기
        BackendGameData_JGD.userData.ark= ark;
        BackendGameData_JGD.userData.gold = gold; 
        BackendGameData_JGD.userData.ruby= ruby;

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