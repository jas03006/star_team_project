using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

//�Ͽ�¡ ���� ��� �ϴ� �κ��丮�� ��� ��ư
//��ư �巡�׸� ���� ��ġ ����
public class Housing_Inven_BTN : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Button btn;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text usable_text;
    [SerializeField] private TMP_Text max_text;
    [SerializeField] private TMP_Text name_text;
    [SerializeField] private ObjectsDatabaseSO db;
    public housing_itemID id { private set; get; }
    public int usable_cnt { private set; get; }
    public int max_cnt { private set; get; }

    //�巡�׸� ���� ��ġ ����
    public void OnPointerDown(PointerEventData eventData)
    {
        if (can_use()) {
            TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
            TCP_Client_Manager.instance.placement_system.StartPlacement((int)id);            
            AudioManager.instance.SFX_Click();

            if (Tutorial_TG.instance.get_type() ==tutorial_type_TG.housing_inven_touch) {
                Tutorial_TG.instance.check_housing_inven_touch(id);
            }
        }
    }

    public void init(housing_itemID id_, int use_cnt_, int max_cnt_) {
        id = id_;
        img.sprite = SpriteManager.instance.Num2Sprite(id);
        usable_cnt = use_cnt_;
        max_cnt = max_cnt_;
        
        name_text.text = db.get_object(id_).name;
        update_UI();
    }

    //������ ���� ���� ����
    //��ġ ���� ���η� ����
    //��ġ ���� ���ΰ� ���ٸ� id ������ ����
    public int get_sort_score() {
        return (usable_cnt > 0? 10000: 0) + (1000 - (int)id);
    }

    //��ġ �� Ƚ�� ����
    public void use()
    {
        if (can_use()) {
            usable_cnt--;
            update_UI();
        }
    }

    //ȸ��
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
