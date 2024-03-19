using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_object : MonoBehaviour
{
    [SerializeField] private float rotate_speed = 5f;
    [SerializeField] private float rotate_direction = 1f;
    [SerializeField] private Vector3 rotate_axis =  Vector3.up;
    // Update is called once per frame
    void Update()
    {
       // transform.Rotate(0,Time.deltaTime * rotate_speed * rotate_direction, 0);
        transform.Rotate(rotate_axis, Time.deltaTime * rotate_speed * rotate_direction);
    }
}
