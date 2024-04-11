using System.Collections.ObjectModel;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class PlacementSystem : MonoBehaviour
{
    [SerializeField] public InputManager inputManager;
    [SerializeField] public Grid grid;

    [SerializeField] ObjectsDatabaseSO database;

    [SerializeField] GameObject gridVisualization;

    public GridData floorData, furnitureData;

    [SerializeField] public PreviewSystem preview;

    [SerializeField] public ObjectPlacer objectPlacer;

    private Vector3Int lastDetectedPostition = Vector3Int.zero;

    public IBuildingState buildingState;

    [SerializeField] List<GameObject> hidden_area_cover_list;
    public HousingInfo_JGD housing_info;
    private string now_room;
    [SerializeField] private ParticleSystem level_up_particle;
    private void Awake()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();

       
    }

    private void Start()
    {
        /*for (int i =0; i < 4; i++) {
            place_structure_init(housing_itemID.none, new Vector3Int(0,0,2*i) );
        }*/
    }

    public void init_house(string room_id_) {
        Debug.Log($"init house {room_id_}");

        int temp_level = 0;
        if (housing_info != null) { 
            temp_level = housing_info.level;
        }

        housing_info = BackendGameData_JGD.Instance.get_data_by_nickname(room_id_);
        if (housing_info == null) {
            Debug.Log("no housing information!");
            return;
        }
        update_placement();

        if (now_room == room_id_)
        {
            if (temp_level < housing_info.level) {
                show_level_up_particle();
            }
        }
        now_room = room_id_;
    }

    public void show_level_up_particle() {
        level_up_particle.Play();
    }

    public void level_up(int delta = 1) {
        if (TCP_Client_Manager.instance.now_room_id == TCP_Client_Manager.instance.my_player.object_id) {



            BackendGameData_JGD.userData.housing_Info.level += delta;
            string[] select = { "Housing_Info" }; 
            BackendGameData_JGD.Instance.GameDataUpdate(select);
            housing_info = BackendGameData_JGD.userData.housing_Info;
            update_placement();
            TCP_Client_Manager.instance.send_update_request();
        }        
    }

    public void update_placement() {
        clear();
        update_hidden_area(housing_info.level);
        for (int i = 0; i < housing_info.objectInfos.Count; i++)
        {
            place_structure_init(housing_info.objectInfos[i]); //housing_info.objectInfos[i].item_ID, housing_info.objectInfos[i].position);
        }
    }

    public void update_hidden_area(int level) {
        for (int i =0; i < hidden_area_cover_list.Count; i++) {
            if (i < level)
            {
                hidden_area_cover_list[i].SetActive(false);
            }
            else {
                hidden_area_cover_list[i].SetActive(true);
            }
        }
    }

    public void clear() {
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer,is_init: true);
        List<Vector3Int> position_list = new List<Vector3Int>();
        foreach (Vector3Int position_ in furnitureData.placedObjects.Keys) {
            position_list.Add(position_);
        }
        for (int i = 0; i < position_list.Count; i++) {
            buildingState.OnAction(position_list[i]);
        }
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }
    public void return_all()
    {
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer, is_init: false);
        List<Vector3Int> position_list = new List<Vector3Int>();
        foreach (Vector3Int position_ in furnitureData.placedObjects.Keys)
        {
            position_list.Add(position_);
        }
        for (int i = 0; i < position_list.Count; i++)
        {
            if (furnitureData.placedObjects.ContainsKey(position_list[i])
                && furnitureData.placedObjects[position_list[i]].ID != housing_itemID.star_nest
                && furnitureData.placedObjects[position_list[i]].ID != housing_itemID.post_box) {
                buildingState.OnAction(position_list[i]);
            }
        }
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }

    //button click
    public void save_edit(bool is_mine = true) {
        
        housing_info = new HousingInfo_JGD(grid, furnitureData.placedObjects, objectPlacer, housing_info.level);
        if (is_mine)
        {           
            BackendGameData_JGD.userData.housing_Info = housing_info;
            BackendGameData_JGD.Instance.GameDataUpdate();
        }
        else {
            UserData ud = new UserData();
            ud.housing_Info = housing_info;
            string[] select = { "Housing_Info" };
            BackendGameData_JGD.Instance.update_userdata_by_nickname(TCP_Client_Manager.instance.now_room_id, select,ud);
        }
        
        TCP_Client_Manager.instance.send_update_request();
        
    }

    public void place_structure_init(HousingObjectInfo hoi)
    {
        
        buildingState = new PlacementState((housing_itemID)hoi.item_ID, grid, preview, database, floorData, furnitureData, objectPlacer, hoi.start_time, hoi.direction, hoi.harvesting_selection, is_init:true);
        Vector3Int gridPosition = new Vector3Int((int)hoi.position.x , 0, (int)hoi.position.y);
        buildingState.OnAction(gridPosition);
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }

    public void place_structure_immediatly(int id, Vector3Int pos)
    {
        StopPlacement();
        buildingState = new PlacementState((housing_itemID)id, grid, preview, database, floorData, furnitureData, objectPlacer, DateTime.MaxValue);
        buildingState.OnAction(pos);
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }
    /*public void place_structure_init(housing_itemID id, Vector3Int gridPosition) {
        buildingState = new PlacementState((housing_itemID)id, grid, preview, database, floorData, furnitureData, objectPlacer);
        buildingState.OnAction(gridPosition);
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
    }*/

    //button event
    public void StartPlacement(int id)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState((housing_itemID) id, grid, preview, database, floorData, furnitureData, objectPlacer, DateTime.MaxValue);

        inputManager.Onclicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;

    }
    public Vector3Int remove(Net_Housing_Object ob)
    {
        return remove(ob.transform.position);
    }
    public Vector3Int remove(Vector3 position_)
    {
        Vector3Int grid_pos = grid.WorldToCell(position_);
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer, is_init: false);
        position_.y = 0;
        buildingState.OnAction(grid_pos);
        buildingState.EndState();
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
        return grid_pos;
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
        if (InputManager.IsPointerOverUI())
        {
            Debug.Log("cancel placement");
            cancel_placement();
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedPosition();


        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        //Debug.Log($"mousePosition: {mousePosition} /gridPosition: {gridPosition} ");

        if (buildingState != null) {
            TCP_Client_Manager.instance.housing_ui_manager.click_up_move_btn(buildingState.OnAction(gridPosition));
        }
        
    }

    public bool cancel_placement() {
                
        if (buildingState == null)
        {
            TCP_Client_Manager.instance.housing_ui_manager.is_move = false;
            TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
            return false;
        }
        buildingState.EndState();
        inputManager.Onclicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPostition = Vector3Int.zero;
        buildingState = null;
        TCP_Client_Manager.instance.housing_ui_manager.click_up_move_btn(false);
        TCP_Client_Manager.instance.housing_ui_manager.hide_edit_UI();
        return true;
    }

    public void StopPlacement()
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

        Vector3 mousePosition = inputManager.GetSelectedPosition();
        //Debug.Log(mousePosition.y);
        if (mousePosition.y < 10000) {
            
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            
            if (lastDetectedPostition != gridPosition)
            {
                buildingState.UpdateState(gridPosition);
                lastDetectedPostition = gridPosition;
            }
        }
        
    }


    public Vector3 get_spawn_point(bool is_my_planet) {
        housing_itemID id_ = housing_itemID.star_nest;
        if (is_my_planet) {
            id_ = housing_itemID.star_nest;
        }
        PlacementData pd = null;
        foreach (Vector3Int pos in furnitureData.placedObjects.Keys) {
            pd = furnitureData.placedObjects[pos];
            if (pd.ID == id_) {
                break;
            }
            pd = null;
        }
        if (pd == null) {
            if (is_my_planet)
            {

                return new Vector3(-5, 0.5f, -5);
            }
            else
            {
                return Vector3.forward * -5f + Vector3.up * 0.5f;
            }
        }    
        
        return grid.CellToWorld(pd.occupiedPostitions[0]) + Vector3.up * 0.5f - Quaternion.Euler(0, pd.direction*90f,0)* Vector3.forward;
    }
}
