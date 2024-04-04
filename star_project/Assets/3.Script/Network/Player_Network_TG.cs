using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Network_TG : Net_Move_Object_TG
{
    protected bool can_move() {
        if (TCP_Client_Manager.instance.housing_ui_manager != null && TCP_Client_Manager.instance.housing_ui_manager.is_edit_mode ) {
            return false;
        }
        return true;
    }
}
