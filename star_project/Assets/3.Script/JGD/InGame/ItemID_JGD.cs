using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemID_JGD : MonoBehaviour
{
    public int ID = 0;
    public int discrimination;
    public float distance;
    [SerializeField] public Obstacle_ID obstacle_ID;
    [SerializeField]private List<GameObject> obstacles = new List<GameObject>();
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        obstacle_ID = (Obstacle_ID)ID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player"))
        {
            Scanner();
            if (obstacles.Count == 0)
            {
                return;
            }
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].GetComponentInParent<ItemID_JGD>().animator.SetTrigger("MoveWall");
            }
            this.gameObject.SetActive(false);
        }
    }

    private void Scanner()//움직이는 장애물이면 가져오기
    {
        obstacles.Clear();
        if (distance == 0)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, distance*100,LayerMask.GetMask("MoveWall"));
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject Obj = colliders[i].gameObject;
            if (Obj.GetComponentInParent<ItemID_JGD>().discrimination == this.discrimination)// 장애물의 discrimination 값이 현재 오브젝트의 discrimination 값과 일치하면 리스트에 추가
            {
                obstacles.Add(Obj);
            }   

        }
    }
}
