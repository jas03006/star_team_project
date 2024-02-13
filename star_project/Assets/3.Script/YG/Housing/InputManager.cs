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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Onclicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedPosition()
    {
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
