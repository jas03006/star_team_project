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

    private int index_UI;//���� ���� �ִ� ĳ���� �ε���
    private Character index_character; //���� ���� �ִ� ĳ���� ����
    private Character cur_character; //�����Ϳ� ���� ĳ���� ����

    [Header("cur_character")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Button is_equip_btn;
    [SerializeField] private Button leveluppannel_btn;
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text special; //Ư�� �ɷ�
    [SerializeField] private TMP_Text unique; //���� �ɷ�

    [Header("sprite")]
    [SerializeField] private Sprite equip_btn_O;
    [SerializeField] private Sprite equip_btn_X;
    [SerializeField] private Sprite levelup_O;
    [SerializeField] private Sprite levelup_X;
    [SerializeField] private Sprite select_border;
    [SerializeField] private Sprite notselect_border;

    [Header("done pannel")]
    [SerializeField] private GameObject donepannel;

    [Header("Levelup")]
    [SerializeField] private TMP_Text next_hp;
    [SerializeField] private TMP_Text cur_hp;
    [SerializeField] private TMP_Text next_level;
    [SerializeField] private TMP_Text cur_level;
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text ark;
    [SerializeField] private TMP_Text basic;
    [SerializeField] private Button levelup_btn;
    [SerializeField] private GameObject max_level;
    [SerializeField] private GameObject max_hp;

    private void OnEnable()
    {
        List<Character> list = BackendChart_JGD.chartData.character_list;
        //content_zone.transform.childCount != list.Count�� ��� ������Ʈ ���� �� �����



        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].curlevel >= 1)
            {
                Make_prefab(list[i].sprite, i);
            }
        }

        Setting();
    }

    private void OnDisable()
    {
        for (int i = 0; i < content_zone.transform.childCount; i++)
        {
            Destroy(content_zone.transform.GetChild(i).gameObject);
        }

        prefab_btn_list.Clear();
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

        if (index_character.character_ID == Character_ID.Green)
        {
            unique.text = index_character.unique + index_character.percent + "% �߰� ����";
        }
        else
        {
            unique.text = index_character.unique + index_character.duration + "�� ����";
        }


        if (cur_character.curlevel >= cur_character.maxlevel)
        {
            Update_btn(false, levelup_btn, levelup_O, levelup_X);
        }
        else
        {
            Update_btn(true, levelup_btn, levelup_O, levelup_X);
        }

        Update_is_equip();
        btn_update();
    }

    private void btn_update()
    {
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


            if (i == BackendGameData_JGD.userData.character)
            {
                prefab_btn_list[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                prefab_btn_list[i].transform.GetChild(1).gameObject.SetActive(false);
            }
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

        btn_update();
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

    public void Equip_btn() //�����ϱ� ��ư
    {
        cur_character = BackendChart_JGD.chartData.character_list[index_UI];
        cur_character.character_Data_update(index_UI);
        Update_is_equip();
        TCP_Client_Manager.instance.my_player.update_model((int)cur_character.character_ID);
    }

    public void Inhance_btn() //ĳ���� ��ȭ ��ư
    {
        int gold_req, ark_req;

        if (index_character.CanLevelup(MoneyManager.instance.gold, MoneyManager.instance.ark, out gold_req, out ark_req))
        {
            index_character.Levelup(gold_req, ark_req);
            Update_UI();
            Enable_levelup_pannel_btn();
        }
        else
        {
            Debug.Log("���� ������ ��ȭ ����");
        }
    }

    #endregion
    #region levelup
    public void Enable_levelup_pannel_btn()
    {
        Character_amount cur_chartdata = BackendChart_JGD.chartData.Characteramount_list[index_character.curlevel - 1];
        Character_amount next_chartdata = null;

        if (index_character.curlevel < BackendChart_JGD.chartData.Characteramount_list.Count)
        {
            next_chartdata = BackendChart_JGD.chartData.Characteramount_list[index_character.curlevel];
        }

        if (next_chartdata == null)
        {
            max_hp.SetActive(true);
            max_level.SetActive(true);

            max_hp.transform.GetChild(0).GetComponent<TMP_Text>().text = (100 + ((cur_chartdata.level - 1) * 10)).ToString();
            max_level.transform.GetChild(0).GetComponent<TMP_Text>().text = cur_chartdata.level.ToString();

            Colored_text(false, "MAX", gold);
            Colored_text(false, "MAX", ark);

            levelup_btn.interactable = false;

            if (index_character.character_ID == Character_ID.Green)
            {
                basic.text = index_character.unique + index_character.percent + "% �߰� ����";
            }
            else
            {
                basic.text = index_character.unique + index_character.duration + "�� ����";
            }
            return;
        }

        max_hp.SetActive(false);
        max_level.SetActive(false);

        cur_level.text = cur_chartdata.level.ToString();
        next_level.text = next_chartdata.level.ToString();

        cur_hp.text = (100 + ((cur_chartdata.level - 1) * 10)).ToString();
        next_hp.text = (100 + ((next_chartdata.level - 1) * 10)).ToString();

        levelup_btn.interactable = true;

        if (index_character.character_ID == Character_ID.Green)
        {
            basic.text = index_character.unique + index_character.percent + "(<color=#B4FC11>" + 0.5 + "</color>)" + "% �߰� ����";
        }
        else
        {
            basic.text = index_character.unique + index_character.duration + "(<color=#B4FC11>" + 0.1 + "</color>)" + "�� ����";
        }

        Colored_text(MoneyManager.instance.gold > cur_chartdata.gold, cur_chartdata.gold, gold);
        Colored_text(MoneyManager.instance.ark > cur_chartdata.ark, cur_chartdata.ark, ark);

    }

    private void Colored_text(bool bool_, object obj, TMP_Text text)
    {
        if (bool_)
        {
            text.text = "<color=#FFFFFF>" + obj + "</color>";
        }
        else
        {
            text.text = "<color=#FF511A>" + obj + "</color>";
        }
        Debug.Log(obj);
    }
    #endregion
}
