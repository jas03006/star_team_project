using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_UI : MonoBehaviour
{
    [SerializeField] int child_cnt;

    [SerializeField] Scrollbar scrollbar;

    [Header("btn_prefab")]
    [SerializeField] GameObject btn_prefab;
    [SerializeField] GameObject content_zone;


    private int index_UI
    {
        get { return index_UI_; }
        set
        {
            index_UI_ = value;
            Update_UI();
        }
    }
    private int index_UI_;//���� ���� �ִ� ĳ���� �ε���
    private Character index_character; //���� ���� �ִ� ĳ���� ����
    private Character cur_character; //�����Ϳ� ���� ĳ���� ����

    [Header("cur_character")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text is_equip;
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text special; //Ư�� �ɷ�
    [SerializeField] private TMP_Text unique; //���� �ɷ�

    void Start()
    {
        List<Character> list = BackendChart_JGD.chartData.character_list;

        for (int i = 0; i < list.Count; i++)
        {
            Make_prefab(list[i].sprite);
        }
    }

    public void Update_UI()
    {
        image.sprite = SpriteManager.instance.Num2Sprite(index_character.sprite);
        character_name.text = index_character.character_name;
        level.text = $"���� : {index_character.curlevel}";
        HP.text = $"HP : {100 + (index_character.curlevel * 10)}";
        is_equip.text = index_character == cur_character ? "������" : "���� �Ϸ�";
        special.text = index_character.special;
        unique.text = index_character.unique;
    }

    public void Make_prefab(int sprite)
    {
        GameObject newobj = Instantiate(btn_prefab);
        newobj.transform.SetParent(content_zone.transform, false);
        newobj.transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(sprite);
        Canvas.ForceUpdateCanvases();
    }

    #region btn
    public void click_scroll_btn(bool is_right)
    {
        if (is_right)
        {
            scrollbar.value += 1f / (float)(child_cnt - 10);
        }
        else
        {
            scrollbar.value -= 1f / (float)(child_cnt - 10);
        }
    }

    public void Set_cur_character_btn(int num)
    {
        index_UI = num;
        index_character = BackendChart_JGD.chartData.character_list[num];
    }

    public void Equip_btn() //�����ϱ� ��ư
    {
        cur_character = BackendChart_JGD.chartData.character_list[index_UI];
        cur_character.character_Data_update(index_UI);
    }

    public void Inhance_btn() //ĳ���� ��ȭ ��ư
    {
        int gold_req, ark_req;

        if (index_character.CanLevelup(MoneyManager.instance.gold, MoneyManager.instance.ark, out gold_req, out ark_req))
        {
            index_character.Levelup(gold_req, ark_req);
            level.text = $"���� : {index_character.curlevel}";
        }
        else
        {
            Debug.Log("���� ������ ��ȭ ����");
        }
    }

    #endregion
}
