using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Item_Info_JGD
{
    public int HouseItemCount = 0;
    public string ItemName = "none";

    public House_Item_Info_JGD()
    {
        HouseItemCount = UnityEngine.Random.Range(0, 99);
    }

    public House_Item_Info_JGD(JsonData json)
    {
        if (json.IsObject)
        {

             HouseItemCount = Int32.Parse(json["HouseItemCount"].ToString());


            ItemName = json["ItemName"].ToString();
        }
    }
}