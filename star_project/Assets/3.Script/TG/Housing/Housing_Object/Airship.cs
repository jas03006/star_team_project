using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//비행선 (하우징 오브젝트)
//[현재는 사용하지 않음]
public class Airship : Net_Housing_Object
{
    private FriendList_JGD airship_UI;
    private string airship_UI_tag = "airship_UI";
    void Awake()
    {
        object_enum = housing_itemID.airship;
    }

    private void Start()
    {
        airship_UI = GameObject.FindGameObjectWithTag(airship_UI_tag).GetComponent<FriendList_JGD>();
    }

    public override void interact(string player_id, int interaction_id = 0, int param = 0)
    {
        base.interact(player_id, interaction_id, param);
        if (TCP_Client_Manager.instance.my_player.object_id == player_id)
        {
            Debug.Log("open airship!");
            open_airship_UI();
        }        
    }

    public void open_airship_UI() {
        if (airship_UI !=null && !airship_UI.transform.GetChild(0).gameObject.activeSelf) {
            airship_UI.transform.GetChild(0).gameObject.SetActive(true);
            airship_UI.ClearFriendList();
            airship_UI.GetFriendList(true);
        }
    }

  
}
