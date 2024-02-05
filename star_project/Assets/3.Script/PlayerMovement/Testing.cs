using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Camera main_cam;
    void Start()
    {
        Grid_ gird = new Grid_(4,2,10f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = main_cam.ScreenToWorldPoint(Input.mousePosition);
            vec.z = 0;
        }
    }
}
