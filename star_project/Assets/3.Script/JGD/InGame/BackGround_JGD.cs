using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround_JGD : MonoBehaviour
{
    [SerializeField] private GameObject BackGround_1;
    [SerializeField] private float speed;
    Rigidbody rigi;
    private void Awake()
    {
        BackGround_1 = GetComponent<GameObject>();
        rigi = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rigi.velocity = Vector3.left * speed;
        if (this.transform.position.x <= -90)
        {
            this.transform.position = new Vector3(56.3f, 0, 19f);
        }
    }

}
