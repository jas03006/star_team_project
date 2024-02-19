using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public Dictionary<Vector3Int, PlacementData> placedObjects = new();
    public int[] level_boudary = {7,10, 13,16};//레벨 별 그리드 반 크기
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

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize, bool is_path_finding = false, bool is_limit_apply = true)
    {
        List<Vector3> player_pos_list = new List<Vector3>();
        if (!is_path_finding)
        {
           /* player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.my_player.transform.position));
            Dictionary<string,Net_Move_Object_TG> dic = TCP_Client_Manager.instance.net_mov_obj_dict;
            foreach (string key in dic.Keys) {
                player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(dic[key].transform.position));
            }*/

            //리스폰 지점에 설치 금지, 이동은 가능
            player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.get_respawn_point(TCP_Client_Manager.instance.now_room_id)));
            player_pos_list.Add(TCP_Client_Manager.instance.placement_system.grid.WorldToCell(TCP_Client_Manager.instance.get_respawn_point("")));
        }

        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (is_limit_apply && !is_inner_pos(pos))
            {
                return false;
            }
            if (placedObjects.ContainsKey(pos) || player_pos_list.Contains(pos))
            {
                
                return false;
            }
        }
        return true;
    }

    public bool is_inner_pos(Vector3Int pos_) {
        int level = TCP_Client_Manager.instance.placement_system.housing_info.level;//BackendGameData_JGD.userData.Housing_Info.level;
        if (level > level_boudary.Length-1) { 
            level = level_boudary.Length-1;
        }
        if (pos_.x > level_boudary[level] || pos_.x < -level_boudary[level] || pos_.z > level_boudary[level] || pos_.z < -level_boudary[level] )
        {
            return false;
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