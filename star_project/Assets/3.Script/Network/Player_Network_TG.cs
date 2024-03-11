using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Network_TG : Net_Move_Object_TG
{

   // [SerializeField] private PlayerMovement myplayer_move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    protected bool can_move() {
        if (TCP_Client_Manager.instance.housing_ui_manager.is_edit_mode ) {
            return false;
        }
        return true;
    }
}
