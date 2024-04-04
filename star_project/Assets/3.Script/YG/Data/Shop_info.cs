using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_info //userdata
{
    public List<int> index_list = new List<int>();
    public Shop_info()
    {

    }

    public Shop_info(JsonData json)//기존 회원
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


