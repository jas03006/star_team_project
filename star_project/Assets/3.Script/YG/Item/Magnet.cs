using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float Speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Vector3 Dis = collision.gameObject.transform.position - this.transform.position;
            Vector3 pos = Dis.normalized;

            collision.transform.position += pos * Speed * Time.deltaTime;
        }
    }
}
