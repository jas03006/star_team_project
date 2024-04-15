using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float Speed;

    Coroutine Black = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Black != null)
            {
                StopCoroutine(Black);
            }
            Black = StartCoroutine(BlackHole_co(collision));

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Black != null)
            {
                StopCoroutine(Black);
            }

        }
    }


    private IEnumerator BlackHole_co(Collider2D collision)
    {
        while (collision)
        {
            Vector3 Dis = this.gameObject.transform.position - collision.transform.position;
            Vector3 pos = Dis.normalized;
            collision.transform.position += pos * Speed * Time.deltaTime;
            yield return null;
        }
    }
}
