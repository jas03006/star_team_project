using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObj : Net_Housing_Object
{
    private Coroutine now_interact_co = null;
    public override void interact(string player_id, int interaction_id = 0, int param = 0) //상호작용 시도한 플레이어 닉네임, 인터액션 종류, 파라미터
    {
        base.interact(player_id, interaction_id, param);
        if (now_interact_co == null) {
            now_interact_co = StartCoroutine(interact_co());
            if (param ==0) {
                TCP_Client_Manager.instance.send_interact_request(((int)transform.position.x).ToString() + ":" + ((int)transform.position.z).ToString(), 0, 1);
            }            
        }
        
    }

    public IEnumerator interact_co() {
        float timer;
        Vector3 origin_pos = transform.position;
        float limit = 1f;
        rotate_object ro = gameObject.GetComponentInChildren<rotate_object>();
        if (ro != null)
        {
            ro.hyper_spin_on();
        }
        for (int i =0; i < 3; i++) {
            timer = 0;
            while (timer < limit)
            {
                transform.position += Vector3.up * Time.deltaTime * (timer < limit/2 ? 1f:-1f);                
                yield return null;
                timer += Time.deltaTime;
            }
        }
        if (ro != null)
        {
            ro.hyper_spin_off();
        }
        transform.position = origin_pos;
        now_interact_co = null;
    }
}