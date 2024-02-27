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


    private int index_UI;//현재 띄우고 있는 캐릭터 인덱스
    private Character index_character; //현재 띄우고 있는 캐릭터 정보
    private Character cur_character; //데이터에 넣을 캐릭터 정보

    [Header("cur_character")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text is_equip;
    [SerializeField] private Button is_equip_btn;
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text special; //특수 능력
    [SerializeField] private TMP_Text unique; //고유 능력

    void Start()
    {
        List<Character> list = BackendChart_JGD.chartData.character_list;

        for (int i = 0; i < list.Count; i++)
        {
            Make_prefab(list[i].sprite,i);
        }

        Setting();
    }

    private void OnEnable()
    {
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
        level.text = $"레벨 : {index_character.curlevel}";
        HP.text = $"HP : {100 + ((index_character.curlevel-1) * 10)}";
        special.text = index_character.special;
        unique.text = index_character.unique;
        Update_is_equip();
    }

    private void Update_is_equip()
    {
        if (index_character == cur_character)
        {
            is_equip.text = "장착 완료";
            is_equip_btn.interactable = false;
        }
        else
        {
            is_equip.text = "장착하기";
            is_equip_btn.interactable = true;
        }

    }

    private void Make_prefab(int sprite, int index)
    {
        GameObject newobj = Instantiate(btn_prefab);
        newobj.transform.SetParent(content_zone.transform, false);

        Button button = newobj.GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => Set_cur_character_btn(index));
        }

        newobj.transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.instance.Num2Sprite(sprite);
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
