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
    public List<int> index_list = new List<int>();
    public Shop_info()
    {

    }

    public Shop_info(JsonData json)//���� ȸ��
    {
        if (json.IsObject)
        {
            foreach (JsonData data in json["index_list"])
            {
                index_list.Add(int.Parse(data.ToString()));
            }
        }
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


