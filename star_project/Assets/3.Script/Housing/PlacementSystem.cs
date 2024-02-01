using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator; //���콺 ���󰡴� ������Ʈ
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

        if (selectedObjectIndex < 0) //��ã��
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

        //mouseIndicator ��� ������ ������Ʈ Ŀ���� ���̱�
        GameObject newobject = Instantiate(database.objectData[selectedObjectIndex].prefab);
        newobject.transform.position = gird.CellToWorld(gridPosition);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1; //�ʱ�ȭ

        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);

        inputManager.Onclicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0) //���õ� ������Ʈ ������ return
            return;

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = gird.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = gird.CellToWorld(gridPosition);
    }

}
