using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int num;
    public int UseItem(Player_YG player)
    {
        return player.star_num + num;
    }
}
