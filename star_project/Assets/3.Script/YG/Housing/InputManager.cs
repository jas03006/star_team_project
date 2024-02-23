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
    private void Update()
    {
        bool now_btn_up = Input.GetMouseButtonUp(0);
        if (now_btn_up && !IsPointerOverUI()) {
            Onclicked?.Invoke();
            OnExit?.Invoke();
        }


        if ( now_btn_up && !old_btn_up && IsPointerOverUI())
        {
            Debug.Log("exit");
           // OnExit?.Invoke();
        }
        old_btn_up = now_btn_up;
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
