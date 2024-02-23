using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Challenge_prefab : MonoBehaviour
{
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text contents_text;
    [SerializeField] TMP_Text count_text;
    [SerializeField] TMP_Text state_text;

    public void UI_update(Challenge challenge)
    {
        name_text.text = challenge.title;
        contents_text.text = challenge.contents;
        count_text.text = challenge.contents2 + $"{challenge.userdata.criterion} / {challenge.goal}";

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
}
