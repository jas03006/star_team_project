using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net_Move_Object_TG : Net_Object_TG
{
    [SerializeField] public int object_id { get; private set; } // 오브젝트의 인스턴스를 나타낼 고유의 id (플레이어 캐릭터의 경우 uuid로 하면 될 듯 하다)
    private Coroutine now_move_co = null;
    [SerializeField] protected bool is_guest = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!is_guest) {
            object_id = UnityEngine.Random.Range(0, 1000);
        }        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(int object_id_, bool is_guest_ = false)
    {
        object_id = object_id_;
        is_guest = is_guest_;
    }

    public void net_move(Vector3 start_pos, Vector3 dest_pos) {
        TCP_Client_Manager.instance.send_move_request(object_id, start_pos, dest_pos);
    }

    public void move(Vector3 start_pos, Vector3 dest_pos)
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
