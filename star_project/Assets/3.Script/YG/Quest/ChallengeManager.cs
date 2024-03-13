using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum challenge_cate
{
    none = -1,
    common,
    play,
    community
}
public enum challenge_state
{
    none = -1,
    incomplete,//완료X, 보상수령X
    can_reward,//완료O, 보상수령X
    complete//완료O, 보상수령O
}
public class ChallengeManager : MonoBehaviour
{
    challenge_cate cate;

    List<Challenge> challenge_common = new List<Challenge>();
    List<Challenge> challenge_play = new List<Challenge>();
    List<Challenge> challenge_community = new List<Challenge>();

    [SerializeField] List<Image> btn_image = new List<Image>();

    [SerializeField] List<Challenge_prefab> challengeprefab_script_list = new List<Challenge_prefab>();
    [SerializeField] List<GameObject> challengeprefab_list = new List<GameObject>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject content_zone;

    [SerializeField] private TMP_Text CP;
    [SerializeField] private Slider CP_slider;

    [SerializeField] List<Button> borders_btn = new List<Button>();
    [SerializeField] List<Image> borders_img = new List<Image>();

    [SerializeField] private Sprite border_O;
    [SerializeField] private Sprite border_X;

    private housing_itemID[] cp_rewards = { housing_itemID.bench, housing_itemID.ball, housing_itemID.seesaw, housing_itemID.swing, housing_itemID.slider };


    private void Start()
    {
        Setting_data();
        Setting_prefab();
        Update_UI();

        BackendGameData_JGD.userData.house_inventory.Add(housing_itemID.seesaw, 1);
    }

    private void Setting_data()
    {
        foreach (var challenge in BackendChart_JGD.chartData.challenge_list)
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
        cate = challenge_cate.common;
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

    private void Update_UI()
    {
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

    public void Change_state(int index) //일반,플레이,커뮤니티에 달 버튼
    {
        cate = (challenge_cate)index;

        for (int i = 0; i < btn_image.Count; i++)
        {
            btn_image[i].enabled = i == index;
        }

        Setting_prefab();
        Update_UI();
    }

    public void CP_reward(int index)
    {

        if (BackendGameData_JGD.userData.quest_Info.challenge_states[index] != challenge_state.can_reward)
            return;

        BackendGameData_JGD.userData.quest_Info.challenge_states[index] = challenge_state.complete;

        BackendGameData_JGD.userData.house_inventory.Add(cp_rewards[index], 1);
        BackendGameData_JGD.Instance.GameDataUpdate();

        Update_UI();
    }
}
