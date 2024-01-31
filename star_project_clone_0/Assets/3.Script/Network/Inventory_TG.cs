using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_TG : MonoBehaviour
{
    private GameObject now_object = null;

    [SerializeField] private GameObject test_prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (now_object != null) {
            if (Input.GetMouseButtonDown(1))
            {
                hide_preview();
            }else if (Input.GetMouseButtonDown(0))
            {
                set_object();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Debug.DrawRay(ray.origin, ray.direction.normalized * 2000f, Color.red);
                if (Physics.Raycast(ray, out RaycastHit hit, 2000f, LayerMask.GetMask("Ground_TG")))
                {
                    Vector3 dest = hit.point;
                    dest.y = 1;
                    now_object.transform.position = dest;
                }
            }         
        }        
    }

    public void show_object_preview() {
        if (now_object != null) {
            Destroy(now_object);
        }
        now_object = Instantiate(test_prefab);
    }

    public void set_object() {
        now_object = null;
        TCP_Client_Manager.instance.send_update_request();
        //오브젝트를 설치/제거한 경우, DB에 정보를 저장한 후 동기화서버에 알리고,동기화 서버는 다른 클라이언트들에게 이 사실을 알려서, 클라이언트들이 새로 DB정보를 받도록한다.
        //클라이언트들은 db정보를 새로 받아 기존것과 비교한 후 달라진 부분만 scene을 업데이트해주는 것으로 한다.
    }
    public void hide_preview()
    {
        if (now_object != null)
        {
            Destroy(now_object);
            now_object = null;
        }
    }

}
