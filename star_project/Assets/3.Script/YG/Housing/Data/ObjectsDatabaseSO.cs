using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
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
    //[field: SerializeField]
    //public housing_itemID special_object { get; private set; } = housing_itemID.none;

}
