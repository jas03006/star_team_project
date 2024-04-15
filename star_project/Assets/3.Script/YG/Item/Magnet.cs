using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] GameObject Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item") || collision.gameObject.layer == LayerMask.NameToLayer("Alphabet"))
        {
            //Vector3 Dis = this.gameObject.transform.position - collision.transform.position;
            //Vector3 pos = Dis.normalized;
            //while(collision)
            //{
            //    collision.transform.position += pos * Speed * Time.deltaTime;
            //}
            StartCoroutine(Magnettic(collision));
        }
    }
    private IEnumerator Magnettic(Collider2D collision)
    {
        float speed = Speed;
        while (collision && this.gameObject)
        {
            Vector3 Dis = this.gameObject.transform.position - collision.transform.position;
            Vector3 pos = Dis.normalized;
            collision.transform.position += pos * (3f + speed) * Time.deltaTime;
            if (collision.transform.position.x < Player.transform.position.x)
            {
                speed = 3f;
            }
            yield return null;
        }
    }
}
