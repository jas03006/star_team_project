using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    #region 주석
    //[SerializeField] private int btn_per_line = 6;
    //[SerializeField] private int page_num = 0;
    //[SerializeField] private int total_page_num = 2;

    //[SerializeField] private Sprite un_select_tab_sprite;
    //[SerializeField] private Sprite select_tab_sprite;

    //[SerializeField] private TMP_Text[] cate_text_arr;

    //[SerializeField] private Scrollbar scrollbar;
    //[SerializeField] private Button[] cate_btn_arr;

    //[SerializeField] private TMP_Text page_text;
    //[SerializeField] private TMP_Text total_page_text;

    //[SerializeField] private Color un_select_tab_text_color;
    //[SerializeField] private Color select_tab_text_color;

    #endregion

    [SerializeField] private goods_cate cur_cate;

    private List<Goods> recommand_list = new List<Goods>();
    private List<Goods> housing_list = new List<Goods>();
    private List<Goods> imozi_list = new List<Goods>();
    private List<Goods> ruby_list = new List<Goods>();
    private List<Goods> etc_list = new List<Goods>();

    [SerializeField] private List<Image> white_border = new List<Image>();
    [SerializeField] private int[] recomands_index = { 0, 1, 4, 5, 6 };
    [SerializeField] private Transform goods_container;
    [SerializeField] private GameObject goods_prefab;

    [SerializeField] private int init_num = 6;//프리팹 생성 갯수
    [SerializeField] private List<GameObject> goods_prefabs = new List<GameObject>();
    [SerializeField] private List<Goods_UI> goods_ui_list = new List<Goods_UI>();

    [SerializeField] private Shop_Popup popup;
    [SerializeField] TMP_Text gold;
    [SerializeField] TMP_Text ark;
    [SerializeField] TMP_Text ruby;

    [SerializeField] Color select_color;
    [SerializeField] Color nonselect_color;

    private void Start()
    {
        Setting();
        Money_UpdateUI();
        change_cate(0);
    }

    public void Setting()
    {
        Debug.Log("세팅 시작");
        Debug.Log(BackendChart_JGD.chartData.Goods_list.Count);
        foreach (var goods in BackendChart_JGD.chartData.Goods_list)
        {
            switch (goods.cate)
            {
                case goods_cate.housing:
                    housing_list.Add(goods);
                    break;
                case goods_cate.imozi:
                    imozi_list.Add(goods);
                    break;
                case goods_cate.ruby:
                    ruby_list.Add(goods);
                    break;
                case goods_cate.etc:
                    etc_list.Add(goods);
                    break;
            }

            if (recomands_index.Contains(goods.index))
            {
                recommand_list.Add(goods);
            }
        }
        Debug.Log($"세팅완료. 현재카운트 housing{housing_list.Count}/imozi{imozi_list.Count} ");

        for (int i = 0; i < init_num; i++)
        {
            GameObject newobj = Instantiate(goods_prefab, goods_container);
            goods_prefabs.Add(newobj);
            goods_ui_list.Add(newobj.GetComponent<Goods_UI>());
        }
    }

    public void change_cate(int num) //index 변경
    {
        cur_cate = (goods_cate)num;

        for (int i = 0; i < white_border.Count; i++)
        {
            white_border[i].color = num == i? select_color: nonselect_color;
        }

        prefab_OnOff();
    }

    public void prefab_OnOff()
    {
        List<Goods> cur_list = Get_list();
        for (int i = 0; i < init_num; i++)
        {
            goods_prefabs[i].SetActive(i < cur_list.Count);

            if (i < cur_list.Count)
            {
                goods_prefabs[i].TryGetComponent(out Goods_UI ui);
                ui.goods = cur_list[i];
            }
        }
    }

    public List<Goods> Get_list()
    {
        switch (cur_cate)
        {
            case goods_cate.recommand:
                return recommand_list;
            case goods_cate.housing:
                return housing_list;
            case goods_cate.imozi:
                return imozi_list;
            case goods_cate.ruby:
                return ruby_list;
            case goods_cate.etc:
                return etc_list;
            default:
                Debug.Log("찾는 리스트없음");
                return null;
        }
    }

    public void Money_UpdateUI()
    {
        gold.text = MoneyManager.instance.gold.ToString();
        ark.text = MoneyManager.instance.ark.ToString();
        ruby.text = MoneyManager.instance.ruby.ToString();
    }

    public void All_updateUI()
    {
        foreach (var goods_UI in goods_ui_list) 
        {
            goods_UI.UpdateUI();
        }
    }

    public void Purchase()
    {
        popup.purchase();
        Money_UpdateUI();
        All_updateUI();
    }
}
