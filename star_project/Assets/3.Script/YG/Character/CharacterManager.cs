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

    private int ark;
    private int gold;

    //UI
    [SerializeField] List<Character_pannel> pannels;

    //select
    [SerializeField] private TMP_Text select_name;


    private void Start()
    {
        Data_set();

        //UI update
        for (int i = 0; i < pannels.Count; i++)
        {
            Character character = BackendChart_JGD.chartData.character_list[i];
            pannels[i].UI_update(character);
        }
    }

    private void Data_set() //���Ŀ� ���� �����Ϳ��� �޾ƿ;���
    {
        ark = BackendGameData_JGD.userData.ark;
        gold = BackendGameData_JGD.userData.gold;
    }

    public void Select_btn(int id) //ĳ���� ���� ��ư
    {
        index = id;
    }

    public void Inhance_btn(int id) //ĳ���� ��ȭ ��ư
    {
        Character character = BackendChart_JGD.chartData.character_list[id];

        if (character.CanLevelup(gold,ark))
        {       
            character.Levelup();
        }
    }

    private void GameStart()
    {
        PlayerPrefs.SetInt("select", index);
        //SceneManager.LoadScene("TestScence");
    }
}


