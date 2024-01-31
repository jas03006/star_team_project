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
        //������Ʈ�� ��ġ/������ ���, DB�� ������ ������ �� ����ȭ������ �˸���,����ȭ ������ �ٸ� Ŭ���̾�Ʈ�鿡�� �� ����� �˷���, Ŭ���̾�Ʈ���� ���� DB������ �޵����Ѵ�.
        //Ŭ���̾�Ʈ���� db������ ���� �޾� �����Ͱ� ���� �� �޶��� �κи� scene�� ������Ʈ���ִ� ������ �Ѵ�.
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
