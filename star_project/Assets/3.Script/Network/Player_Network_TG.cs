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

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!is_guest && TCP_Client_Manager.instance.now_room_id != -1) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            
            Debug.DrawRay(ray.origin, ray.direction.normalized * 2000f, Color.red);
            if (Input.GetMouseButtonUp(1))
            {
               // Debug.Log("RightClick!");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Interact_TG")))
                {
                    Vector3 dest = hit.point;
                    dest.y = transform.position.y;
                    dest.x -= 1f;
                    net_move(transform.position, dest);
                    hit.collider.gameObject.GetComponent<Net_Housing_Object>().interact();
                }
                else if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Ground_TG")| LayerMask.GetMask("Placement_YG")))
                {
                   // Debug.Log("check!");
                    Vector3 dest = hit.point;
                    dest.y = transform.position.y;
                    net_move(transform.position, dest);
                }
            }
        }
        
    }
}
