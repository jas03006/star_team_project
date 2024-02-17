using LitJson;
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public item_ID id;
    public string item_name;
    public Sprite sprite;

    //해당하는 아이템
    public char alphabet; //스펠링 //Alphabet
    public double percent;//회복량 //heart
    public int num; //개수 //star,shield,size,speed
    public int duration; //지속시간 //speed,size,magnet
    public Item()
    {

    }
    public Item(JsonData gameData)
    {
        id = (item_ID)int.Parse(gameData["item_ID"].ToString());
        item_name = gameData["item_name"].ToString();
        //sprite = DataBaseManager.Instance.Num2Sprite(int.Parse(gameData["sprite"].ToString()));

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
            num = int.Parse(gameData["num"].ToString());

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

//0215 todo
//차트매니저에서 아이템 데이터 불러오기
//차트매니저에서 아이템 생성해서 정보 들고있기
//아이템 스킬 어디서 사용할지 정하기

