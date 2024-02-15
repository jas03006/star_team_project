using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controll_JGD : MonoBehaviour
{
    [SerializeField] private float Jump;
    Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigi.velocity = Vector3.up * Jump;
        }
    }
}
