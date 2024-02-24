using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    can_reward,//�Ϸ�O, �������X
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
            UI_update();
        }
    }

    public challenge_id state_;
    List<Challenge> challenge_common = new List<Challenge>();
    List<Challenge> challenge_play = new List<Challenge>();
    List<Challenge> challenge_community = new List<Challenge>();
    Challenge cur_challenge;

    [Header("Instantiate")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject content_zone;


    private void Start()
    {
        Setting();
        UI_update();
    }

    private void Setting()//�̼� ������ �ҷ�����
    {
        foreach (var challenge in BackendChart_JGD.chartData.challenge_list)
        {
            switch (challenge.id)
            {
                case challenge_id.common:
                    challenge_common.Add(challenge);
                    break;
                case challenge_id.play:
                    challenge_play.Add(challenge);
                    break;
                case challenge_id.community:
                    challenge_community.Add(challenge);
                    break;
                default:
                    break;
            }
        }
        state = challenge_id.common;
    }

    private void UI_update()
    {
        List<Challenge> cur_list = new List<Challenge>();
        switch (state)
        {
            case challenge_id.common:
                cur_list = challenge_common;
                break;
            case challenge_id.play:
                cur_list = challenge_play;
                break;
            case challenge_id.community:
                cur_list = challenge_community;
                break;
            default:
                break;
        }

        foreach (var challenge in cur_list)
        {
            Make_prefab(challenge);
        }
    }

    public void Make_prefab(Challenge challenge)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(content_zone.transform, false);
        Canvas.ForceUpdateCanvases();

        Challenge_prefab challenge_prefab = obj.GetComponent<Challenge_prefab>();
        challenge_prefab.UI_update(challenge);
    }

    public void Change_state(int tmp) //�Ϲ�,�÷���,Ŀ�´�Ƽ�� �� ��ư
    {
        state = (challenge_id)tmp;
    }
}
