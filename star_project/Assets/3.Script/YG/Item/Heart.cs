using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    [field: SerializeField]
    public int percent { get; private set; } //È¸º¹·®

    [field: SerializeField]
    public int duration { get; private set; }

    public void UseItem()
    {
        
    }
    
}
