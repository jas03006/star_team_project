using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controll_JGD : MonoBehaviour
{
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    public Rigidbody2D rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigi.velocity = Vector2.up * Jump;
        }
    }
}
