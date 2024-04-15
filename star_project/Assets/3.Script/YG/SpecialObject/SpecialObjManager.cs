using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public interface IObject
{
    void Interactive();
}

public class SpecialObjManager : MonoBehaviour
{
    [SerializeField] Harvesting harvesting;

    private void Start()
    {
        harvesting = GetComponent<Harvesting>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ScanObject();
        }
    }

    public void ScanObject()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "HousingObject")
            {
                //특수 하우징 오브젝트는 SpecialObj 컴포넌트를 들고있음
                SpecialObj obj = hit.collider.gameObject.GetComponent<SpecialObj>();
                if (obj != null)
                {
                    StateChange(obj.object_enum);
                }
            }
        }
    }

    public void StateChange(housing_itemID id)
    {
        switch (id)
        {
            case housing_itemID.none:
                break;
            case housing_itemID.ark_cylinder:
               // harvesting.Interactive();
                break;
            default:
                break;
        }
    }
}
