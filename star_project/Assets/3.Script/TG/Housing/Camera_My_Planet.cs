using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_My_Planet : MonoBehaviour
{
    public float distance = 10f;
    public float min_distance = 5f;
    public float max_distance = 20f;

    public Vector3 angle = new Vector3(70f, 45f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!can_move_camera())
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                distance -= Input.mouseScrollDelta.y * 1.2f;
                if (distance < min_distance)
                {
                    distance = min_distance;
                }
                else if (distance > max_distance)
                {
                    distance = max_distance;
                }
            }
            transform.position = TCP_Client_Manager.instance.my_player.transform.position - distance * transform.forward;
        }
        else { 
            //TODO: 화면 드래그 시 화면 옮기기
        }        
    }

    private bool can_move_camera() {
        if (!TCP_Client_Manager.instance.housing_ui_manager.is_edit_mode)
        {
            return false;    
        }
        return true;
    }
}
