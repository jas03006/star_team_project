using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goods_UI : MonoBehaviour
{
    public Goods goods//상품 데이터
    {
        get { return goods_; }
        set
        {
            goods_ = value;
            UpdateUI();
        }
    }
    private Goods goods_;

    [SerializeField] private Image goods_img;
    [SerializeField] private TMP_Text nolimit;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text value_text;
    [SerializeField] private Image money;

    [SerializeField] private Button btn;
    [SerializeField] private Image btn_img;

    [SerializeField] private Sprite can_pur;
    [SerializeField] private Sprite cant_pur;

    [SerializeField] private Shop_Popup popup = null;

    public void UpdateUI()
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

        nolimit.enabled = goods.can_repurchase ? false : true;
        name_text.text = goods.goods_name;
        value_text.text = goods.value.ToString();
        money.sprite = SpriteManager.instance.Num2Sprite(goods.money);

        bool is_contain = !BackendGameData_JGD.userData.shop_info.index_list.Contains(goods.index);
        money.enabled = is_contain;
        btn_img.sprite = is_contain ? can_pur : cant_pur;
        btn.interactable = is_contain;
        value_text.enabled = is_contain;
    }

    public void Spend_goods()
    {
        if (popup == null)
        {
            popup = FindObjectOfType<Shop_Popup>();
        }
        popup.UpdateGoods(goods);
    }
}
