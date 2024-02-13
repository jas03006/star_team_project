using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedGameObject = new();
    GridSystem grid;
    [SerializeField] LayerMask layer;

    private void Awake()
    {
        grid = FindObjectOfType<GridSystem>();
    }
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        //오브젝트 설치
        GameObject newobject = Instantiate(prefab);
        newobject.tag = "HousingObject";
        newobject.transform.position = position;
        placedGameObject.Add(newobject);

        grid.Invoke("CreateGird", 0.05f);

        return placedGameObject.Count - 1;
    }

    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObject.Count <= gameObjectIndex || placedGameObject[gameObjectIndex] == null)
            return;

        Destroy(placedGameObject[gameObjectIndex]);

        grid.Invoke("CreateGird", 0.05f);

        placedGameObject[gameObjectIndex] = null;
    }
}
