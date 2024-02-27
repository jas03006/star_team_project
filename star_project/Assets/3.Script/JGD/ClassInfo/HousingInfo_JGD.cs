using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HousingInfo_JGD
{
    public int level = 0; //확장 레벨
    public int exp = 0;

    public List<HousingObjectInfo> objectInfos = new List<HousingObjectInfo>();
    public HousingInfo_JGD()
    {

    }
    public HousingInfo_JGD(Grid grid, Dictionary<Vector3Int, PlacementData> placement_info, ObjectPlacer objectPlacer, int level_)
    {
        level = level_;
        /*foreach (Vector3Int key in placement_info.Keys)
        {
            PlacementData pd = placement_info[key];
            HousingObjectInfo hoi = new HousingObjectInfo(pd.ID, new Vector2(key.x, key.z), pd.direction);
            objectInfos.Add(hoi);
            if (pd.ID == housing_itemID.ark_cylinder) {
                Harvesting hob = objectPlacer.find_harvest_object(pd.PlacedObjectIndex);
                if (hob!=null) {
                    hoi.start_time = hob.start_time;
                    hoi.harvesting_selection = hob.selection;
                }
            }
            
        }*/

        for (int i=0; i <  objectPlacer.placedGameObject.Count; i++)
        {
            GameObject go = objectPlacer.placedGameObject[i];
            if (go == null) {
                continue;
            }
            Net_Housing_Object nho =  go.GetComponentInChildren<Net_Housing_Object>();
            Vector3Int pos = grid.WorldToCell(go.transform.position);
            HousingObjectInfo hoi = new HousingObjectInfo(nho.object_enum, new Vector2(pos.x, pos.z), placement_info[pos].direction);
            objectInfos.Add(hoi);
            if (nho.object_enum == housing_itemID.ark_cylinder)
            {
                Harvesting hob = nho as Harvesting;
                if (hob != null)
                {
                    hoi.start_time = hob.start_time;
                    hoi.harvesting_selection = hob.selection;
                }
            }
        }
    }

    public HousingInfo_JGD(JsonData json)
    {
        //Debug.Log(json.ToString());
        if (json.IsObject)
        {
            level = Int32.Parse(json["level"].ToString());
            exp = Int32.Parse(json["exp"].ToString());

            foreach (JsonData item in json["objectInfos"])
            {
                objectInfos.Add(new HousingObjectInfo(item));
            }
        }
    }
    public void Add_object(HousingObjectInfo obj)
    {
        objectInfos.Add(obj);
    }
   
}

public class HousingObjectInfo
{
    public Vector2 position;
    public int direction = 0; //0,1,2,3 동서남북
    public housing_itemID item_ID = housing_itemID.none;
    public int harvesting_selection = -1;
    public DateTime start_time = DateTime.MaxValue;
   
    public HousingObjectInfo()
    {
        position = new Vector2(UnityEngine.Random.Range(0, 7), UnityEngine.Random.Range(0, 7));
    }

    public HousingObjectInfo(housing_itemID item_ID_)
    {
        item_ID = item_ID_;
        position = new Vector2(0,0);
        direction = 0;
        start_time = DateTime.MaxValue;
        harvesting_selection = -1;
    }
    public HousingObjectInfo(housing_itemID item_ID_, Vector2 position_, int direction_)
    {
        item_ID = item_ID_;
        position = position_;
        direction = direction_;
        start_time = DateTime.MaxValue;
        harvesting_selection = -1;
    }
    public HousingObjectInfo(JsonData json)
    {
        //Debug.Log(json.ToString());
        if (json.IsObject)
        {
            position = new Vector2(Int32.Parse(json["position"][0].ToString()), Int32.Parse(json["position"][1].ToString()));
            direction = Int32.Parse(json["direction"].ToString());
            item_ID = (housing_itemID)Int32.Parse(json["item_ID"].ToString());
            start_time = Convert.ToDateTime(json["start_time"].ToString());
            harvesting_selection = Int32.Parse(json["harvesting_selection"].ToString());
        }
    }
}

[Serializable]
public enum housing_itemID
{
    none = -1,
    ark_cylinder = 1,
    airship=2,
    star_nest=3,
    chair=4,
    bed=5,
    table=6,
    post_box=7,
    panda = 8,
    balloon =9,
    block = 10,
    camera = 11,
    airplane =12
    
    
    
}

public class Memo_info
{
    public List<Memo> memo_list { get; private set; } = new List<Memo>();

    public Memo_info()
    {
        memo_list = new List<Memo>();
    }

    public Memo_info(JsonData json)
    {
        if (json.IsObject)
        {
            foreach (LitJson.JsonData item in json["memo_list"])
            {
                memo_list.Add(new Memo(item));
            }
        }
    }

    public void Add_object()
    {
        memo_list.Add(new Memo());
    }

    public void Add_object(Memo memo)
    {
        memo_list.Add(memo);
    }
}

public class Memo
{
    public string UUID; //사실상 닉네임
    public string content;

    public Memo(JsonData json)
    {
        UUID = json["UUID"].ToString();
        content = json["content"].ToString();
    }

    public Memo()
    {
        UUID = string.Empty;
        content = string.Empty;
    }

    public void Change(string UUID_, string content_) //방명록 내용 바꾸는 메서드
    {
        UUID = UUID_;
        content = content_;
    }

}
