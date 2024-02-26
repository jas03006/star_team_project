using LitJson;
using System;
using UnityEngine;

[Serializable]
public class Item //차트 정보 저장을 위한 클래스 선언(인게임에서는 Item_game 상속 script 사용 )
{
    public item_ID id;
    public string item_name;
    public int sprite;

    //해당하는 아이템
    public char alphabet; //스펠링 //Alphabet
    public double percent;//회복량 //heart
    public double num; //개수 //star,shield,size,speed
    public int duration; //지속시간 //speed,size,magnet

    public Item()
    {

    }

    public Item(JsonData gameData)
    {
        id = (item_ID)int.Parse(gameData["item_ID"].ToString());
        item_name = gameData["item_name"].ToString();
        sprite = int.Parse(gameData["sprite_ID"].ToString());

        if ((int)id < (int)item_ID.small_heart) //Alphabet
        {
            alphabet = item_name[0];
        }
        else if ((int)id < (int)item_ID.small_star) //heart
        {
            percent = double.Parse(gameData["percent"].ToString());
        }
        else if ((int)id < (int)item_ID.Megnet) //star,shield,size,speed
        {
            num = double.Parse(gameData["num"].ToString());

            if ((int)id > (int)item_ID.Shield)//size,speed
            {
                duration = int.Parse(gameData["duration"].ToString());
            }
        }
        else if ((int)id == (int)item_ID.Megnet)//megnet
        {
            duration = int.Parse(gameData["duration"].ToString());
        }
    }
}

public enum item_ID
{
    None = -1,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    small_heart,
    big_heart,
    small_star,
    big_star,
    Shield,
    SpeedUp,
    SizeUp,
    SizeDown,
    Megnet,
    Random
}

