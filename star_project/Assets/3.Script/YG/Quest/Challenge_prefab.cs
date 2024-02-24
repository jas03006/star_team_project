using TMPro;
using UnityEngine;

public class Challenge_prefab : MonoBehaviour
{
    public Challenge challenge
    {
        get { return challenge_; }
        set
        {
            challenge_ = value;
            Update_UI();
        }
    }
    private Challenge challenge_;
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text contents_text;
    [SerializeField] TMP_Text count_text;
    [SerializeField] TMP_Text CP_text;
    [SerializeField] TMP_Text state_text;

    [SerializeField] GameObject detail;

    public void Update_UI()
    {
        name_text.text = challenge.title;
        contents_text.text = challenge.contents;
        count_text.text = challenge.contents2 + $"{challenge.userdata.criterion} / {challenge.goal}";
        CP_text.text = $"{challenge.CP}\nCP";
        Update_UI_state();
    }

    private void Update_UI_state()
    {
        switch (challenge.userdata.state)
        {
            case challenge_state.incomplete:
                state_text.text = "�̿Ϸ�";
                break;
            case challenge_state.can_reward:
                state_text.text = "�������";
                break;
            case challenge_state.complete:
                state_text.text = "�Ϸ�";
                break;
        }
    }

    private void OnEnable()
    {
        Active_Change();
    }

    public void Active_Change()
    {
        detail.SetActive(!detail.activeSelf);
        Canvas.ForceUpdateCanvases();
    }

    public void Get_reward_btn() //����ޱ� ��ư�� �־�� �޼���
    {
        challenge.Get_reward();
        Update_UI_state();
    }

    public void criterionUp_btn() //����ġ��� ��ư Ŭ���� ȣ�� - ����
    {
        challenge.userdata.criterion++;//����ġ ����
        challenge.userdata.Data_update();

        if (challenge.Check_clear()) //Ŭ�����ߴٸ�
        {
            Update_UI_state();//state UI ������Ʈ
        }
    }
}
