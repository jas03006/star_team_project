using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Housing_UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject edit_button;
    [SerializeField] private GameObject housing_UI;
    [SerializeField] private MeshRenderer grid_renderer;
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
        }
        else
        {
            edit_button.SetActive(false);
        }
    }

    public void click_edit_btn() {
        edit_button.SetActive(false);
        housing_UI.SetActive(true);
        grid_renderer.enabled = true;
    }
    public void click_X_btn()
    {
        edit_button.SetActive(true);
        housing_UI.SetActive(false);
        grid_renderer.enabled = false;


    }
}
