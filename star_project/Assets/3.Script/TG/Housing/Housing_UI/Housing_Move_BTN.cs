using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Housing_Move_BTN : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        TCP_Client_Manager.instance.housing_ui_manager.click_down_move_btn();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        TCP_Client_Manager.instance.placement_system.inputManager.invoke_onclick_while_move();
    }
}
