using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Item_Info_JGD : MonoBehaviour
{
    public int HouseItemCount = 0;
    public string ItemName;

    public House_Item_Info_JGD(JsonData json)
    {
        HouseItemCount = Int32.Parse(json["HouseItem"].ToString());

        ItemName = json["HouseName"].ToString();
    }
}