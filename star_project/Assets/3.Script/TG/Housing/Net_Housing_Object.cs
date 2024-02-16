using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class housing_object_data {
    int type;
}*/
public class Net_Housing_Object : Net_Object_TG
{
    //public housing_object_data data;
    public housing_itemID object_enum;
    public void init(string object_id_)
    {
        object_id = object_id_;
    }
    public void load_data() {
        object_id = Random.Range(1,1000).ToString();
    }
    public void request_interact(int interaction_id, int param)
    {// host_id object_id interaction_id param
        TCP_Client_Manager.instance.send_interact_request(object_id, interaction_id, param);
    }
    public virtual void interact(string player_id, int interaction_id =0, int param = 0) //상호작용 시도한 플레이어 닉네임, 인터액션 종류, 파라미터
    {
        Debug.Log($"{object_enum}: Interaction!");
    }
}
