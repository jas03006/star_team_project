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
            //select_name.text = $"{BackendChart_JGD.chartData.character_list[value].character_name}";
            select_name.text = $"{character_list[value].character_name}";
        }
    }
    private List<Character> character_list = new List<Character>();
    public static CharacterManager instance;
    private int Index;

    private int ark;
    private int gold;

    //UI
    [SerializeField] List<Character_pannel> pannels;

    //select
    [SerializeField] private TMP_Text select_name;

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
    }

    private void Start()
    {
        Test_set();

        //UI update
        for (int i = 0; i < pannels.Count; i++)
        {
            //Character character = BackendChart_JGD.chartData.character_list[i];
            Character character = character_list[i];
            pannels[i].UI_update(character);
        }
    }

    private void Test_set() //���Ŀ� ���� �����Ϳ��� �޾ƿ;���
    {
        ark = 1000;
        gold = 1000;

        for (int i = 0; i < 5; i++)
        {
            character_list.Add(new Character());
        }
    }

    public void Select_btn(int id) //ĳ���� ���� ��ư
    {
        index = id;
    }

    public void Inhance_btn(int id) //ĳ���� ��ȭ ��ư
    {
        //Character character = BackendChart_JGD.chartData.character_list[id];
        Character character = character_list[id];

        if (character.CanLevelup())
            character.Levelup();
    }

    private void GameStart()
    {
        PlayerPrefs.SetInt("select", index);
        //SceneManager.LoadScene("TestScence");
    }
}


