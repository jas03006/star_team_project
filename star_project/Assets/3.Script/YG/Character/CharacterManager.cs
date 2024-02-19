using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public int index
    {
        get { return Index; }
        set
        {
            Index = value;
            select_name.text = $"{BackendChart_JGD.chartData.character_list[value].character_name}";
        }
    }
    private int Index;

    //UI
    [SerializeField] List<Character_pannel> pannels;

    //select
    [SerializeField] private TMP_Text select_name;


    private void Start()
    {
        //UI update
        for (int i = 0; i < pannels.Count; i++)
        {
            Character character = BackendChart_JGD.chartData.character_list[i];
            pannels[i].UI_update(character);
        }
    }

    public void Select_btn(int id) //ĳ���� ���� ��ư
    {
        index = id;
    }

    public void Inhance_btn(int id) //ĳ���� ��ȭ ��ư
    {
        Character character = BackendChart_JGD.chartData.character_list[id];
        int gold_req, ark_req;

        if (character.CanLevelup(MoneyManager.instance.gold,MoneyManager.instance.ark,out gold_req,out ark_req))
        {
            character.Levelup(gold_req, ark_req);
            pannels[id].Level_update(character);
        }
        else
        {
            Debug.Log("���� ������ ��ȭ ����");
        }
    }

    private void GameStart()
    {
        PlayerPrefs.SetInt("select", index);
        //SceneManager.LoadScene("TestScence");
    }
}


