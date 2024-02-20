using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    public housing_itemID id;
    public Grid gird;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData floorData;
    public GridData furnitureData;
    ObjectPlacer objectPlacer;

    public DateTime start_time;
    public int direction;
    public int selection;

    public PlacementState(housing_itemID id, Grid gird, PreviewSystem previewSystem, ObjectsDatabaseSO database, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer
        , DateTime start_time, int direction =0, int selection=-1)
    {
        this.id = id;
        this.gird = gird;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        this.start_time = start_time;
        this.direction = direction;
        this.selection = selection;


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

        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].prefab, gird.CellToWorld(gridPosition) + new Vector3(gird.cellSize.x / 2f, 0, gird.cellSize.z / 2f), this);

        GridData selectedData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, database.objectData[selectedObjectIndex].size, database.objectData[selectedObjectIndex].id, index);
        previewSystem.UpdatePostition(gird.CellToWorld(gridPosition) + new Vector3(gird.cellSize.x / 2f, 0, gird.cellSize.z / 2f), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
        return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectedObjectIndex].size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePostition(gird.CellToWorld(gridPosition) + new Vector3(gird.cellSize.x / 2f, 0, gird.cellSize.z / 2f), placementValidity);
    }
}
