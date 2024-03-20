using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Camera_My_Planet : MonoBehaviour
{
    public Vector3 angle = new Vector3(70f, 45f, 0f);
    
    public float dragSpeed = 1f;
    
    public float zoom_speed = 0.03f;
    public float distance = 10f;
    public float min_distance = 3f;
    public float max_distance = 20f;

    public Vector3 center_pos;
    public float max_distance_x = 18f;
    public float max_distance_y = 15f;

    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();        
    }

    public void init()
    {        
        transform.position = TCP_Client_Manager.instance.my_player.transform.position - transform.forward * distance*3f;
        center_pos = -transform.forward * distance * 3f;
    }

    public void set_tutorial_position() {
        transform.position = center_pos;
    }
    private Vector2 nowPos, prePos;
    private Vector3 movePos;
    private bool isoverUI=false;
    private void FixedUpdate()
    {
        isoverUI = InputManager.IsPointerOverUI();
        if (Tutorial_TG.instance.is_progressing) {
            transform.position = center_pos;
            return;
        }
        //if (!can_move_camera())
      //  {
        if (Input.mouseScrollDelta.y != 0)
        {
            distance -= Input.mouseScrollDelta.y * 1.2f;
            if (distance < min_distance)
            {
                distance = min_distance;
            }
            else if (distance > max_distance)
            {
                distance = max_distance;
            }
            camera.orthographicSize = distance;
        }
            
           // transform.position = TCP_Client_Manager.instance.my_player.transform.position - transform.forward*30f; // *distance;
       // }

        if (Input.touchCount == 2 && !isoverUI) //손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장함
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (deltaMagnitudeDiff != 0)
            {
                distance += deltaMagnitudeDiff * zoom_speed;
                if (distance < min_distance)
                {
                    distance = min_distance;
                }
                else if (distance > max_distance)
                {
                    distance = max_distance;
                }
            }

            // 만약 카메라가 OrthoGraphic모드 라면
            if (camera.orthographic)
            {
                camera.orthographicSize = distance;
            }
            else
            {
                camera.fieldOfView += deltaMagnitudeDiff * 5f;
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
        //TODO: 화면 드래그 시 화면 옮기기
        if (Input.touchCount == 1)
        {
            if (!TCP_Client_Manager.instance.placement_system.inputManager.is_moving && TCP_Client_Manager.instance.placement_system.buildingState == null && !isoverUI)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    prePos = touch.position - touch.deltaPosition;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    nowPos = touch.position - touch.deltaPosition;
                    movePos = (Vector3)(prePos - nowPos) * Time.fixedDeltaTime * dragSpeed;
                    movePos.z = 0;
                    //camera.transform.Translate(movePos);
                    adjust_cam_pos(movePos);
                    prePos = nowPos;
                }
            }            
        }
    }

    private void adjust_cam_pos(Vector3 transition) {
        Vector3 delta_pos = (camera.transform.position + transition.x* camera.transform.right  + transition.y * camera.transform.up - center_pos);
        float delta_x =Vector3.Dot(delta_pos, camera.transform.right);
        float delta_y =  Vector3.Dot(delta_pos, camera.transform.up);
        delta_pos = Vector3.zero;
        if (Mathf.Abs( delta_x) <= max_distance_x) {
            delta_pos += camera.transform.right * transition.x;
            //camera.transform.Translate(camera.transform.right * transition.x);
        }
        if (Mathf.Abs(delta_y) <= max_distance_y) {
            delta_pos += camera.transform.up * transition.y;
            //camera.transform.Translate(camera.transform.up * transition.y);
        }
        camera.transform.position += delta_pos;
    }
    

    private bool can_move_camera() {
        if (!TCP_Client_Manager.instance.housing_ui_manager.is_edit_mode)
        {
            return false;    
        }
        return true;
    }
}
