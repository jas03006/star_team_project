using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//�� Ʃ�丮�� �ܰ� �ϳ��� ������ ��� �ִ� Ŭ����
public class Tutorial_Screen_Object : MonoBehaviour
{
    public tutorial_type_TG type;
    public bool is_finger_move = false; //�հ��� ǥ�� ������ ����
    public bool is_giving_objects = false; //������Ʈ ���� ����
    private float delay_time = 0.7f; //��ġ �Է� ���� �ð�
    public float timeout_time = 0.7f; //Ÿ�Ӿƿ� �� �����ܰ�� �Ѿ�� �ð�
    public housing_itemID target = housing_itemID.none; //�Ͽ�¡ ������Ʈ ��ġ Ʃ�丮�󿡼� Ÿ���� �Ǵ� ������Ʈid
    public GameObject target_go = null; // ��Ŀ�� �ؾ��ϴ� Ÿ�� ���� ������Ʈ


    public Button screen; // any touch���� ����� ���� ��ũ�� (��ġ �� ���� �ܰ��)
    public GameObject press_screen_UI; // ȭ���� Ŭ���ϼ���
    public GameObject finger_UI; //�հ��� �׸�
   
    public Transform start_pos; //�հ��� ���� ��ġ
    public Transform end_pos; //�հ��� �̵� �� ��ġ
    private Vector3 dir; //�հ��� �̵� ���� (start pos �� end pos�� ���)


    public Button target_button; // Ÿ���� �Ǵ� ��ư (Ŭ���� ���� �ܰ��)
    private UnityAction step_action; // Ÿ�� Ŭ�� �� �׼� (add listener�� �������� �Ҵ�)

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

    //Ʃ�丮�� Ÿ�Կ� ���� ó��
    public IEnumerator process() {


        switch (type)
        {
            case tutorial_type_TG.any_touch:
                screen.GetComponent<Image>().alphaHitTestMinimumThreshold = 0f; // ��� ��ġ �� ����
               
                yield return new WaitForSeconds(delay_time);
                StartCoroutine(blink_press_UI());
                step_action = () => { AudioManager.instance.SFX_Click(); Tutorial_TG.instance.step(); screen?.onClick.RemoveListener(step_action); };
                screen.onClick.AddListener(step_action);
                break;
            case tutorial_type_TG.particular_touch:
                //yield return new WaitForSeconds(delay_time);
                step_action = () => { AudioManager.instance.SFX_Click(); Tutorial_TG.instance.step(); target_button?.onClick.RemoveListener(step_action); };
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

    // Ÿ���� ��Ŀ�� (Ÿ���� ��ġ�� �������� Ư�� �ܰ迡���� ���)
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