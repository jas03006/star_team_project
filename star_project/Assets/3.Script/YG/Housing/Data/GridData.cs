using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectsize, housing_itemID id, int placedobjectindex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectsize);
        PlacementData data = new PlacementData(positionToOccupy, id, placedobjectindex);

        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                throw new Exception($"Dictionary already contains this cell position {pos}");
            placedObjects[pos] = data; 
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectsize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectsize.x; x++)
        {
            for (int y = 0; y < objectsize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize, bool is_path_finding = false)
    {
        List<Vector3> player_pos_list = new List<Vector3>();        
        player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.my_player.transform.position));
        Dictionary<string,Net_Move_Object_TG> dic = TCP_Client_Manager.instance.net_mov_obj_dict;
        foreach (string key in dic.Keys) {
            player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(dic[key].transform.position));
        }

        if (!is_path_finding) { //리스폰 지점에 설치 금지, 이동은 가능
            player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.get_respawn_point(TCP_Client_Manager.instance.now_room_id)));
            player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.get_respawn_point("-")));
        }

        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos) || player_pos_list.Contains(gridPosition))
            {
                return false;
            }
        }
        return true;
    }

    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPostitions)
        {
            placedObjects.Remove(pos);
        }
    }

    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if (placedObjects.ContainsKey(gridPosition) == false)
            return -1;
        return placedObjects[gridPosition].PlacedObjectIndex;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPostitions;
    public housing_itemID ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }
    public int direction;
    public PlacementData(List<Vector3Int> occupiedPostitions, housing_itemID id, int placeObjectIndex, int direction=0)
    {
        this.occupiedPostitions = occupiedPostitions;
        ID = id;
        PlacedObjectIndex = placeObjectIndex;
        this.direction = direction;
    }

}