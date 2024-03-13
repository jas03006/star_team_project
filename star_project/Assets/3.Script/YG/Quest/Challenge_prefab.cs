using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challenge_prefab : MonoBehaviour
{
    public Challenge challenge;

    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text contents_text;
    [SerializeField] TMP_Text sub_text;
    [SerializeField] TMP_Text count_text;
    [SerializeField] TMP_Text CP_text;
    [SerializeField] Image state_image;
    [SerializeField] Button reward_btn;

    [SerializeField] Sprite incomplete;
    [SerializeField] Sprite can_reward;
    [SerializeField] Sprite complete;

    public void Update_UI()
    {
        name_text.text = challenge.title;
        contents_text.text = challenge.contents;
        sub_text.text = challenge.sub_text;

        if (challenge.userdata.criterion >= challenge.goal)
        {
            if(challenge.userdata.state == challenge_state.incomplete)
            {
                challenge.userdata.state = challenge_state.can_reward;
            }
            count_text.text = "(<color=#43E0F7>" + challenge.goal + "</color>/" + challenge.goal + ")";
        }

        else
        {
            count_text.text = "(<color=#FF382B>" + challenge.userdata.criterion+ "</color>/" + challenge.goal + ")";
        }
        CP_text.text = $"{challenge.CP}";
        Update_UI_state();
    }

    private void Update_UI_state()
    {
        switch (challenge.userdata.state)
        {
            case challenge_state.incomplete:
                state_image.sprite = incomplete;
                reward_btn.interactable = false;
                break;
            case challenge_state.can_reward:
                state_image.sprite = can_reward;
                reward_btn.interactable = true;
                break;
            case challenge_state.complete:
                state_image.sprite = complete;
                reward_btn.interactable = false;
                break;
        }
    }

    public void Get_reward_btn() //����ޱ� ��ư�� �־�� �޼���
    {
        if (challenge.userdata.state != challenge_state.can_reward)
            return;

        Debug.Log(challenge.userdata.state + "����Ϸ�");

        Debug.Log("Get_reward_btn");

        reward_btn.interactable = false;
        challenge.Get_reward();
        Update_UI_state();
        QuestManager.instance.Challenge_update();
    }

    public void criterionUp_btn() //����ġ��� ��ư Ŭ���� ȣ�� - ����
    {
        if (challenge.userdata.state == challenge_state.incomplete)
        {
            challenge.userdata.criterion++;//����ġ ����
            challenge.userdata.Data_update();
        }

        if (challenge.Check_clear()) //Ŭ�����ߴٸ�
        {
            Update_UI_state();//state UI ������Ʈ
        }
    }
}
