using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Icon : MonoBehaviour
{
    //Icon
    public Image Icon;
    public Sprite O;
    public Sprite X;

    [SerializeField] List<Mission_userdata> missions = new List<Mission_userdata>();
    [SerializeField] List<Challenge_userdata> challenges = new List<Challenge_userdata>();


    void Start()
    {
        Check_CanReward();
        Icon_change();
        Debug_count();
    }

    public void Check_CanReward()
    {
        foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.mission_userdata)
        {
            if (data.is_clear && !data.get_rewarded)
            {
                missions.Add(data);
            }
        }

        foreach (Challenge_userdata data in BackendGameData_JGD.userData.challenge_Userdatas)
        {
            if (data.is_clear && !data.get_rewarded)
            {
                challenges.Add(data);
            }
        }
    }

    public void Remove(Mission_userdata mission=null, Challenge_userdata challenge = null)
    {
        if (mission != null && missions.Contains(mission))
        {
            missions.Remove(mission);
        }

        if (challenge != null && challenges.Contains(challenge))
        {
            challenges.Remove(challenge);
        }

        Icon_change();
        Debug_count();
    }

    public void Icon_change()
    {
        Icon.sprite = missions.Count == 0 && challenges.Count == 0 ? O : X;
    }
    public void Debug_count()
    {
        Debug.Log($"미션:{missions.Count} / 업적:{challenges.Count}");
    }
}
