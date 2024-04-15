using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� �����͸� �޾ƿ� UI ������ִ� Ŭ����.
/// </summary>
public class ChallengeManager : MonoBehaviour
{
    challenge_cate cate;//���� ������ ī�װ�

    [Header("Challenge_list")]//ī�װ��� ���� ����
    List<Challenge> challenge_common = new List<Challenge>();
    List<Challenge> challenge_play = new List<Challenge>();
    List<Challenge> challenge_community = new List<Challenge>();

    [SerializeField] List<Image> btn_image = new List<Image>();//ī�װ� ���ù�ư �̹���

    [Header("Prefab")]
    [SerializeField] List<Challenge_prefab> challengeprefab_script_list = new List<Challenge_prefab>();//������ ��ũ��Ʈ�� ����� list
    [SerializeField] List<GameObject> challengeprefab_list = new List<GameObject>();//������ ������ ������Ʈ�� ����� list
    [SerializeField] private GameObject prefab;//������ ������ ������Ʈ
    [SerializeField] private GameObject content_zone; //������ ������ ��ġ

    [Header("CP_UI")]
    [SerializeField] private TMP_Text CP;
    [SerializeField] private Slider CP_slider;

    //�̼� ���� ���¿� ���� ����� ��ư ������Ʈ
    [SerializeField] List<Button> borders_btn = new List<Button>();
    //�̼� ���� ���¿� ���� ����� �̹��� ������Ʈ
    [SerializeField] List<Image> borders_img = new List<Image>();

    //������Ʈ�� �� ��������Ʈ.
    [SerializeField] private Sprite border_O;
    [SerializeField] private Sprite border_X;
    private bool is_change; //true�� ��� DB�� ����� ������ ����

    //������� CP�̼� �޼� �� ���޵Ǵ� ������
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
        // get_rewarded�� true�� �����۵��� �ӽ÷� ������ ����Ʈ
        List<Challenge> rewardedChallenges = new List<Challenge>();

        // get_rewarded�� true�� ���, rewardedChallenges�� �߰��ϰ� ���� ����Ʈ������ ����
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].userdata.state == challenge_state.complete)
            {
                rewardedChallenges.Add(list[i]);
                list.RemoveAt(i);
            }
        }

        // rewardedChallenges�� �����۵��� ���� ����Ʈ�� �� �ڷ� �߰�
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

        if (challengeprefab_script_list.Count == 0) //ó�� ����
        {
            //Debug.Log("ó�� ����");
            for (int i = 0; i < get_list_count; i++)
            {
                Make_prefab(Get_list()[i]);
            }
        }

        else if (prefab_count > get_list_count)
        {
            //Debug.Log($"{prefab_count - get_list_count}�� ����");
            for (int i = 0; i < prefab_count - get_list_count; i++)
            {
                Destroy(challengeprefab_list[i]);

                challengeprefab_list.Remove(challengeprefab_list[i]);
                challengeprefab_script_list.Remove(challengeprefab_script_list[i]);
            }
        }

        else if (prefab_count < get_list_count)
        {
            //Debug.Log($"{prefab_count - get_list_count}�� ����");
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

    public void Change_state(int index) //�Ϲ�,�÷���,Ŀ�´�Ƽ ���� ��ư Ŭ���� ���� 
    {
        cate = (challenge_cate)index;

        for (int i = 0; i < btn_image.Count; i++)
        {
            btn_image[i].enabled = i == index;
        }

        Setting_prefab();
        Update_UI();
    }

    public void CP_reward(int index) //���� ��ư Ŭ�� �� �����.
    {

        if (BackendGameData_JGD.userData.quest_Info.challenge_states[index] != challenge_state.can_reward)
            return;

        BackendGameData_JGD.userData.quest_Info.challenge_states[index] = challenge_state.complete;

        BackendGameData_JGD.userData.house_inventory.Add(cp_rewards[index], 1);
        BackendGameData_JGD.Instance.GameDataUpdate();

        Update_UI();
    }
}
