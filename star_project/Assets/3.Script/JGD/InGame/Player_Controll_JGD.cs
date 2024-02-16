using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controll_JGD : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] private float Jump;
    [SerializeField] public float Speed;
    public Rigidbody2D rigi;
    

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        if(this.transform.position.x >=camera.transform.position.x)
        {
            camera.transform.position = new Vector3(this.transform.position.x, 3, -3);
        }

    }
    private void FixedUpdate()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            rigi.velocity = Vector2.up *Jump;
        }
    }



}
