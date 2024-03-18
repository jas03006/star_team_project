using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum goods_cate
{
    none = -1,
    housing,
    imozi,
    etc
}

public class Goods : MonoBehaviour
{
    public int index;
    public goods_cate cate;
    public string goods_name;
    public int value;
    public bool have_num;//갯수 정해서 살수있는지
    public bool can_repurchase;//재구매 가능한지
    public housing_itemID housing_id;
    public int imozi_id;

    public Goods(JsonData gameData)
    {
        index = int.Parse(gameData["index"].ToString());
        cate = (goods_cate)int.Parse(gameData["cate"].ToString());
        goods_name = gameData["name"].ToString();
        value = int.Parse(gameData["value"].ToString());
        have_num = bool.Parse(gameData["have_num"].ToString());
        can_repurchase = bool.Parse(gameData["can_repurchase"].ToString());
        housing_id = (housing_itemID)int.Parse(gameData["housing_id"].ToString());
        imozi_id = int.Parse(gameData["imozi_id"].ToString());
    }
}
