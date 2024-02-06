using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class housing_object_data {
    int type;
}
public class Net_Housing_Object : Net_Object_TG
{
    public housing_object_data data;

    public void load_data() {
        object_id = Random.Range(1,1000);
    }
    public void request_interact(int interaction_id, int param)
    {// host_id object_id interaction_id param
        TCP_Client_Manager.instance.send_interact_request(object_id, interaction_id, param);
    }
    public virtual void interact()
    { 
        
    }
}
