using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager_ : MonoBehaviour
{
    public static MissionManager_ instance;

    public List<Mission> missions = new List<Mission>(); //전체미션
    public List<CriterionType> cur_missiontypes = new List<CriterionType>(); //미션 수락 시 내용 담김

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Setting();
    }

    public void Setting()
    {
        bool is_change = false;

        foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.userdata)
        {
            Mission mission = BackendChart_JGD.chartData.mission_list[data.mission_id - 1];

            if (data.criterion_type == CriterionType.none)
            {
                data.criterion_type = mission.criterion_type;
                is_change = true;
            }

            missions.Add(mission);

            if (data.is_accept && !cur_missiontypes.Contains(mission.criterion_type))
            {
                cur_missiontypes.Add(mission.criterion_type);
            }
        }

        if (is_change)
        {
            BackendGameData_JGD.userData.quest_Info.userdata[0].Data_update();
        }
    }

    //CriterionType에 맞는 수락한 미션이 있는지 확인
    //조건에서 해당 메서드 부르면 됨
    public void Check_mission(CriterionType type)
    {
        if (cur_missiontypes.Contains(type))
        {
            foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.userdata)
            {
                if (data.criterion_type == type)
                {
                    data.criterion++;
                    data.Data_update();
                }
            }
        }
    }

    public Mission_userdata Mission2data(Mission mission)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission)
            {
                return BackendGameData_JGD.userData.quest_Info.userdata[i + 1];
            }
        }
        Debug.Log("대응하는 Mission_userdata 없음");
        return null;
    }

}
