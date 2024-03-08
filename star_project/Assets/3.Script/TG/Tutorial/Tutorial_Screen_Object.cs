using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial_Screen_Object : MonoBehaviour
{

    public tutorial_type_TG type;
    public bool is_finger_move = false;
    public float delay_time = 3f;
    public housing_itemID target = housing_itemID.none; 


    public Button screen;
    public GameObject press_screen_UI;
    public GameObject finger_UI;

    public Transform start_pos;
    public Transform end_pos;
    private Vector3 dir;

    public Button target_button;
    private UnityAction step_action;
    // Start is called before the first frame update
    void Start()
    {
        step_action = () => { AudioManager.instance.SFX_Click(); Tutorial_TG.instance.step(); target_button?.onClick.RemoveListener(step_action); };
        press_screen_UI.SetActive(false);
        
        screen.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        
        if (target != 0)
        {
            find_target();
        }

        if (is_finger_move) {
            finger_UI.transform.position = start_pos.position;
            dir = end_pos.position - start_pos.position;
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


    public IEnumerator process() {


        switch (type)
        {
            case tutorial_type_TG.any_touch:
                screen.GetComponent<Image>().alphaHitTestMinimumThreshold = 0f; // 모든 터치 다 막기
                yield return new WaitForSeconds(delay_time);
                StartCoroutine(blink_press_UI());
                screen.onClick.AddListener(step_action);
                break;
            case tutorial_type_TG.particular_touch:
                yield return new WaitForSeconds(delay_time);
                target_button?.onClick.AddListener(step_action);
                break;
            case tutorial_type_TG.timeout:
                yield return new WaitForSeconds(delay_time);
                Tutorial_TG.instance.step();
                break;
            case tutorial_type_TG.housing:
                Tutorial_TG.instance.is_housing_tutorial = true;
                break;
            case tutorial_type_TG.move:
                Tutorial_TG.instance.is_move_tutorial = true;
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

        screen.transform.position = Camera.main.WorldToScreenPoint(target_go.transform.position);
    }
}