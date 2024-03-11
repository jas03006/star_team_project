using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class House_Inventory_Info_JGD
{
    public List<House_Item_Info_JGD> item_list = new List<House_Item_Info_JGD>();
    public House_Inventory_Info_JGD()
    {
      
    }


    public House_Inventory_Info_JGD(JsonData json)
    {
        if (json.IsObject)
        {
            foreach (JsonData item in json["item_list"])
            {
                Add(new House_Item_Info_JGD(item));
            }
        }
    }

    public void Add(housing_itemID id_, int count_)
    {   //TODO: 중복 검사 후 누적 시키기
        for (int i=0; i < item_list.Count; i++) {
            if (item_list[i].id == id_) {
                item_list[i].count += count_;
                return;
            }
        }
        item_list.Add(new House_Item_Info_JGD(id_,  count_));
    }
    public void Add(House_Item_Info_JGD item_info)
    {
        for (int i = 0; i < item_list.Count; i++)
        {
            if (item_list[i].id == item_info.id)
            {
                item_list[i].count += item_info.count;
                return;
            }
        }
        item_list.Add(item_info);
    }

    public void Remove(housing_itemID id_, int count_=0) {
        if (count_ <= 0) {
            for (int i = 0; i < item_list.Count; i++)
            {
                if (item_list[i].id == id_)
                {
                    item_list.RemoveAt(i);
                    return;
                }
            }
        }
    }
}

public class House_Item_Info_JGD
{
    
    public housing_itemID id = housing_itemID.none;
    public int count = 0;
    public House_Item_Info_JGD()
    {
        count = UnityEngine.Random.Range(0, 99);
       // ItemName = (housing_itemID) UnityEngine.Random.Range(0, Enum.GetValues(typeof(housing_itemID)).Length);
    }

    public House_Item_Info_JGD(housing_itemID id_, int count_) {
        id = id_;
        count = count_;
    }

    public House_Item_Info_JGD(JsonData json)
    {
        if (json.IsObject)
        {
            id = (housing_itemID)Int32.Parse(json["id"].ToString());
            count = Int32.Parse(json["count"].ToString());            
        }
    }
}