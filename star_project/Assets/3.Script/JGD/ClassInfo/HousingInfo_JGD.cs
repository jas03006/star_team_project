using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HousingInfo_JGD
{
    public int level = 0; //Ȯ�� ����
    public int exp = 0;

    public List<HousingObjectInfo> objectInfos = new List<HousingObjectInfo>();
    public HousingInfo_JGD()
    {

    }
    public HousingInfo_JGD(Dictionary<Vector3Int, PlacementData> placement_info)
    {
        foreach (Vector3Int key in placement_info.Keys)
        {
            PlacementData pd = placement_info[key];
            objectInfos.Add(new HousingObjectInfo(pd.ID, new Vector2(key.x, key.z), pd.direction));
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
    public int direction = 0; //0,1,2,3 ��������
    public housing_itemID item_ID = housing_itemID.none;
    public HousingObjectInfo()
    {
        position = new Vector2(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
    }

    public HousingObjectInfo(housing_itemID item_ID_)
    {
        item_ID = item_ID_;
        position = new Vector2(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
    }
    public HousingObjectInfo(housing_itemID item_ID_, Vector2 position_, int direction_)
    {
        item_ID = item_ID_;
        position = position_;
        direction = direction_;
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

[Serializable]
public enum housing_itemID
{
    none = -1,
    ark_cylinder = 1,
    airship,
    star_nest,
    chair,
    bed,
    table,
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
    public string UUID; //��ǻ� �г���
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

    public void Change(string UUID_, string content_) //���� ���� �ٲٴ� �޼���
    {
        UUID = UUID_;
        content = content_;
    }

}
