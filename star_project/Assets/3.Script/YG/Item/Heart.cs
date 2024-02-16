using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    [field: SerializeField]
    public float percent { get; private set; } //ȸ����

    public int UseItem(Player_YG player)
    {
        return player.hp += (int)(player.hp * percent);
    }
}