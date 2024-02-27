using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Move_Object_TG : Net_Object_TG
{
    
    private Coroutine now_move_co = null;
    [SerializeField] protected bool is_guest = false;
    [SerializeField] protected Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (!is_guest) {
            object_id = UnityEngine.Random.Range(0, 1000).ToString();
        }        
    }


    // Update is called once per frame
    

    public void init(string object_id_, bool is_guest_ = false)
    {
        object_id = object_id_;
        is_guest = is_guest_;
        load();
    }

    public virtual void load()
    {
        //TODO: object_id(uuid)를 이용하여 뒤끝베이스에서 유저 정보(닉네임, 외형 정보 등)를 받아와 저장하고 외형을 변경
        
    }

    public virtual void hide_UI() {
        if (canvas != null) {
            canvas.gameObject.SetActive(false);
        }
    }
    public virtual void show_UI()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
    }
    public void net_move(Vector3 start_pos, Vector3 dest_pos) {
        TCP_Client_Manager.instance.send_move_request(object_id, start_pos, dest_pos);
    }

    public virtual void move(Vector3 start_pos, Vector3 dest_pos, TweenCallback callback = null)
    {
        if (now_move_co != null) {
            StopCoroutine(now_move_co);
            now_move_co = null;
        }       
        now_move_co = StartCoroutine(move_co( start_pos,  dest_pos));
    }

    public IEnumerator move_co(Vector3 start_pos, Vector3 dest_pos)
    {
        //Debug.Log("start co!");
        start_pos.y = transform.position.y;
        dest_pos.y = transform.position.y;
        transform.position = start_pos;
        
        //float timer = 0;
        Vector3 dir = (dest_pos - transform.position).normalized;

        while ((dest_pos - transform.position).sqrMagnitude > 0.05f) {
            transform.position += dir * 10f * Time.deltaTime;
            yield return null;
        }
        transform.position = dest_pos;
    }


}
