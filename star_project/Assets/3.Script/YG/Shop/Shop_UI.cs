using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    [SerializeField] private int btn_per_line = 6;
    [SerializeField] private int page_num = 0;
    [SerializeField] private int total_page_num = 2;

    [SerializeField] private GameObject goods_prefab;

    [SerializeField] private Sprite un_select_tab_sprite;
    [SerializeField] private Sprite select_tab_sprite;

    [SerializeField] private TMP_Text[] cate_text_arr;

    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Button[] cate_btn_arr;

    [SerializeField] private TMP_Text page_text;
    [SerializeField] private TMP_Text total_page_text;

    [SerializeField] private Color un_select_tab_text_color;
    [SerializeField] private Color select_tab_text_color;

    [SerializeField] private Transform goods_container;

    float child_cnt = 0f;

    public void init_goods()
    {
        Instantiate(goods_prefab, goods_container);

        //count check
        child_cnt = goods_container.childCount;
        total_page_num = (int)(child_cnt-1) / btn_per_line +1;
        total_page_text.text = total_page_num.ToString();
    }

    public void click_scroll_btn(bool is_right)
    {
        if (child_cnt <= btn_per_line)
        {
            return;
        }

        if (is_right)
        {
            scrollbar.value -= 1f / (float)(total_page_num - 1);
            scrollbar.value = Mathf.Max(scrollbar.value, 0f);
            page_num = Mathf.Min(page_num + 1, total_page_num);
        }

        else
        {
            scrollbar.value += 1f / (float)(total_page_num - 1);
            scrollbar.value = Mathf.Min(scrollbar.value, 1f);
            page_num = Mathf.Max(page_num - 1, 1);
        }
        page_text.text = page_num.ToString();
    }

    public void click_category_btn(int cate)
    {
        for (int i = 0; i < cate_btn_arr.Length; i++)
        {
            cate_btn_arr[i].image.sprite = un_select_tab_sprite;
            cate_text_arr[i].color = un_select_tab_text_color;
        }
        cate_btn_arr[cate].image.sprite = select_tab_sprite;
        cate_text_arr[cate].color = select_tab_text_color;

        child_cnt = 0;
        for (int i = 0; i < goods_container.childCount; i++)
        {
            if (cate == 0)
            {
                goods_container.GetChild(i).gameObject.SetActive(true);
                child_cnt++;
                continue;
            }
            if (cate == get_category(goods_container.GetChild(i).GetComponent<Housing_Inven_BTN>().id))
            {
                goods_container.GetChild(i).gameObject.SetActive(true);
                child_cnt++;
            }
            else
            {
                goods_container.GetChild(i).gameObject.SetActive(false);
            }
        }
        total_page_num = (int)(child_cnt - 1) / btn_per_line + 1;
        total_page_text.text = total_page_num.ToString();
        page_num = 1;
        page_text.text = "1";
        // scrollbar.value = 1f;
        StartCoroutine(init_scroll_co());
    }

    public int get_category(housing_itemID id_)
    {
        housing_itemID[] special_arr = { housing_itemID.ark_cylinder, housing_itemID.star_nest, housing_itemID.airship, housing_itemID.post_box };
        if (special_arr.Contains(id_))
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public IEnumerator init_scroll_co()
    {
        yield return null;
        scrollbar.value = 1f;
        Canvas.ForceUpdateCanvases();

    }
}
