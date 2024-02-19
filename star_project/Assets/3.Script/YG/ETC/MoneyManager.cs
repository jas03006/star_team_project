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
            Debug.Log($"���� ark : {value}");
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
            Debug.Log($"���� gold : {value}");
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
            Debug.Log($"���� ruby : {value}");
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
        Send_data(money);
    }

    public void Spend_Money(Money money, int num)
    {
        //��ũ �� ���̳ʽ� ��Ű��
        //������ �ֱ�
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
        Send_data(money);
    }

    private void Send_data(Money money) //���� �����Ϳ� �ֱ�
    {
        if (BackendGameData_JGD.userData == null)
        {
            Debug.LogError("�������� �ٿ�ްų�  ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param();

        switch (money)
        {
            case Money.ark:
                param.Add("ark", BackendGameData_JGD.userData.ark);
                break;
            case Money.gold:
                param.Add("gold", BackendGameData_JGD.userData.gold);
                break;
            case Money.ruby:
                param.Add("ruby", BackendGameData_JGD.userData.ruby);
                break;
            default:
                break;
        }

        BackendReturnObject bro = null;
        var gameDataRowInDate = BackendGameData_JGD.Instance.gameDataRowInDate;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
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