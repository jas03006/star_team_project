using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public enum tutorial_type_TG { 
    any_touch=0,
    particular_touch,
    timeout,
    housing,
    move
}
public class Tutorial_TG : MonoBehaviour
{
    public static Tutorial_TG instance = null;
    private int now_index =0;
    public List<Tutorial_Screen_Object> tutorial_sequence;
    public bool is_housing_tutorial = false;
    public float blink_delay = 0.5f;
    public bool is_progressing = false;
    public bool is_move_tutorial = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { 
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        start_tutorial();
    }
    public void start_tutorial() {
        is_progressing = true;
        now_index = 0;
        show();
    }

    public void step() {
        now_index++;
        is_move_tutorial = false;
        is_housing_tutorial = false;
        show();
    }

    public void show() {
        if (now_index > 0) {
            tutorial_sequence[now_index - 1].StopAllCoroutines();
            tutorial_sequence[now_index - 1].gameObject.SetActive(false);
        }
        if (now_index < tutorial_sequence.Count)
        {
            tutorial_sequence[now_index].gameObject.SetActive(true);
            tutorial_sequence[now_index].start_process();
        }
        else {
            is_progressing = false;
            //Á¾·á
        }              
    }
    public void check_housing_condition() {

        bool has_star_nest = false;
        bool has_post_box = false;
        bool has_ark_cylinder = false;

        ObjectPlacer objectPlacer = TCP_Client_Manager.instance.placement_system.objectPlacer;
        Dictionary<Vector3Int, PlacementData> placement_info = TCP_Client_Manager.instance.placement_system.furnitureData.placedObjects;
        Grid grid = TCP_Client_Manager.instance.placement_system.grid;

        for (int i = 0; i < objectPlacer.placedGameObject.Count; i++)
        {
            GameObject go = objectPlacer.placedGameObject[i];
            if (go == null)
            {
                continue;
            }
            Net_Housing_Object nho = go.GetComponentInChildren<Net_Housing_Object>();
            Vector3Int pos = grid.WorldToCell(go.transform.position);
            HousingObjectInfo hoi = new HousingObjectInfo(nho.object_enum, new Vector2(pos.x, pos.z), placement_info[pos].direction);

            if (nho.object_enum == housing_itemID.ark_cylinder)
            {
                has_ark_cylinder = true;
            } else if (nho.object_enum == housing_itemID.post_box) {
                has_post_box = true;
            }
            else if (nho.object_enum == housing_itemID.star_nest)
            {
                has_star_nest = true;
            }
        }

        if (has_star_nest && has_post_box && has_ark_cylinder) {
            step();
        }
    }
}
