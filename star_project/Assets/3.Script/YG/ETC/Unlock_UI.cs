using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// é�� �ر� �� ���� UI����ϴ� Ŭ����.
/// </summary>
public class Unlock_UI : MonoBehaviour
{
    [SerializeField] TMP_Text guide;
    [SerializeField] TMP_Text num;
    [SerializeField] Button unlock_btn;
    [SerializeField] Button pannel_btn;

    public void Can_unlock(int cur, int goal) //�ر� ����
    {
        guide.text = "é�͸� �ر��� �� �ֽ��ϴ�.\n�Ʒ� ��ư�� ���� �ر��� �ּ���!";
        num.text = $"<color=#43E0F7>{cur}</color>/{goal}";
        unlock_btn.interactable = true;
        pannel_btn.interactable = false;
    }

    public void Cannot_unlock(int cur, int goal) //�ر� �Ұ���
    {
        guide.text = "é�͸� �ر��Ϸ���\n���� ��Ÿ�� �ʿ��մϴ�.";
        num.text = $"<color=#FF9900>{cur}</color>/{goal}";
        unlock_btn.interactable = false;
        pannel_btn.interactable = true;
    }

}
