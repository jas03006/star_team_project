using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float Speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            while(collision)
            {
                Vector3 Dis = this.gameObject.transform.position - collision.transform.position;
                Vector3 pos = Dis.normalized;
                collision.transform.position += pos * Speed * Time.deltaTime;
            }

        }
    }
}
