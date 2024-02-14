using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    [field: SerializeField]
    public item_ID id { get; private set; } = item_ID.Alphabat;

    [field: SerializeField]
    public string item_name { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }

    [field: SerializeField]
    public string alphabat { get; private set; } //알파벳

    [field: SerializeField]
    public int percent { get; private set; } //heart,size,speed

    [field: SerializeField]
    public int time { get; private set; } //지속시간
}

public enum item_ID
{
    None,
    Alphabat,
    Heart,
    Star,
    Shield,
    Megnet,
    SpeedUp,
    SizeUp,
    SizeDown,
    Random
}
/*
     public void Interactive()
{
    switch (id)
    {
        case item_ID.None:
            Debug.Log("id = none");
            break;
        case item_ID.Alphabat:
            Alphabat();
            break;
        case item_ID.Heart:
            Heart();
            break;
        case item_ID.Star:
            Star();
            break;
        case item_ID.Shield:
            Shield();
            break;
        case item_ID.Megnet:
            Megnet();
            break;
        case item_ID.SpeedUp:
            Speed();
            break;
        case item_ID.SizeUp:
            Size();
            break;
        case item_ID.SizeDown:
            Size();
            break;
        case item_ID.Random:
            Random();
            break;
        default:
            break;
    }
}

public void Alphabat()
{

}

public void Heart()
{

}

public void Star()
{

}

public void Shield()
{

}

public void Megnet()
{

}

public void Speed()
{

}

public void Size()
{

}

public void Random()
{
    int ran = UnityEngine.Random.Range(0,5);
    switch (ran) 
    {
        case 0:
            Shield();
            break;
        case 1:
            Megnet();
            break;
        case 2:
            Speed();
            break;
        case 3:
            Size();
            break;
        case 4:
            Size();
            break;
        default:
            break;
    }
}
 */

