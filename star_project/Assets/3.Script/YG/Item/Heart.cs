using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    [field: SerializeField]
    public int percent { get; private set; } //ȸ����

    [field: SerializeField]
    public int duration { get; private set; }

    public void UseItem()
    {
        
    }
    
}
