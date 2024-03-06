using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_UI : MonoBehaviour
{
    List<GameObject> prefab_btn_list = new List<GameObject>();

    [Header("btn_prefab")]
    [SerializeField] GameObject btn_prefab;
    [SerializeField] GameObject content_zone;


    private int index_UI;//현재 띄우고 있는 캐릭터 인덱스
    private Character index_character; //현재 띄우고 있는 캐릭터 정보
    private Character cur_character; //데이터에 넣을 캐릭터 정보

    [Header("cur_character")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Button is_equip_btn;
    [SerializeField] private Button levelup_btn;
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text special; //특수 능력
    [SerializeField] private TMP_Text unique; //고유 능력

    [Header("sprite")]
    [SerializeField] private Sprite equip_btn_O;
    [SerializeField] private Sprite equip_btn_X;
    [SerializeField] private Sprite levelup_O;
    [SerializeField] private Sprite levelup_X;
    [SerializeField] private Sprite select_border;
    [SerializeField] private Sprite notselect_border;

    void Start()
    {
        //List<Character> list = BackendChart_JGD.chartData.character_list;

        //for (int i = 0; i < list.Count; i++)
        //{
        //    if (list[i].curlevel >= 1)
        //    {
        //        Make_prefab(list[i].sprite,i);
        //    }
        //}

        //Setting();
    }

    private void OnEnable()
    {
        List<Character> list = BackendChart_JGD.chartData.character_list;
        //content_zone.transform.childCount != list.Count일 경우 오브젝트 삭제 후 재생성

        for (int i = 0; i < content_zone.transform.childCount; i++)
        {
            Destroy(content_zone.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].curlevel >= 1)
            {
                Make_prefab(list[i].sprite, i);
            }
        }

        Setting();
    }

    private void Setting()
    {
        index_character = BackendChart_JGD.chartData.character_list[0];
        cur_character = BackendChart_JGD.chartData.character_list[BackendGameData_JGD.userData.character];
        Update_UI();

        index_UI = 0;
    }

    private void Update_UI()
    {
        image.sprite = SpriteManager.instance.Num2Sprite(index_character.sprite);

        character_name.text = index_character.character_name;
        level.text = index_character.curlevel.ToString();
        HP.text = (100 + ((index_character.curlevel - 1) * 10)).ToString();
        special.text = index_character.special;
        unique.text = index_character.unique;


        if (cur_character.curlevel >= cur_character.maxlevel)
        {
            Update_btn(false, levelup_btn, levelup_O, levelup_X);
        }
        else
        {
            Update_btn(true, levelup_btn, levelup_O, levelup_X);
        }

        Update_is_equip();

        for (int i = 0; i < prefab_btn_list.Count; i++)
        {
            if (i == index_UI)
            {
                prefab_btn_list[i].GetComponent<Image>().sprite = select_border;
            }
            else
            {
                prefab_btn_list[i].GetComponent<Image>().sprite = notselect_border;
            }
        }

        foreach (GameObject item in prefab_btn_list)
        {
            //if (prefab_btn_list[])
            //    item.GetComponent<Image>().sprite
        }
    }

    private void Update_is_equip()
    {
        if (index_character == cur_character)
        {
            Update_btn(false, is_equip_btn, equip_btn_O, equip_btn_X);
        }
        else
        {
            Update_btn(true, is_equip_btn, equip_btn_O, equip_btn_X);
        }

    }

    private void Update_btn(bool bool_, Button btn, Sprite O, Sprite X)
    {
        if (bool_)
        {
            btn.interactable = true;
            btn.gameObject.GetComponent<Image>().sprite = O;
        }

        else
        {
            btn.interactable = false;
            btn.gameObject.GetComponent<Image>().sprite = X;
        }
    }

    private void Make_prefab(int sprite, int index)
    {
        GameObject newobj = Instantiate(btn_prefab, content_zone.transform);
        prefab_btn_list.Add(newobj);
        //newobj.transform.SetParent(content_zone.transform, false);

        Button button = newobj.GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => Set_cur_character_btn(index));
        }

        newobj.transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(sprite);
    }

    #region btn

    public void Set_cur_character_btn(int num)
    {
        index_UI = num;
        index_character = BackendChart_JGD.chartData.character_list[num];
        Update_UI();
        Debug.Log($"Set_cur_character_btn : {num}");
    }

    public void Equip_btn() //장착하기 버튼
    {
        cur_character = BackendChart_JGD.chartData.character_list[index_UI];
        cur_character.character_Data_update(index_UI);
        Update_is_equip();
    }

    public void Inhance_btn() //캐릭터 강화 버튼
    {
        int gold_req, ark_req;

        if (index_character.CanLevelup(MoneyManager.instance.gold, MoneyManager.instance.ark, out gold_req, out ark_req))
        {
            index_character.Levelup(gold_req, ark_req);
            Update_UI();
        }
        else
        {
            Debug.Log("돈이 부족해 강화 실패");
        }
    }

    #endregion
}
