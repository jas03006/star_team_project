using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoodsDataBaseSO : ScriptableObject
{
    public List<Goods> GoodsData;
}
public enum Shop_cate
{
    none = -1,
    housing,
    emoji,
    ruby,
    etc
}

public enum Goods_id
{
    none = -1,
    good1,
    good2,
    good3,
    good4,
    good5
}

public class Shop_info //userdata
{
    public List<Goods_info> goods_list = new List<Goods_info>();
    public Shop_info()
    {

    }

    public Shop_info(JsonData json)//기존 회원
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["goods_list"])
            {
                goods_list.Add(new Goods_info(data));
            }
        }
    }

    public void Insert_data()//회원 가입
    {
       
    }
}

public class Goods_info
{
    public Goods_id id;
    public bool purchased = false;

    public Goods_info()
    {
    
    }

    public Goods_info(Goods_id id_, bool purchased_)
    {
        id = id_;
        purchased = purchased_;
    }

    public Goods_info(JsonData json)
    {
        id = (Goods_id)Int32.Parse(json["id"].ToString());
        purchased = bool.Parse(json["purchased"].ToString());
    }
}


