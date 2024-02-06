using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int id;
    Grid gird;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public PlacementState( int id, Grid gird, PreviewSystem previewSystem, ObjectsDatabaseSO database, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        this.id = id;
        this.gird = gird;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectData.FindIndex(data => data.id == id);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objectData[selectedObjectIndex].prefab, database.objectData[selectedObjectIndex].size);
        }
        else
        {
            throw new System.Exception($"No object with ID {id}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].prefab, gird.CellToWorld(gridPosition));

        GridData selectedData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, database.objectData[selectedObjectIndex].size, database.objectData[selectedObjectIndex].id, index);
        previewSystem.UpdatePostition(gird.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
        return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectedObjectIndex].size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePostition(gird.CellToWorld(gridPosition), placementValidity);
    }
}
