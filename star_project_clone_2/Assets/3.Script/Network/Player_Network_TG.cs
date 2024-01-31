using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Network_TG : Net_Move_Object_TG
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_guest) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            Debug.DrawRay(ray.origin, ray.direction.normalized * 2000f, Color.red);
            if (Input.GetMouseButtonUp(1))
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 2000f, LayerMask.GetMask("Ground_TG")))
                {
                    Vector3 dest = hit.point;
                    dest.y = transform.position.y;
                    net_move(transform.position, dest);
                }
            }
        }
        
    }
}
