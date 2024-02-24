using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Housing_UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject edit_button;
    [SerializeField] private List<GameObject> hide_UI_list;
    [SerializeField] private GameObject housing_UI;
    [SerializeField] private MeshRenderer grid_renderer;

    [SerializeField] private Transform button_container;
    [SerializeField] private GameObject button_prefab;

    private Dictionary<housing_itemID, Housing_Inven_BTN> id2btn_dic = new Dictionary<housing_itemID, Housing_Inven_BTN>();

    public Camera camera;
    public LayerMask default_mask;
    public LayerMask edit_mode_mask;

    public bool is_edit_mode = false;
    
    // Start is called before the first frame update
    void Start()
    {
       // init_housing_UI();

    }

    

    public void init_housing_UI() {
        grid_renderer.enabled = false;        
        if (TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id)
        {
            edit_button.SetActive(true);
            init_housing_inventory();
        }
        else
        {
            edit_button.SetActive(false);
        }
        housing_UI.SetActive(false);
    }

    public void init_housing_inventory(bool is_first_time =true)
    {
        if (is_first_time) {
            id2btn_dic = new Dictionary<housing_itemID, Housing_Inven_BTN>();
        }
        
        foreach (House_Item_Info_JGD item in BackendGameData_JGD.userData.house_inventory.item_list) {
            if (id2btn_dic.ContainsKey(item.id))
            {
                if (is_first_time)
                {
                    Housing_Inven_BTN hiBTN_ = id2btn_dic[item.id];
                    hiBTN_.init(item.id, hiBTN_.usable_cnt + item.count, hiBTN_.max_cnt+item.count);
                }
                else {
                    id2btn_dic[item.id].init(item.id, item.count, item.count);
                }
                continue;
            }
            GameObject go = Instantiate(button_prefab, button_container);
            Button btn = go.GetComponent<Button>();

            Housing_Inven_BTN hiBTN = go.GetComponent<Housing_Inven_BTN>();

            hiBTN.init(item.id, item.count, item.count);
            id2btn_dic[item.id] = hiBTN;
        }
        Dictionary<Vector3Int, PlacementData> po = TCP_Client_Manager.instance.placement_system.furnitureData.placedObjects;
        foreach (var key in po.Keys) {
            PlacementData pd = po[key];
            if (id2btn_dic.ContainsKey(pd.ID)) {
                id2btn_dic[pd.ID].use();
            }
        }
    }


    public void decrease_use_count(housing_itemID id) {
        id2btn_dic[id].use();
    }
    public void increase_use_count(housing_itemID id)
    {
        id2btn_dic[id].back();
    }
    public bool can_use(housing_itemID id) { 
        return id2btn_dic[id].can_use(); ;
    }

    public void click_edit_btn() {
        is_edit_mode = true;
        foreach (GameObject ui in hide_UI_list) {
            ui.SetActive(false);
        }

        edit_button.SetActive(false);
        housing_UI.SetActive(true);
        grid_renderer.enabled = true;

        camera.cullingMask = edit_mode_mask;
    }
    public void click_X_btn()
    {
        is_edit_mode = false;
        foreach (GameObject ui in hide_UI_list)
        {
            ui.SetActive(true);
        }

        hide_edit_UI();
        edit_button.SetActive(true);
        housing_UI.SetActive(false);
        grid_renderer.enabled = false;

        camera.cullingMask = default_mask;
        TCP_Client_Manager.instance.placement_system.update_placement();
        init_housing_inventory(false);
    }


    #region object edit mode
    public Net_Housing_Object now_focus_ob = null;
    public RectTransform edit_UI;
    public Action delete_action;
    private Coroutine edit_show_co=null;
    public void show_edit_UI(Net_Housing_Object ob) {
        edit_UI.gameObject.SetActive(true);
        now_focus_ob = ob;
        //TCP_Client_Manager.instance.placement_system.StartPlacement((int)ob.object_enum);
        delete_action = delegate(){ TCP_Client_Manager.instance.placement_system.remove(ob);  };

        if (edit_show_co!=null) {
            StopCoroutine(edit_show_co);
        }
        edit_show_co = StartCoroutine(show_edit_UI_co());
    }
    public void hide_edit_UI() {
        if (edit_show_co != null)
        {
            StopCoroutine(edit_show_co);
            edit_show_co = null;
        }
        now_focus_ob = null;        
        delete_action = null;
        edit_UI.gameObject.SetActive(false);
    }
    public void click_delete_btn() {
        delete_action?.Invoke();
        hide_edit_UI();
    }

    public IEnumerator show_edit_UI_co() { 
        while(now_focus_ob != null) { 
            edit_UI.position =  Camera.main.WorldToScreenPoint(now_focus_ob.gameObject.transform.position) - Vector3.up*50f;
            yield return null;         
        }
    }
    #endregion
}
