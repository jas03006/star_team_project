using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 챕터 해금 시 관련 UI출력하는 클래스.
/// </summary>
public class Unlock_UI : MonoBehaviour
{
    [SerializeField] TMP_Text guide;
    [SerializeField] TMP_Text num;
    [SerializeField] Button unlock_btn;
    [SerializeField] Button pannel_btn;

    public void Can_unlock(int cur, int goal) //해금 가능
    {
        guide.text = "챕터를 해금할 수 있습니다.\n아래 버튼을 눌러 해금해 주세요!";
        num.text = $"<color=#43E0F7>{cur}</color>/{goal}";
        unlock_btn.interactable = true;
        pannel_btn.interactable = false;
    }

    public void Cannot_unlock(int cur, int goal) //해금 불가능
    {
        guide.text = "챕터를 해금하려면\n레드 스타가 필요합니다.";
        num.text = $"<color=#FF9900>{cur}</color>/{goal}";
        unlock_btn.interactable = false;
        pannel_btn.interactable = true;
    }

}
