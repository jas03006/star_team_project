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
    public float select_timer = 0f;
    private float select_time_threshold = 0.5f;
    public bool is_moving { get { return select_timer > select_time_threshold; }  }
    private void Update()
    {
        if (Input.touchCount >= 2)
        {
            return;
        }

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

            /*if (Input.GetMouseButton(0) && TCP_Client_Manager.instance.housing_ui_manager.now_focus_ob != null && !IsPointerOverUI())
            {
                if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Interact_TG")))
                {
                    Net_Housing_Object ob = hit.collider.gameObject.GetComponentInParent<Net_Housing_Object>();
                    if (ob == TCP_Client_Manager.instance.housing_ui_manager.now_focus_ob)
                    {
                        select_timer += Time.deltaTime;
                        if (select_timer >= select_time_threshold)
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
            }*/


            /*if (now_btn_up && !old_btn_up && IsPointerOverUI())
            {
                Debug.Log("exit");
                // OnExit?.Invoke();
            }*/

            old_btn_up = now_btn_up;
        }

        
    }

    public void invoke_onclick_while_move() {
        if (TCP_Client_Manager.instance.housing_ui_manager.is_move && Onclicked != null)
        {
            TCP_Client_Manager.instance.housing_ui_manager.is_move = false;
            Onclicked?.Invoke();
            OnExit?.Invoke();
            
        }
    }

    public static bool IsPointerOverUI() {
        
        for (int i =0; i<Input.touchCount;i++)
        {
            Debug.Log("touch check"+i);
            int id = Input.GetTouch(i).fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                
                // ui touched
                return true;
            }

            var eventData = new PointerEventData(EventSystem.current) { position = Input.GetTouch(0).position };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            if (results.Count > 0)
            {
                foreach (RaycastResult result in results) {
                    if (result.gameObject.GetComponentsInChildren<Housing_Move_BTN>().Length > 0) {
                        Debug.Log("is on move btn");
                        return false;
                    }
                }
                return true;
            }
        }


        return EventSystem.current.IsPointerOverGameObject();     
    }

    public bool IsPointerOverClickableUI() { // �����÷��ֿ��� ĳ���Ϳ� ��ȣ�ۿ� ������UI ���� �ִ��� üũ 
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
        
        //���콺 ���� ��ġ ���
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = scene_cam.nearClipPlane; //ī�޶��� ��ġ���� �������� ���� �� �ּ� ��ġ������ �Ÿ�(ī�޶� ������Ʈ���� near�� return)

        if (TCP_Client_Manager.instance.housing_ui_manager.is_move)
        {
            mousePos.y += TCP_Client_Manager.instance.housing_ui_manager.ui_distance;
        }

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
