using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//우체통 (하우징 오브젝트)
public class Post_Box : Net_Housing_Object
{
    private Post_Box_UI post_box_UI;
    private string post_box_UI_tag = "post_box_UI";
    void Awake()
    {
        object_enum = housing_itemID.post_box;
    }

    private void Start()
    {
        post_box_UI = GameObject.FindGameObjectWithTag(post_box_UI_tag).GetComponent<Post_Box_UI>();
    }

    public override void interact(string player_id, int interaction_id = 0, int param = 0)
    {
        base.interact(player_id, interaction_id, param);
        if (TCP_Client_Manager.instance.my_player.object_id == TCP_Client_Manager.instance.now_room_id)
        {
            Debug.Log("open post_box!");
            open_post_box_UI();
        }
    }

    public void open_post_box_UI()
    {
        if (post_box_UI != null)
        {
            post_box_UI.show_UI();
          //  post_box_UI.GetFriendList();
        }
    }


}
