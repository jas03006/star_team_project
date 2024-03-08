using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    //mission
    public List<Mission> missions = new List<Mission>(); //전체미션
    public List<Criterion_type> cur_missiontypes = new List<Criterion_type>(); //미션 수락 시 내용 담김

    public int friend_num;

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
        Debug.Log("설정 시작!");
        bool is_change = false;

        foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.mission_userdata)
        {
            Mission mission = BackendChart_JGD.chartData.mission_list[data.mission_id - 1];

            if (data.criterion_type == Criterion_type.none)
            {
                data.criterion_type = mission.criterion_type;
                is_change = true;
            }

            missions.Add(mission);

            if (data.is_accept && !cur_missiontypes.Contains(mission.criterion_type))
            {
                cur_missiontypes.Add(mission.criterion_type);
            }

            Debug_mission(mission);
        }

        if (is_change)
        {
            BackendGameData_JGD.userData.quest_Info.mission_userdata[0].Data_update();
        }
    }

    private void Debug_mission(Mission mission)
    {
        Debug.Log(mission.mission_id + "/" + mission.criterion_type);
    }

    public void Test_btn(int index)
    {
        Debug.Log(index + "/"+ (Criterion_type)index);
        Debug.Log(cur_missiontypes.Count);
        Check_mission((Criterion_type)index);
    }

    //CriterionType에 맞는 수락한 미션이 있는지 확인
    //조건에서 해당 메서드 부르면 됨
    public void Check_mission(Criterion_type type)
    {
        if (cur_missiontypes.Contains(type))
        {
            Debug.Log("미션 있음");
            foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.mission_userdata)
            {
                if (data.criterion_type == type)
                {
                    Debug.Log("해당 미션 번호 : "+ data.mission_id);
                    data.criterion++;
                    data.Data_update();
                }
            }
        }
        Debug.Log("미션 없음");
    }

    //Clear_type에 맞는 챌린지 업데이트
    //조건에서 해당 메서드 부르면 됨
    public void Check_challenge(Clear_type type)
    {
       List<Challenge> list = BackendChart_JGD.chartData.challenge_list;

        for (int i = 0; i < BackendGameData_JGD.userData.challenge_Userdatas.Count; i++)
        {
            Challenge_userdata data = BackendGameData_JGD.userData.challenge_Userdatas[i];
            if (data.state == challenge_state.incomplete && list[i].clear_type == type)
            {
                data.criterion++;
                data.Data_update();
            }
        }
    }

    public Mission_userdata Mission2data(Mission mission)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission)
            {
                return BackendGameData_JGD.userData.quest_Info.mission_userdata[i + 1];
            }
        }
        Debug.Log("대응하는 Mission_userdata 없음");
        return null;
    }

    public void ALL_Accept_btn() //수락버튼 클릭 시 호출
    {
        for (int i = 0; i < BackendGameData_JGD.userData.quest_Info.mission_userdata.Count; i++)
        {
            Accept_mission(i);
        }
        Debug.Log("모두 수락완료");
    }

    public void Accept_mission(int index)
    {
        Mission_userdata mission =BackendGameData_JGD.userData.quest_Info.mission_userdata[index];
        mission.is_accept = true;
        cur_missiontypes.Add(mission.criterion_type);
        Debug.Log("수락완료");
    }

}
