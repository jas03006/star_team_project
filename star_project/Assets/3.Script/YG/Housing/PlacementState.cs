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
    public bool is_init;
    public PlacementState(housing_itemID id, Grid gird, PreviewSystem previewSystem, ObjectsDatabaseSO database, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer
        , DateTime start_time, int direction =0, int selection=-1, bool is_init =false)
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
        this.is_init = is_init;

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

    public bool OnAction(Vector3Int gridPosition)
    {
        if (!is_init && !TCP_Client_Manager.instance.housing_ui_manager.can_use(id))
        {
            TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
            return false;
        }

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
            return false;
        }

        int index = objectPlacer.PlaceObject(database.objectData[selectedObjectIndex].prefab, gird.CellToWorld(gridPosition) + new Vector3(gird.cellSize.x / 2f, 0, gird.cellSize.z / 2f), this);

        GridData selectedData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
        selectedData.AddObjectAt(gridPosition, database.objectData[selectedObjectIndex].size, database.objectData[selectedObjectIndex].id, index);
        previewSystem.UpdatePostition(gird.CellToWorld(gridPosition) + new Vector3(gird.cellSize.x / 2f, 0, gird.cellSize.z / 2f), false);

        if (Tutorial_TG.instance.get_type() == tutorial_type_TG.housing) {
            Tutorial_TG.instance.check_housing_condition();
        }
        return true;
    }

    public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
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
