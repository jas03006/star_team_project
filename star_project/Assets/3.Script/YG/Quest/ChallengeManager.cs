using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 업적 데이터를 받아와 UI 출력해주는 클래스.
/// </summary>
public class ChallengeManager : MonoBehaviour
{
    challenge_cate cate;//현재 선택한 카테고리

    [Header("Challenge_list")]//카테고리별 업적 정보
    List<Challenge> challenge_common = new List<Challenge>();
    List<Challenge> challenge_play = new List<Challenge>();
    List<Challenge> challenge_community = new List<Challenge>();

    [SerializeField] List<Image> btn_image = new List<Image>();//카테고리 선택버튼 이미지

    [Header("Prefab")]
    [SerializeField] List<Challenge_prefab> challengeprefab_script_list = new List<Challenge_prefab>();//프리펩 스크립트가 저장된 list
    [SerializeField] List<GameObject> challengeprefab_list = new List<GameObject>();//생성된 프리펩 오브젝트가 저장된 list
    [SerializeField] private GameObject prefab;//생성할 프리펩 오브젝트
    [SerializeField] private GameObject content_zone; //프리펩 생성할 위치

    [Header("CP_UI")]
    [SerializeField] private TMP_Text CP;
    [SerializeField] private Slider CP_slider;

    //미션 진행 상태에 따라 변경될 버튼 컴포넌트
    [SerializeField] List<Button> borders_btn = new List<Button>();
    //미션 진행 상태에 따라 변경될 이미지 컴포넌트
    [SerializeField] List<Image> borders_img = new List<Image>();

    //컴포넌트에 들어갈 스프라이트.
    [SerializeField] private Sprite border_O;
    [SerializeField] private Sprite border_X;
    private bool is_change; //true일 경우 DB에 변경된 데이터 전송

    //순서대로 CP미션 달성 시 지급되는 아이템
    private housing_itemID[] cp_rewards = { housing_itemID.ailen, housing_itemID.dessert, housing_itemID.tundra, housing_itemID.frozen, housing_itemID.earth };
    

    private void Start()
    {
        Setting_data();
        Setting_prefab();
        Update_UI();
    }

    private void Setting_data()
    {
        foreach (Challenge challenge in BackendChart_JGD.chartData.challenge_list)
        {
            switch (challenge.challenge_cate)
            {
                case challenge_cate.common:
                    challenge_common.Add(challenge);
                    break;
                case challenge_cate.play:
                    challenge_play.Add(challenge);
                    break;
                case challenge_cate.community:
                    challenge_community.Add(challenge);
                    break;
                default:
                    break;
            }
        }
        All_SortChallengeList();
        cate = challenge_cate.common;
    }

    private void All_SortChallengeList()
    {
        SortChallengeList(challenge_common);
        SortChallengeList(challenge_play);
        SortChallengeList(challenge_community);
    }

    private void SortChallengeList(List<Challenge> list)
    {
        // get_rewarded가 true인 아이템들을 임시로 저장할 리스트
        List<Challenge> rewardedChallenges = new List<Challenge>();

        // get_rewarded가 true인 경우, rewardedChallenges에 추가하고 기존 리스트에서는 제거
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].userdata.state == challenge_state.complete)
            {
                rewardedChallenges.Add(list[i]);
                list.RemoveAt(i);
            }
        }

        // rewardedChallenges의 아이템들을 원래 리스트의 맨 뒤로 추가
        foreach (Challenge challenge in rewardedChallenges)
        {
            list.Add(challenge);
        }
    }

    private List<Challenge> Get_list()
    {
        switch (cate)
        {
            case challenge_cate.common:
                return challenge_common;
            case challenge_cate.play:
                return challenge_play;
            case challenge_cate.community:
                return challenge_community;
            default:
                return null;
        }
    }

    private void Setting_prefab()
    {
        int prefab_count = challengeprefab_script_list.Count;
        int get_list_count = Get_list().Count;
        Debug.Log($"{prefab_count} / {get_list_count}");
        if (prefab_count == get_list_count)
            return;

        if (challengeprefab_script_list.Count == 0) //처음 생성
        {
            //Debug.Log("처음 생성");
            for (int i = 0; i < get_list_count; i++)
            {
                Make_prefab(Get_list()[i]);
            }
        }

        else if (prefab_count > get_list_count)
        {
            //Debug.Log($"{prefab_count - get_list_count}개 삭제");
            for (int i = 0; i < prefab_count - get_list_count; i++)
            {
                Destroy(challengeprefab_list[i]);

                challengeprefab_list.Remove(challengeprefab_list[i]);
                challengeprefab_script_list.Remove(challengeprefab_script_list[i]);
            }
        }

        else if (prefab_count < get_list_count)
        {
            //Debug.Log($"{prefab_count - get_list_count}개 생성");
            for (int i = 0; i < get_list_count - prefab_count; i++)
            {
                Make_prefab(Get_list()[i]);
            }
        }
    }

    public void Update_UI()
    {
        All_SortChallengeList();
        List<Challenge> cur_list = Get_list();

        for (int i = 0; i < challengeprefab_script_list.Count; i++)
        {
            challengeprefab_script_list[i].challenge = cur_list[i];
            challengeprefab_script_list[i].Update_UI();
        }

        int cp = BackendGameData_JGD.userData.CP;
        CP.text = cp.ToString();
        CP_slider.value = cp;

        for (int i = 0; i < borders_img.Count; i++)
        {
            if (cp <= (i + 1) * 1000)
            {
                borders_img[i].sprite = border_X;
            }
            else
            {
                borders_img[i].sprite = border_O;

                if (BackendGameData_JGD.userData.quest_Info.challenge_states[i] == challenge_state.incomplete)
                {
                    BackendGameData_JGD.userData.quest_Info.challenge_states[i] = challenge_state.can_reward;
                    is_change = true;
                }
            }

            switch (BackendGameData_JGD.userData.quest_Info.challenge_states[i])
            {
                case challenge_state.incomplete:
                    borders_btn[i].interactable = false;
                    borders_btn[i].transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case challenge_state.can_reward:
                    borders_btn[i].interactable = true;
                    borders_btn[i].transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case challenge_state.complete:
                    borders_btn[i].interactable = false;
                    borders_btn[i].transform.GetChild(2).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        if (is_change)
        {
            BackendGameData_JGD.Instance.GameDataUpdate();
            is_change=false;
        }
    }

    public void Make_prefab(Challenge challenge)
    {
        GameObject obj = Instantiate(prefab, content_zone.transform);
        challengeprefab_list.Add(obj);

        Challenge_prefab challenge_prefab = obj.GetComponent<Challenge_prefab>();
        challenge_prefab.challenge = challenge;
        challenge_prefab.Update_UI();

        challengeprefab_script_list.Add(challenge_prefab);
    }

    public void Change_state(int index) //일반,플레이,커뮤니티 변경 버튼 클릭시 실행 
    {
        cate = (challenge_cate)index;

        for (int i = 0; i < btn_image.Count; i++)
        {
            btn_image[i].enabled = i == index;
        }

        Setting_prefab();
        Update_UI();
    }

    public void CP_reward(int index) //보상 버튼 클릭 시 실행됨.
    {

        if (BackendGameData_JGD.userData.quest_Info.challenge_states[index] != challenge_state.can_reward)
            return;

        BackendGameData_JGD.userData.quest_Info.challenge_states[index] = challenge_state.complete;

        BackendGameData_JGD.userData.house_inventory.Add(cp_rewards[index], 1);
        BackendGameData_JGD.Instance.GameDataUpdate();

        Update_UI();
    }
}
