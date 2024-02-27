using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Housing_Inven_BTN : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Button btn;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text usable_text;
    [SerializeField] private TMP_Text max_text;
    [SerializeField] private TMP_Text name_text;

    public housing_itemID id { private set; get; }
    public int usable_cnt { private set; get; }
    public int max_cnt { private set; get; }

    private string[] name_arr = {
        "none",
        "아크실린더",
        "비행선",
        "별둥지",
        "의자",
        "침대",
        "탁자",
        "우체통",
        "인형",
        "풍선",
        "블럭",
        "카메라",
        "비행기"
     };

    public void OnPointerDown(PointerEventData eventData)
    {
        if (can_use()) {
            TCP_Client_Manager.instance.placement_system.StartPlacement((int)id);
            AudioManager.instance.SFX_Click();
        }
    }

    public void init(housing_itemID id_, int use_cnt_, int max_cnt_) {
        id = id_;
        img.sprite = SpriteManager.instance.Num2Sprite(id);
        usable_cnt = use_cnt_;
        max_cnt = max_cnt_;
        name_text.text = name_arr[(int)id];
        update_UI();
    }

    public void use()
    {
        if (can_use()) {
            usable_cnt--;
            update_UI();
            
        }
    }

    public void back()
    {
        if (usable_cnt < max_cnt) {
            usable_cnt++;
            update_UI();
        }           
    }

    public void update_UI() {
        usable_text.text = usable_cnt.ToString();
        max_text.text = max_cnt.ToString();
        if (!can_use())
        {
            btn.interactable = false;
        }
        else {
            btn.interactable = true;
        }
    } 

    public bool can_use() {
        if (usable_cnt < 1) {
            return false;
        }
        return true;
    }
}
