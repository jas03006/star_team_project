using BackEnd;
using TMPro;
using UnityEngine;
/// <summary>
/// Game ������ ȯ�漳���� �����ϰ� UI�� ����ϴ� Ŭ����.
/// �̱����̸� 
/// </summary>
public enum Money//���� �� ��ȭ
{
    ark = 0, gold, ruby
}
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; //�̱���

    [Header("Text")] //�� ��ȭ�� ������ִ� �ؽ�Ʈ ������Ʈ
    [SerializeField] private TMP_Text ark_text;
    [SerializeField] private TMP_Text gold_text;
    [SerializeField] private TMP_Text ruby_text;

    //�� ��ȭ���� �ּ� 0���� �����ϰ� ���� �� UI ������Ʈ
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

    private void Awake()//�̱���
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
        //������ �޾ƿ���
        ark = BackendGameData_JGD.userData.ark;
        gold = BackendGameData_JGD.userData.gold;
        ruby = BackendGameData_JGD.userData.ruby;
    }

    public void Get_Money(Money money, int num)//��ȭ �߰��� �����
    {
        //�Ű����� : money = ��ȭ ����, num = �׼�
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
    public void Get_Money(int gold_ = 0, int ark_ = 0, int ruby_ = 0)//��ȭ �߰��� �����2
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
    public void Spend_Money(Money money, int num)//��ȭ ���� �����
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
    public void Spend_Money(int gold_ = 0, int ark_ = 0, int ruby_ = 0)//��ȭ ���� �����2
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

    public int Check_Money(Money money)//���� ��ȭ Ȯ�� �� ���
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
    public void Data_update() //����� ��ȭ DB�� ����
    {
        //�����Ϳ� �ֱ�
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
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }

        else
        {
            Debug.Log($"{BackendGameData_JGD.Instance.gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", BackendGameData_JGD.Instance.gameDataRowInDate, Backend.UserInDate, param);
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