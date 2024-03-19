using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Popup : MonoBehaviour
{
    public Goods goods
    {
        get { return goods_; }
        set
        {
            goods_ = value;
            UpdateUI();
        }
    }
    private Goods goods_;
    private int select_num
    {
        get { return select_num_; }
        set
        {
            if (select_num_ < 1)
            {
                select_num_ = 1;
            }
            select_num_ = value;
            UpdateUI_num();
        }
    }
    private int select_num_;
    [SerializeField] Image goods_img;
    [SerializeField] TMP_Text name_text;
    [SerializeField] TMP_Text num_text;
    [SerializeField] TMP_Text value_text;
    [SerializeField] TMP_Text mymoney_text;
    [SerializeField] List<Image> money_img;
    [SerializeField] List<GameObject> plus_minus;
    [SerializeField] Button btn;
    [SerializeField] Image btn_img;
    [SerializeField] Sprite can_pur;
    [SerializeField] Sprite cant_pur;
    [SerializeField] Color can_color;
    [SerializeField] Color cant_color;

    private void UpdateUI()
    {
        switch (goods.cate)
        {
            case goods_cate.housing:
                goods_img.sprite = SpriteManager.instance.Num2Sprite(goods.housing_id);
                break;
            case goods_cate.imozi:
                goods_img.sprite = SpriteManager.instance.Num2emozi(goods.imozi_id);
                break;
        }
        foreach (GameObject go in plus_minus)
        {
            go.SetActive(goods.have_num);
        }
        num_text.text = select_num.ToString();
        mymoney_text.text = MoneyManager.instance.Check_Money(goods.money).ToString();
        UpdateUI_num();
        name_text.text = goods.goods_name;
        foreach (var img in money_img)
        {
            img.sprite = SpriteManager.instance.Num2Sprite(goods.money);
        }
        select_num_ = 1;
        bool have_money = MoneyManager.instance.Check_Money(goods.money) >= goods.value;
        btn_img.sprite = have_money ? can_pur : cant_pur;
        btn.interactable = have_money;
        value_text.color = have_money ? can_color : cant_color;
    }

    private void UpdateUI_num()
    {
        value_text.text = (goods.value * select_num).ToString();
        num_text.text = select_num.ToString();
    }


    public void UpdateGoods(Goods goods)
    {
        this.goods = goods;
        UpdateUI(); // UI 업데이트
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Setting_num(bool is_plus)
    {
        int tmp = is_plus ? 1 : -1;
        select_num += tmp;

        UpdateUI_num();
    }

    public void purchase()
    {
        if (goods.value >= MoneyManager.instance.Check_Money(goods.money))
        {
            Debug.Log("가진돈 없어서 return");
            return;
        }
        Debug.Log("구매 가능함");
        QuestManager.instance.Check_challenge(Clear_type.buy);
        MoneyManager.instance.Spend_Money(goods.money,goods.value * select_num);
        switch (goods.cate)
        {
            case goods_cate.housing:
                BackendGameData_JGD.userData.house_inventory.Add(goods.housing_id, select_num);
                break;
            case goods_cate.imozi:
                BackendGameData_JGD.userData.emozi_List.Add(goods.imozi_id);
                break;
            default:
                Debug.Log("상품 카테고리 이상함");
                break;
        }
        if (!goods.can_repurchase)
        {
            BackendGameData_JGD.userData.shop_info.index_list.Add(goods.index);
        }
        BackendGameData_JGD.Instance.GameDataUpdate();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
