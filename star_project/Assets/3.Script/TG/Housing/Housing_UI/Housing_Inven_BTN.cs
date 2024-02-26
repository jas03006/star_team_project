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

    public housing_itemID id { private set; get; }
    public int usable_cnt { private set; get; }
    public int max_cnt { private set; get; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (can_use()) {
            TCP_Client_Manager.instance.placement_system.StartPlacement((int)id);
        }        
    }



    public void init(housing_itemID id_, int use_cnt_, int max_cnt_) {
        id = id_;
        img.sprite = SpriteManager.instance.Num2Sprite(id);
        usable_cnt = use_cnt_;
        max_cnt = max_cnt_;
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
