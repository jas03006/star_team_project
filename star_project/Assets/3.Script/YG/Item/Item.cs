using LitJson;
using System;
using UnityEngine;

[Serializable]
public class Item //��Ʈ ���� ������ ���� Ŭ���� ����(�ΰ��ӿ����� Item_game ��� script ��� )
{
    public item_ID id;
    public string item_name;
    public int sprite;

    //�ش��ϴ� ������
    public char alphabet; //���縵 //Alphabet
    public double percent;//ȸ���� //heart
    public double num; //���� //star,shield,size,speed
    public int duration; //���ӽð� //speed,size,magnet

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

