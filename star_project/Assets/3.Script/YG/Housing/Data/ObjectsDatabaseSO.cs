using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;

    public Dictionary<housing_itemID, ObjectData> object_dic = null;

    public ObjectData get_object(housing_itemID id) {
        if (object_dic == null || object_dic.Count != objectData.Count) {
            init_dic();
        }
        return object_dic[id];
    }

    public void init_dic() {
        if (object_dic == null)
        {
            object_dic = new Dictionary<housing_itemID, ObjectData>();
        }
        else {
            object_dic.Clear();
        }
        
        for (int i =0; i < objectData.Count; i++) {
            object_dic[objectData[i].id] = objectData[i];
        }
    }
}


public enum housing_object_category { 
    none = -1,
    common = 0,
    special = 1
}

[Serializable]
public class ObjectData
{
    [field :SerializeField] 
    public string name { get; private set; }
    [field: SerializeField] 
    public housing_itemID id { get; private set; }
    [field: SerializeField]
    public Vector2Int size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject prefab { get; private set; }

    [field: SerializeField]
    public housing_object_category category { get; private set; }
    //[field: SerializeField]
    //public housing_itemID special_object { get; private set; } = housing_itemID.none;

}
