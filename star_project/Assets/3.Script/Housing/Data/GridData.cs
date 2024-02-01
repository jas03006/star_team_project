using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectsize, int id, int placedobjectindex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectsize);
        PlacementData data = new PlacementData(positionToOccupy, id, placedobjectindex);

        foreach (var pos in positionToOccupy)
        {

        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectsize)
    {
        throw new NotImplementedException();
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPostitions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPostitions, int id, int placeObjectIndex)
    {
        this.occupiedPostitions = occupiedPostitions;
        ID = id;
        PlacedObjectIndex = placeObjectIndex;
    }

}