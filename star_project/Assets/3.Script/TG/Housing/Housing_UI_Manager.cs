using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] private Button[] cate_btn_arr;
    [SerializeField] private TMP_Text[] cate_text_arr;
    [SerializeField] private Color un_select_tab_text_color;
    [SerializeField] private Color select_tab_text_color;
    [SerializeField] private Sprite un_select_tab_sprite;
    [SerializeField] private Sprite select_tab_sprite;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TMP_Text page_text;
    [SerializeField] private TMP_Text total_page_text;
    [SerializeField] private int btn_per_line = 6;
    private int page_num=0;
    private int total_page_num=1;

    private Dictionary<housing_itemID, Housing_Inven_BTN> id2btn_dic = new Dictionary<housing_itemID, Housing_Inven_BTN>();

    public Camera camera;
    public LayerMask default_mask;
    public LayerMask edit_mode_mask;

    public bool is_edit_mode = false;
    float child_cnt = 1f;
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
        if (is_first_time)
        {
            id2btn_dic = new Dictionary<housing_itemID, Housing_Inven_BTN>();
            child_cnt = 0;
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
            child_cnt += 1;
            GameObject go = Instantiate(button_prefab, button_container);
            Button btn = go.GetComponent<Button>();

            Housing_Inven_BTN hiBTN = go.GetComponent<Housing_Inven_BTN>();

            hiBTN.init(item.id, item.count, item.count);
            id2btn_dic[item.id] = hiBTN;
        }
        /*Dictionary<Vector3Int, PlacementData> po = TCP_Client_Manager.instance.placement_system.furnitureData.placedObjects;
        foreach (var key in po.Keys) {
            PlacementData pd = po[key];
            if (id2btn_dic.ContainsKey(pd.ID)) {
                id2btn_dic[pd.ID].use();
            }
        }*/
        List<GameObject> go_list = TCP_Client_Manager.instance.placement_system.objectPlacer.placedGameObject;
        for (int i =0; i < go_list.Count; i++)
        {
            if (go_list[i] ==null) {
                continue;
            }
            Net_Housing_Object go = go_list[i].GetComponentInChildren<Net_Housing_Object>();
            if (id2btn_dic.ContainsKey(go.object_enum))
            {
                id2btn_dic[go.object_enum].use();
            }
        }
        
        click_category_btn(0);
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

        foreach (var key in TCP_Client_Manager.instance.net_mov_obj_dict.Keys)
        {
            TCP_Client_Manager.instance.net_mov_obj_dict[key].hide_UI();
        }

        init_housing_inventory(false);
    }
    public void click_X_btn(bool is_cancel = true)
    {
        TCP_Client_Manager.instance.placement_system.cancel_placement();
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

        if (is_cancel) {
            TCP_Client_Manager.instance.placement_system.update_placement();
            init_housing_inventory(false);
        }

        foreach (var key in TCP_Client_Manager.instance.net_mov_obj_dict.Keys)
        {
            TCP_Client_Manager.instance.net_mov_obj_dict[key].show_UI();
        }
    }

    public void click_scroll_btn(bool is_right) {
        if (child_cnt <= btn_per_line) {
            return;
        }
        if (is_right)
        {
            scrollbar.value -= 1f/ (float)(total_page_num-1);
            scrollbar.value = Mathf.Max(scrollbar.value, 0f);            
            page_num = Mathf.Min(page_num+1, total_page_num);
        }
        else {
            scrollbar.value += 1f / (float)(total_page_num-1);
            scrollbar.value = Mathf.Min(scrollbar.value, 1f);
            page_num = Mathf.Max(page_num - 1, 1);
        }
        page_text.text = page_num.ToString();
    }
    
    public void click_category_btn(int cate) {
        for (int i=0; i < cate_btn_arr.Length;i++) {
            cate_btn_arr[i].image.sprite = un_select_tab_sprite;
            cate_text_arr[i].color = un_select_tab_text_color;
        }
        cate_btn_arr[cate].image.sprite = select_tab_sprite;
        cate_text_arr[cate].color = select_tab_text_color;

        child_cnt = 0;
        for (int i = 0; i < button_container.childCount; i++) {
            if (cate == 0) {
                button_container.GetChild(i).gameObject.SetActive(true);
                child_cnt++;
                continue;
            }
            if (cate == get_category( button_container.GetChild(i).GetComponent<Housing_Inven_BTN>().id)) {
                button_container.GetChild(i).gameObject.SetActive(true);
                child_cnt++;
            }
            else {
                button_container.GetChild(i).gameObject.SetActive(false);
            }
        }
        total_page_num = (int)(child_cnt - 1) / btn_per_line + 1;
        total_page_text.text = total_page_num.ToString();
        page_num = 1;
        page_text.text = "1";
       // scrollbar.value = 1f;
        StartCoroutine(init_scroll_co());
    }

    public IEnumerator init_scroll_co() {
        yield return null;
        scrollbar.value = 1f;
        Canvas.ForceUpdateCanvases();
      
    }
    public int get_category(housing_itemID id_) {
        housing_itemID[] special_arr = { housing_itemID.ark_cylinder, housing_itemID.star_nest, housing_itemID.airship, housing_itemID .post_box};
        if (special_arr.Contains(id_)) {
            return 1;
        }
        else {
            return 2;
        }
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
            edit_UI.position =  Camera.main.WorldToScreenPoint(now_focus_ob.gameObject.transform.position) - Vector3.up*(12f+Camera.main.orthographicSize*1.8f);
            yield return null;         
        }
    }
    #endregion



}
