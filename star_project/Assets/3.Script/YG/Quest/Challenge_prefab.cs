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
    public int index;
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
                state_text.text = "미완료";
                break;
            case challenge_state.can_reward:
                state_text.text = "보상수령";
                break;
            case challenge_state.complete:
                state_text.text = "완료";
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

    public void Get_reward_btn() //보상받기 버튼에 넣어둘 메서드
    {
        challenge.Get_reward();
        BackendGameData_JGD.userData.Achievements_List.Add(index);
        Update_UI_state();
    }

    public void criterionUp_btn() //기준치상승 버튼 클릭시 호출 - 예시
    {
        challenge.userdata.criterion++;//기준치 증가
        challenge.userdata.Data_update();

        if (challenge.Check_clear()) //클리어했다면
        {
            Update_UI_state();//state UI 업데이트
        }
    }
}
