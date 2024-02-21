using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Housing_UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject edit_button;
    [SerializeField] private GameObject housing_UI;
    [SerializeField] private MeshRenderer grid_renderer;

    [SerializeField] private Transform button_container;
    [SerializeField] private GameObject button_prefab;

    private Dictionary<housing_itemID, Housing_Inven_BTN> id2btn_dic;  

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

    public void init_housing_inventory()
    {
        id2btn_dic = new Dictionary<housing_itemID, Housing_Inven_BTN>();
        foreach (House_Item_Info_JGD item in BackendGameData_JGD.userData.house_inventory.item_list) {
            Debug.Log(item.id);
            //TODO: 인벤토리 버튼 추가
            GameObject go = Instantiate(button_prefab, button_container);
            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(
                delegate () { 
                    TCP_Client_Manager.instance.placement_system.StartPlacement((int)item.id);
                }
                );

            

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
        edit_button.SetActive(false);
        housing_UI.SetActive(true);
        grid_renderer.enabled = true;

        camera.cullingMask = edit_mode_mask;
    }
    public void click_X_btn()
    {
        is_edit_mode = false;
        edit_button.SetActive(true);
        housing_UI.SetActive(false);
        grid_renderer.enabled = false;

        camera.cullingMask = default_mask;
    }
}
