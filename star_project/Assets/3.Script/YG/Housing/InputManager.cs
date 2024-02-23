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
