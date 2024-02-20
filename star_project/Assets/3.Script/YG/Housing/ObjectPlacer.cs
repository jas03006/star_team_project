using System.Collections.Generic;
using UnityEditor.Rendering;
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
    public int PlaceObject(GameObject prefab, Vector3 position, PlacementState ps)
    {
        //오브젝트 설치
        GameObject newobject = Instantiate(prefab);

        if (ps.id==housing_itemID.ark_cylinder) {
            Harvesting hob = newobject.GetComponent<Harvesting>();
            if (hob != null)
            {
                hob.init(ps.start_time, ps.selection);
            }
        }        

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

    public Harvesting find_harvest_object(int index) {
        Harvesting hob = null;
       // for (int i =0; i < placedGameObject.Count; i++) {
            hob = placedGameObject[index].GetComponentInChildren<Harvesting>();
            if (hob != null) {
                return hob;
            }
      //  }
        return null;
    }
}
