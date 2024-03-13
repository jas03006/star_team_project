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

        if (challenge.userdata.criterion > challenge.goal)
        {
            count_text.text = "(" + challenge.goal + "/" + challenge.goal + ")";
        }
        else
        {
            count_text.text =  "(" +challenge.userdata.criterion+ "/"+ challenge.goal + ")";
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
                break;
            case challenge_state.can_reward:
                state_image.sprite = can_reward;
                break;
            case challenge_state.complete:
                state_image.sprite = complete;
                break;
        }
    }

    public void Get_reward_btn() //����ޱ� ��ư�� �־�� �޼���
    {
        if (challenge.userdata.state != challenge_state.can_reward)
            return;

        Debug.Log("Get_reward_btn");
        Update_UI_state();

        reward_btn.interactable = false;
        challenge.Get_reward();
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
