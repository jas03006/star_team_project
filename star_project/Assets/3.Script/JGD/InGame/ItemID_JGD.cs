using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID_JGD : MonoBehaviour
{
    public int ID = 0;
    public int discrimination;
    public float distance;
    [SerializeField] public Obstacle_ID obstacle_ID;
    [SerializeField]private List<GameObject> obstacles = new List<GameObject>();

    private Animator animator;

    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //}

    private void Start()
    {
        obstacle_ID = (Obstacle_ID)ID;
        Scanner();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].GetComponent<ItemID_JGD>().animator.SetTrigger("MoveWall");
            }
        }
    }





    private void Scanner()
    {
        if (distance == 0)
        {
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if(obj.GetComponent<ItemID_JGD>().discrimination == this.discrimination)
            {
                obstacles.Add(obj);
            }
        }
    }
}
