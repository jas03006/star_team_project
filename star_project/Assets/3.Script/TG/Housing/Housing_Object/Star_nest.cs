using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star_nest : Net_Housing_Object
{
    private Star_nest_UI star_nest_UI;
    private string star_nest_UI_tag = "star_nest_UI";
    void Awake()
    {
        object_enum = housing_itemID.star_nest;
    }

    private void Start()
    {
        star_nest_UI = GameObject.FindGameObjectWithTag(star_nest_UI_tag).GetComponent<Star_nest_UI>();
    }

    public override void interact(string player_id, int interaction_id = 0, int param = 0)
    {
        base.interact(player_id, interaction_id, param);
        if (TCP_Client_Manager.instance.my_player.object_id == player_id)
        {
            Debug.Log("open star nest!");
            open_star_nest_UI();
        }
    }

    public void open_star_nest_UI()
    {
        if (star_nest_UI != null && !star_nest_UI.transform.GetChild(0).gameObject.activeSelf)
        {
            star_nest_UI.show_UI(TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id);
            //star_nest_UI.ClearFriendList();
            //star_nest_UI.GetFriendList();
        }
    }
}
