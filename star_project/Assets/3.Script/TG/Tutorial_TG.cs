using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public enum tutorial_type_TG { 
    none=-1,
    any_touch=0,
    particular_touch,
    timeout,
    housing,
    move,
    housing_object_touch,
    housing_inven_touch
}
public class Tutorial_TG : MonoBehaviour
{
    public static Tutorial_TG instance = null;
    private int now_index =0;
    public List<Tutorial_Screen_Object> tutorial_sequence;
    //public bool is_housing_tutorial = false;
    public float blink_delay = 0.5f;
    public bool is_progressing = false;
    // public bool is_move_tutorial = false;

    private float timer = 0f;
    public float time_delay = 0.1f;

    [SerializeField] private GameObject reward_UI;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject panel;
    
    [SerializeField] private Camera_My_Planet camera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { 
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
    }
    private void Start()
    {
        Debug.Log("Tuto Check");
        if (BackendGameData_JGD.userData.tutorial_Info.state == Tutorial_state.myplanet)
        {

            start_tutorial();
        }
        else {
            panel.SetActive(false);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void start_tutorial() {
        container.SetActive(true);
        panel.SetActive(true);
        is_progressing = true;
        now_index = 0;
        show();

        camera.set_tutorial_position();
        BackendGameData_JGD.userData.housing_Info = new HousingInfo_JGD();
        BackendGameData_JGD.userData.housing_Info.Add_object(new HousingObjectInfo(housing_itemID.star_nest, new Vector2(-1,-1),0));
        
        TCP_Client_Manager.instance.placement_system.housing_info = BackendGameData_JGD.userData.housing_Info;
        TCP_Client_Manager.instance.placement_system.update_placement();

        BackendGameData_JGD.userData.house_inventory.Remove(housing_itemID.ark_cylinder);
        BackendGameData_JGD.userData.house_inventory.Remove(housing_itemID.post_box);
        TCP_Client_Manager.instance.housing_ui_manager.init_housing_inventory(true);
    }

    public void end_tutorial() {
        is_progressing = false;
        now_index = 0;
        BackendGameData_JGD.userData.Adjective_ID_List.Add(adjective.Cute);
        BackendGameData_JGD.userData.Noun_ID_List.Add(noun.Beginner);
        //TODO: 받은 칭호 DB에 저장 (상위 매니저가 처리해도됨)
        reward_UI.SetActive(true);
        container.SetActive(false);
        panel.SetActive(false);
        BackendGameData_JGD.userData.tutorial_Info.state = Tutorial_state.clear;
        BackendGameData_JGD.Instance.GameDataUpdate();
    }
    public void step() {
        if (timer < time_delay) {
            return;
        }
        timer = 0f;

        now_index++;
        show();
    }

    public void force_step() {
        now_index++;
        show();
    }

    public void show() {

        
        if (now_index > 0) {
            tutorial_sequence[now_index - 1].StopAllCoroutines();
            panel.SetActive(true);
            StartCoroutine(active_co(now_index - 1, false));
            //tutorial_sequence[now_index - 1].gameObject.SetActive(false);
        }
        if (now_index >= tutorial_sequence.Count)
        {
            end_tutorial();
            return;
        }else if (now_index >= 0)
        {
            StartCoroutine(active_co(now_index, true));
            //tutorial_sequence[now_index].gameObject.SetActive(true);
            //tutorial_sequence[now_index].start_process();
        }            
    }
    public IEnumerator active_co(int index_, bool is_on) {
        yield return null;
        yield return null;
        yield return null;
        tutorial_sequence[index_].gameObject.SetActive(is_on);
        if (is_on) {
            tutorial_sequence[index_].start_process();
            yield return null; 
            yield return null;
            yield return null;
            panel.SetActive(false);
        }
    }

    public tutorial_type_TG get_type() {
        if (!is_progressing || now_index < 0 || now_index >= tutorial_sequence.Count) {
            return tutorial_type_TG.none;
        }
        return tutorial_sequence[now_index].type;
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
    public void check_housing_object_touch(housing_itemID id_) {
        if (id_ == tutorial_sequence[now_index].target) {
            step();
        }
    }
    public void check_housing_inven_touch(housing_itemID id_)
    {
        if (id_ == tutorial_sequence[now_index].target)
        {
            step();
        }
    }
}
