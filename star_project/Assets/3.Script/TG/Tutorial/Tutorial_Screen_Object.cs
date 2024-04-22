using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//각 튜토리얼 단계 하나의 정보를 담고 있는 클래스
public class Tutorial_Screen_Object : MonoBehaviour
{
    public tutorial_type_TG type;
    public bool is_finger_move = false; //손가락 표시 움직임 여부
    public bool is_giving_objects = false; //오브젝트 지급 여부
    private float delay_time = 0.7f; //터치 입력 제한 시간
    public float timeout_time = 0.7f; //타임아웃 후 다음단계로 넘어가는 시간
    public housing_itemID target = housing_itemID.none; //하우징 오브젝트 터치 튜토리얼에서 타켓이 되는 오브젝트id
    public GameObject target_go = null; // 포커스 해야하는 타켓 게임 오브젝트


    public Button screen; // any touch에서 사용할 투명 스크린 (터치 시 다음 단계로)
    public GameObject press_screen_UI; // 화면을 클릭하세요
    public GameObject finger_UI; //손가락 그림
   
    public Transform start_pos; //손가락 시작 위치
    public Transform end_pos; //손가락 이동 끝 위치
    private Vector3 dir; //손가락 이동 방향 (start pos 와 end pos로 계산)


    public Button target_button; // 타겟이 되는 버튼 (클릭시 다음 단계로)
    public string target_button_tag = string.Empty; // 타겟이 되는 버튼의 태그 (타겟이 싱글톤일 경우 사용)
    public string goal_object_parent_tag = string.Empty; // 타겟 버튼 클릭 시 active 변경 되어야하는 gomeobject의 부모의 tag ( 선택적, 버튼을 클릭하지 않더라도 해당 object가 active되어 있다면 다음 단계로)
    public bool goal_should_on = true; // 골 오브젝트가 active되어야 하는지 deactive 되어야하는지 (선택)
    private UnityAction step_action; // 타겟 클릭 시 액션 (add listener로 동적으로 할당)

    private void Awake()
    {
        step_action = () => { AudioManager.instance.SFX_Click(); Tutorial_TG.instance.step(); };
    }
    // Start is called before the first frame update
    void Start()
    {        
        press_screen_UI.SetActive(false);

        screen.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        if (type != tutorial_type_TG.housing_inven_touch && target != housing_itemID.none)
        {
            find_target();
        } else if (target_go != null) {
            find_target_go();
        }

        if (is_finger_move) {
            finger_UI.transform.position = start_pos.position;
            dir = end_pos.position - start_pos.position;
        }

        if (is_giving_objects) {
            BackendGameData_JGD.userData.house_inventory.Add(housing_itemID.ark_cylinder, 1);
            BackendGameData_JGD.userData.house_inventory.Add(housing_itemID.post_box, 1);
            TCP_Client_Manager.instance.housing_ui_manager.init_housing_inventory();
        }
    }

    private void Update()
    {
        if (is_finger_move && finger_UI != null)
        {
            if (Vector3.Distance(finger_UI.transform.position, end_pos.position) <= 5f)
            {
                finger_UI.transform.position = start_pos.position;
            }
            else {
                finger_UI.transform.position += dir * Time.deltaTime;
            }
        }

    }
    public void start_process() {
        StartCoroutine(process());
    }
    
    private bool check_goal(GameObject goal_object) {
        if (goal_object_parent_tag != string.Empty)
        {
            if (goal_object != null && goal_object.activeSelf == goal_should_on)
            {
                target_button?.onClick.RemoveListener(step_action);
                Tutorial_TG.instance.step();
                return true;
            }
        }
        return false;
        
    }
    private IEnumerator check_goal_co() {
        GameObject goal_object = GameObject.FindGameObjectWithTag(goal_object_parent_tag)?.transform.GetChild(0)?.gameObject;
        while (goal_object != null && !check_goal(goal_object)) {
            yield return new WaitForSeconds(0.4f);
        }
    }
    //튜토리얼 타입에 따른 처리
    public IEnumerator process() {


        switch (type)
        {
            case tutorial_type_TG.any_touch:
                screen.GetComponent<Image>().alphaHitTestMinimumThreshold = 0f; // 모든 터치 다 막기
               
                yield return new WaitForSeconds(delay_time);
                StartCoroutine(blink_press_UI());
                step_action = () => { AudioManager.instance.SFX_Click(); Tutorial_TG.instance.step(); screen?.onClick.RemoveListener(step_action); };
                screen.onClick.AddListener(step_action);
                break;
            case tutorial_type_TG.particular_touch:
                if (target_button_tag != string.Empty) {
                    target_button = GameObject.FindGameObjectWithTag(target_button_tag)?.GetComponent<Button>();
                }
                if (goal_object_parent_tag != string.Empty)
                {
                    GameObject goal_object = GameObject.FindGameObjectWithTag(goal_object_parent_tag)?.transform.GetChild(0)?.gameObject;
                    if (goal_object != null && goal_object.activeSelf == goal_should_on)
                    {
                        yield return new WaitForSeconds(0.3f);
                        Tutorial_TG.instance.step();
                        break;
                    }
                }
                StartCoroutine(check_goal_co());
                step_action = () => { Tutorial_TG.instance.step(); target_button?.onClick.RemoveListener(step_action); };
                target_button?.onClick.AddListener(step_action);
                break;
            case tutorial_type_TG.timeout:
                yield return new WaitForSeconds(timeout_time);
                Tutorial_TG.instance.force_step();
                break;
            case tutorial_type_TG.housing:
                break;
            case tutorial_type_TG.move:
                break;
            case tutorial_type_TG.housing_object_touch:
                break;
            case tutorial_type_TG.housing_inven_touch:
                break;
            default:
                break;
        }
    }

    public IEnumerator blink_press_UI() {
        while (true) {
            press_screen_UI.SetActive(true);
            yield return new WaitForSeconds(Tutorial_TG.instance.blink_delay);
            press_screen_UI.SetActive(false);
            yield return new WaitForSeconds(Tutorial_TG.instance.blink_delay);
        }
    }

    // 타겟을 포커싱 (타겟의 위치가 가변적인 특정 단계에서만 사용)
    public void find_target() {
        GameObject target_go = gameObject;
        switch (target)
        {
            case housing_itemID.star_nest:
                target_go = GameObject.FindAnyObjectByType<Star_nest>().gameObject;
                break;
            case housing_itemID.post_box:
                target_go = GameObject.FindAnyObjectByType<Post_Box>().gameObject;
                break;
            case housing_itemID.ark_cylinder:
                target_go = GameObject.FindAnyObjectByType<Harvesting>().gameObject;
                break;
            default:
                break;
        }
        Vector3 temp = Camera.main.WorldToScreenPoint(target_go.transform.position) + Vector3.up * 10f;
        temp.z = 0;
        screen.transform.position = temp;

    }
    public void find_target_go() {
        screen.transform.position = target_go.transform.position;
    }
}