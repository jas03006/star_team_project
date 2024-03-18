using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goods_UI : MonoBehaviour
{
    public Goods goods;
    [SerializeField]private Image goods_img;
    [SerializeField] private Image nolimit;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private TMP_Text value_text;
    [SerializeField] private Image money;

    public void updateUI()
    {
        switch(goods.cate)
        {
            case goods_cate.housing:
                goods_img.sprite = SpriteManager.instance.Num2Sprite(goods.housing_id);
                break;
            case goods_cate.imozi:
                goods_img.sprite = SpriteManager.instance.Num2emozi(goods.imozi_id);
                break;
        }

        nolimit.enabled = goods.can_repurchase ? true: false;
        name_text.text = goods.name;
        value_text.text = goods.value.ToString();
        //스프라이트매니저에 money 추가 후 등록
    }
}
