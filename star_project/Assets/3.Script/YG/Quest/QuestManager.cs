using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    //mission
    public List<Mission> missions = new List<Mission>(); //전체미션
    public List<Criterion_type> cur_missiontypes = new List<Criterion_type>(); //미션 수락 시 내용 담김
    public int friend_num;

    //challenge
    public ChallengeManager manager = null;

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
            return;
        }

        Setting();
        manager = null;
    }

    public void Setting()
    {
        Debug.Log("설정 시작!");
        bool is_change = false;

        foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.mission_userdata)
        {
            data.criterion_type = BackendChart_JGD.chartData.mission_list[data.mission_id - 1].criterion_type;

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

            //Debug_mission(mission);
        }

        if (is_change)
        {
            BackendGameData_JGD.Instance.GameDataUpdate();
        }
    }

    private void Debug_mission(Mission mission)
    {
        Debug.Log(mission.mission_id + "/" + mission.criterion_type + "/" + mission.mission_type);
    }

    public void Challenge_update()
    {
        if (manager == null)
        {
            manager = FindObjectOfType<ChallengeManager>();
        }
        manager.Update_UI();
    }

    public void Test_btn(int index)
    {
        Debug.Log(index + "/" + (Criterion_type)index);
        Debug.Log(cur_missiontypes.Count);
        Check_mission((Criterion_type)index);
    }

    //CriterionType에 맞는 수락한 미션이 있는지 확인
    //조건에서 해당 메서드 부르면 됨
    public void Check_mission(Criterion_type type, int num = 1)
    {
        if (cur_missiontypes.Contains(type))
        {
            Debug.Log("미션 있음");
            foreach (Mission_userdata data in BackendGameData_JGD.userData.quest_Info.mission_userdata)
            {
                if (data.criterion_type == type)
                {

                    Debug.Log("해당 미션 번호 : " + data.mission_id);
                    data.criterion += num;
                    BackendGameData_JGD.Instance.GameDataUpdate();
                    return;
                }
            }
        }
        Debug.Log("미션 없음");

        string str = null;
        for (int i = 0; i < cur_missiontypes.Count; i++)
        {
            str += $"{cur_missiontypes[i]}/";
        }
        Debug.Log($"현재 미션{str}");
    }

    //Clear_type에 맞는 챌린지 업데이트
    //조건에서 해당 메서드 부르면 됨
    public void Check_challenge(Clear_type type, int num = 1)
    {
        List<Challenge> list = BackendChart_JGD.chartData.challenge_list;
        //FriendList_JGD.friend_dic.Count
        for (int i = 0; i < BackendGameData_JGD.userData.challenge_Userdatas.Count; i++)
        {
            Challenge_userdata data = BackendGameData_JGD.userData.challenge_Userdatas[i];
            if (data.state == challenge_state.incomplete && list[i].clear_type == type)
            {
                if (type == Clear_type.add_friend)
                {
                    if (FriendList_JGD.friend_dic.Count > BackendGameData_JGD.userData.quest_Info.challenge_dic[data.clear_Type])
                    {
                        BackendGameData_JGD.userData.quest_Info.challenge_dic[data.clear_Type] = FriendList_JGD.friend_dic.Count;
                    }
                }

                else
                {
                    BackendGameData_JGD.userData.quest_Info.challenge_dic[data.clear_Type] += num;
                }

                BackendGameData_JGD.Instance.GameDataUpdate();
                return;
            }
        }
    }

    public Mission_userdata Mission2data(Mission mission)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission)
            {
                return BackendGameData_JGD.userData.quest_Info.mission_userdata[i];
            }
        }
        Debug.Log("대응하는 Mission_userdata 없음");
        return null;
    }

    #region 디버그
    //public void ALL_Accept_btn() //수락버튼 클릭 시 호출
    //{
    //    for (int i = 0; i < BackendGameData_JGD.userData.quest_Info.mission_userdata.Count; i++)
    //    {
    //        Accept_mission(i);
    //    }
    //    Debug.Log("모두 수락완료");
    //}

    //public void Accept_mission(int index)
    //{
    //    Mission_userdata mission = BackendGameData_JGD.userData.quest_Info.mission_userdata[index];
    //    mission.is_accept = true;
    //    cur_missiontypes.Add(mission.criterion_type);
    //    Debug.Log("수락완료");
    //}
    #endregion
}
