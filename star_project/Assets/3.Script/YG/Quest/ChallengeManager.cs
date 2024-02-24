using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

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
            Update_UI();
        }
    }
    public challenge_id state_;

    List<Challenge> challenge_common = new List<Challenge>();
    List<Challenge> challenge_play = new List<Challenge>();
    List<Challenge> challenge_community = new List<Challenge>();

    List<Challenge_prefab> challenge_prefab_list = new List<Challenge_prefab>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject content_zone;

    private void Start()
    {
        Setting_data();
        Setting_prefab();
    }

    private void Setting_data()
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

    private List<Challenge> Get_list()
    {
        switch (state)
        {
            case challenge_id.common:
                return challenge_common;
            case challenge_id.play:
                return challenge_play;
            case challenge_id.community:
                return challenge_community;
            default:
                return null;
        }
    }

    private void Setting_prefab()
    {
        foreach (Challenge challenge in Get_list())
        {
            Make_prefab(challenge);
        }
    }

    private void Update_UI()
    {
        List<Challenge> cur_list = Get_list();

        for (int i = 0; i < challenge_prefab_list.Count; i++)
        {
            challenge_prefab_list[i].challenge = cur_list[i];
        }
    }

    public void Make_prefab(Challenge challenge)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(content_zone.transform, false);
        Canvas.ForceUpdateCanvases();

        Challenge_prefab challenge_prefab = obj.GetComponent<Challenge_prefab>();
        challenge_prefab.challenge = challenge;
        challenge_prefab.Update_UI();

        challenge_prefab_list.Add(challenge_prefab);
    }

    public void Change_state(int index) //�Ϲ�,�÷���,Ŀ�´�Ƽ�� �� ��ư
    {
        state = (challenge_id)index;
    }
}
