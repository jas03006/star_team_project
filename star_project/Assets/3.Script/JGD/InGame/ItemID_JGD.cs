using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID_JGD : MonoBehaviour
{
    public int ID = 0;
    [SerializeField] public Obstacle_ID obstacle_ID;

    private void Start()
    {
        obstacle_ID = (Obstacle_ID)ID;
    }
}
