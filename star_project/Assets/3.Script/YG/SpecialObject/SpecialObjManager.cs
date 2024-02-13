using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public enum specialobject_enum //���ٰ���
{
    none = -1, // Ư�� ������ƮX
    ark_cylinder
}

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
                //Ư�� �Ͽ�¡ ������Ʈ�� SpecialObj ������Ʈ�� �������
                SpecialObj obj = hit.collider.gameObject.GetComponent<SpecialObj>();
                if (obj != null)
                {
                    StateChange(obj.object_enum);
                }
            }
        }
    }

    public void StateChange(specialobject_enum id)
    {
        switch (id)
        {
            case specialobject_enum.none:
                break;
            case specialobject_enum.ark_cylinder:
                harvesting.Interactive();
                break;
            default:
                break;
        }
    }
}
