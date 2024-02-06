using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Grid grid;

    [SerializeField] ObjectsDatabaseSO database;

    [SerializeField] GameObject gridVisualization;

    private GridData floorData, furnitureData;

    [SerializeField] private PreviewSystem preview;

    [SerializeField] ObjectPlacer objectPlacer;

    private Vector3Int lastDetectedPostition = Vector3Int.zero;

    IBuildingState buildingState;

    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int id)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(id, grid, preview, database, floorData, furnitureData, objectPlacer);

        inputManager.Onclicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer);

        inputManager.Onclicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        Debug.Log($"mousePosition: {mousePosition} /gridPosition: {gridPosition} ");

        buildingState.OnAction(gridPosition);
    }

    //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    //{
    //    GridData selectData = database.objectData[selectedObjectIndex].id == 0 ? floorData : furnitureData;
    //    return selectData.CanPlaceObjectAt(gridPosition, database.objectData[selectedObjectIndex].size);
    //}

    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.Onclicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null) //선택된 오브젝트 있으면 return
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPostition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPostition = gridPosition;
        }
    }

}
