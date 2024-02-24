using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera scene_cam;
    private Vector3 last_postion;
    [SerializeField] private LayerMask placement_layermask;

    public event Action Onclicked, OnExit;

    private bool old_btn_up = false;
    private float select_timer = 0f;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool now_btn_up = Input.GetMouseButtonUp(0);

        if (TCP_Client_Manager.instance.housing_ui_manager.is_edit_mode) {
            if (now_btn_up && !IsPointerOverUI())
            {
                if (Onclicked != null)
                {
                    Onclicked?.Invoke();
                    OnExit?.Invoke();
                }
                else {
                    if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Interact_TG")))
                    {
                        TCP_Client_Manager.instance.housing_ui_manager.show_edit_UI(hit.collider.gameObject.GetComponentInParent<Net_Housing_Object>());
                    }
                    else {
                        TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
                    }
                }
            }    

            if (Input.GetMouseButton(0) && TCP_Client_Manager.instance.housing_ui_manager.now_focus_ob != null && !IsPointerOverUI())
            {
                if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Interact_TG")))
                {
                    Net_Housing_Object ob = hit.collider.gameObject.GetComponentInParent<Net_Housing_Object>();
                    if (ob == TCP_Client_Manager.instance.housing_ui_manager.now_focus_ob)
                    {
                        select_timer += Time.deltaTime;
                        if (select_timer >= 0.5f)
                        {
                            TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
                            TCP_Client_Manager.instance.placement_system.remove(ob);
                            TCP_Client_Manager.instance.placement_system.StartPlacement((int)ob.object_enum);
                        }
                    }
                    else
                    {
                        select_timer = 0f;
                    }
                }
                else
                {
                    select_timer = 0f;
                }
            }
            else {
                select_timer = 0f;
            }


            /*if (now_btn_up && !old_btn_up && IsPointerOverUI())
            {
                Debug.Log("exit");
                // OnExit?.Invoke();
            }*/

            old_btn_up = now_btn_up;
        }

       
        


        
       


       
       
        
    }

    public bool IsPointerOverUI() {
        
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                // ui touched

            }
        }
        return EventSystem.current.IsPointerOverGameObject();     
    }

    public bool IsPointerOverClickableUI() { // 마이플래닛에서 캐릭터와 상호작용 가능한UI 위에 있는지 체크 
        return false;
       // return true;
    }

    public Vector3 GetSelectedPosition()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touches.Length == 0)
            {
                return Vector3.up * 10000;
            }
        }
        
        //마우스 선택 위치 찍기
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = scene_cam.nearClipPlane; //카메라의 위치부터 렌더링을 시작 할 최소 위치까지의 거리(카메라 컴포넌트에서 near값 return)
        Ray ray = scene_cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placement_layermask))
        {
            last_postion = hit.point;
        }

        last_postion = new Vector3(last_postion.x, 0, last_postion.z);
        return last_postion;
    }
}
