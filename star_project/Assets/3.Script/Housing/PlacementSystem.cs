using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator; //마우스 따라가는 오브젝트
    [SerializeField] InputManager inputManager;
    [SerializeField] Grid gird;

    [SerializeField] ObjectsDatabaseSO database;
    [SerializeField] int selectedObjectIndex = -1;

    [SerializeField] GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int id)
    {
        StopPlacement();
        selectedObjectIndex = database.objectData.FindIndex(data => data.id == id);

        if (selectedObjectIndex < 0) //못찾음
        {
            Debug.LogError("ID not Found");
            return;
        }

        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);

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
        Vector3Int gridPosition = gird.WorldToCell(mousePosition);

        //mouseIndicator 대신 생성한 오브젝트 커서에 붙이기
        GameObject newobject = Instantiate(database.objectData[selectedObjectIndex].prefab);
        newobject.transform.position = gird.CellToWorld(gridPosition);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1; //초기화

        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);

        inputManager.Onclicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0) //선택된 오브젝트 있으면 return
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = gird.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = gird.CellToWorld(gridPosition);
    }

}
