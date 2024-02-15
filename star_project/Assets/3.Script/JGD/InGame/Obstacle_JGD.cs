using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_JGD : MonoBehaviour
{
    [SerializeField] float Speed;
    Spawner_JGD spawner_;
    Rigidbody rigi;
    
    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigi.velocity = Vector3.left * Speed;
    }
}
