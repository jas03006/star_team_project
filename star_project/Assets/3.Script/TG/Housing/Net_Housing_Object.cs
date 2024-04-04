using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public class housing_object_data {
    int type;
}*/

// 하우징이 가능한 Net_Object의 부모클래스
public class Net_Housing_Object : Net_Object_TG
{
    //public housing_object_data data;
    public housing_itemID object_enum;
    public void init(string object_id_)
    {
        object_id = object_id_;
    }

    //현재는 사용하지않음
    public void load_data() {
        object_id = Random.Range(1,1000).ToString();
    }

    //서버에 상호작용 요청
    public void request_interact(int interaction_id, int param)
    {// host_id object_id interaction_id param
        TCP_Client_Manager.instance.send_interact_request(object_id, interaction_id, param);
    }

    //플레이어와 상호작용
    public virtual void interact(string player_id, int interaction_id =0, int param = 0) //상호작용 시도한 플레이어 닉네임, 인터액션 종류, 파라미터
    {
        Debug.Log($"{object_enum}: Interaction!");
        if (Tutorial_TG.instance.get_type() == tutorial_type_TG.housing_object_touch) {
            Tutorial_TG.instance.check_housing_object_touch(object_enum);
        }
    }
}
