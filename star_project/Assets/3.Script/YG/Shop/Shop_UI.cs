using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    #region ¡÷ºÆ
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

    [SerializeField] private Transform goods_container;
    [SerializeField] private GameObject goods_prefab;
    [SerializeField] private List<GameObject> goods = new List<GameObject>();


    float child_cnt = 0f;

    public void init_data(goods_cate cate)
    {
        foreach (var goods in BackendChart_JGD.chartData.Goods_list)
        {
               
        }
    }

    public void init_goods()
    {
        GameObject newobj = Instantiate(goods_prefab, goods_container);
        goods.Add(newobj);
    }
}
