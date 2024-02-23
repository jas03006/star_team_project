using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
�ؾߵɰ� : �鿣����Ʈ�� ç���� ������ �߰��ϱ�

 */
public enum challenge_id
{
    none = -1,
    common,
    play,
    community
}

public enum challenge_state
{
    none = -1,
    incomplete,//�Ϸ�X, �������X
    get_reward,//�Ϸ�O, �������X
    complete//�Ϸ�O, �������O
}
public class ChallengeManager : MonoBehaviour
{
    challenge_id state
    {
        get { return state_; }
        set
        {
            state_ = value;
            //Reset_btn();
        }
    }

    public challenge_id state_;
    List<Challenge> missions_common = new List<Challenge>();
    List<Challenge> missions_play = new List<Challenge>();
    List<Challenge> missions_community = new List<Challenge>();
    Challenge cur_challenge;

    [Header("Instantiate")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject content_zone;

    [Header("UI")]
    [SerializeField] private Button reward_btn;


    private void Start()
    {
        Setting();
    }

    private void Setting()//�̼� ������ �ҷ�����
    {
        List<Challenge> challenges = BackendChart_JGD.chartData.challenge_list;

        foreach (var challenge in challenges)
        {
            switch (challenge.id)
            {
                case challenge_id.common:
                    missions_common.Add(challenge);
                    break;
                case challenge_id.play:
                    missions_play.Add(challenge);
                    break;
                case challenge_id.community:
                    missions_community.Add(challenge);
                    break;
                default:
                    break;
            }
        }

        state = challenge_id.common;
        reward_btn.enabled = false;
    }

    public void Make_prefab()
    {
        GameObject obj = Instantiate(prefab);
    }
}
