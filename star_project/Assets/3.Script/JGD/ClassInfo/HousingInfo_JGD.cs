using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HousingInfo_JGD
{
    public int level = 0; //확장 레벨
    public int exp = 0;
    //public string name = "as";
    //public bool flag = false;
    
    public List<HousingObjectInfo> objectInfos = new List<HousingObjectInfo>();
    public HousingInfo_JGD()
    {
    }

    public HousingInfo_JGD(JsonData json)
    {
        //Debug.Log(json.ToString());
        if (json.IsObject) {
            level = Int32.Parse(json["level"].ToString());
            exp = Int32.Parse(json["exp"].ToString());
            foreach (LitJson.JsonData item in json["objectInfos"])
            {
                objectInfos.Add(new HousingObjectInfo(item));
            }
        }
    }

    public void add_object(HousingObjectInfo obj) {
        objectInfos.Add(obj);
    }
}

public class HousingObjectInfo
{
    public Vector2 position;
    public int direction = 0; //0,1,2,3 동서남북
    public housing_itemID item_ID = housing_itemID.none;
    public HousingObjectInfo()
    {
        position = new Vector2(0, UnityEngine.Random.Range(0, 100));
    }
    public HousingObjectInfo(housing_itemID item_ID_)
    {
        item_ID = item_ID_;
        position = new Vector2(0, UnityEngine.Random.Range(0, 100));
    }
    public HousingObjectInfo(JsonData json)
    {
        //Debug.Log(json.ToString());
        if (json.IsObject)
        {
            position = new Vector2(Int32.Parse(json["position"][0].ToString()), Int32.Parse(json["position"][1].ToString()));
            direction = Int32.Parse(json["direction"].ToString());
            item_ID = (housing_itemID)Int32.Parse(json["item_ID"].ToString());
        }
    }

}
public enum housing_itemID
{
    none = -1,
    ark_cylinder,
    airship,
    star_nest,
    chair,
    bed,
}
