using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Item_Info_JGD
{
    public int HouseItemCount = 0;
    public housing_itemID ItemName = housing_itemID.none;

    public House_Item_Info_JGD()
    {
        HouseItemCount = UnityEngine.Random.Range(0, 99);
       // ItemName = (housing_itemID) UnityEngine.Random.Range(0, Enum.GetValues(typeof(housing_itemID)).Length);
    }

    public House_Item_Info_JGD(JsonData json)
    {
        if (json.IsObject)
        {

            HouseItemCount = Int32.Parse(json["HouseItemCount"].ToString());
           // ItemName = (housing_itemID)Int32.Parse(json["ItemName"].ToString());
        }
    }
}