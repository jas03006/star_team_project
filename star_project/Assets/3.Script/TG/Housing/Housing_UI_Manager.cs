using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Housing_UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject edit_button;
    [SerializeField] private GameObject housing_UI;
    [SerializeField] private MeshRenderer grid_renderer;

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
        housing_UI.SetActive(false);
        if (TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id)
        {
            edit_button.SetActive(true);
            init_housing_inventory();
        }
        else
        {
            edit_button.SetActive(false);
        }
    }

    public void init_housing_inventory()
    {
        foreach (House_Item_Info_JGD item in BackendGameData_JGD.userData.house_inventory.item_list) { 
            //TODO: 인벤토리 버튼 추가
        }
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
