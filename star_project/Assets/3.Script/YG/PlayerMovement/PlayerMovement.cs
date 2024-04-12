using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : Player_Network_TG
{
    public LayerMask hitLayers;
    public List<Node> finalPath;

    public Transform player;
    public Transform player_container;

    public PathFinding pathFinding;
    public GridSystem grid;

    private SpecialObjManager specialObjManager;

    public RectTransform name_tag_root;
    public TMP_Text title_tag;
    public TMP_Text name_tag;

    [SerializeField] private GameObject emozi_box;
    [SerializeField] private Image emozi_image;
    private Coroutine now_emozi_co=null;
    
    Tween now_tween = null;

    public int character=-1;
    [SerializeField] private MeshRenderer renderer_;
    private void OnEnable()
    {
        find_grid();
    }

    public float drag_timer = 0f;
    private float drag_time_threshold = 0.17f;

    public override void load()
    {
        base.load();
        name_tag.text = object_id;
        update_title();
        update_model();
    }
    public void update_title() {
        if (!is_guest)
        {
            title_tag.text = (BackendGameData_JGD.userData.title_adjective == adjective.none ? "" : BackendGameData_JGD.userData.title_adjective.ToString())
            + " " + (BackendGameData_JGD.userData.title_noun == noun.none ? "" : BackendGameData_JGD.userData.title_noun.ToString());
            character = BackendGameData_JGD.userData.character;
            //TODO: 선택 캐릭터 적용

        }
        else
        {
            string[] select = { "title_adjective", "title_noun", "character" };
            UserData ud = BackendGameData_JGD.Instance.get_userdata_by_nickname(object_id, select);
            title_tag.text = (ud.title_adjective == adjective.none ? "" : ud.title_adjective.ToString())
            + " " + (ud.title_noun == noun.none ? "" : ud.title_noun.ToString());
            character = ud.character;
        }
    }

    public void update_model() {
        renderer_.material = SpriteManager.instance.Num2Material(character);
    }
    public void update_model(int char_id)
    {
        character = char_id;
        renderer_.material = SpriteManager.instance.Num2Material(character);
    }

    private void FixedUpdate()
    {
        if (TCP_Client_Manager.instance.now_room_id != "-")
        {
            name_tag_root.position = Camera.main.WorldToScreenPoint(player.position) + Vector3.up * (10f + 450f / Camera.main.orthographicSize);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       

        if (!is_guest && TCP_Client_Manager.instance.now_room_id != "-")
        {
            if (Input.touchCount >= 2)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            Debug.DrawRay(ray.origin, ray.direction.normalized * 2000f, Color.red);
            if (Input.GetMouseButtonDown(0))
            {
                drag_timer = 0f;
            }
            if(Input.GetMouseButton(0)) {
                drag_timer += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {// Debug.Log("RightClick!");

                if (drag_timer > drag_time_threshold) {
                    drag_timer = 0f;
                    return;
                }
             //if (!TCP_Client_Manager.instance.placement_system.cancel_placement())
             // {
                if (InputManager.IsPointerOverUI() )//&& !TCP_Client_Manager.instance.placement_system.inputManager.IsPointerOverClickableUI())
                {
                }
                else {
                    //Debug.Log("Move Click");
                    if (can_move())
                      {
                         RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Interact_TG")))
                        {
                           // Debug.Log("Interaction detect!");

                            Vector3 dest = hit.point;
                            dest.y = 0f;

                            dest = grid.find_nearest_space(dest, transform.position);
                            net_move(transform.position, dest);

                            TweenCallback action = () => { hit.collider.gameObject.GetComponentInParent<Net_Housing_Object>().interact(object_id); };
                            move(transform.position, dest, action);
                            AudioManager.instance.SFX_Click();
                        }
                        else if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("Ground_TG") | LayerMask.GetMask("Placement_YG")))
                        {
                            //Debug.Log("check!");
                            Vector3 dest = hit.point;
                            dest.y = transform.position.y;

                            net_move(transform.position, dest);
                            move(transform.position, dest);
                            AudioManager.instance.SFX_Click();
                            if (Tutorial_TG.instance.get_type() == tutorial_type_TG.move) {
                                Tutorial_TG.instance.step();
                            }
                        }
                      }
                }                
            } 
        }

        //SetStartandTargetPos();
/*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            move(pathFinding.StartPosition.position, pathFinding.TargetPosition.position);
        }*/
    }

    public void find_grid() {
        Debug.Log("finding grid system");
        pathFinding = FindObjectOfType<PathFinding>();
        grid = FindObjectOfType<GridSystem>();
    }
    

    private void SetStartandTargetPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;

            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
            {
                // this.transform.position = hit.point;
                pathFinding.StartPosition = transform;
                pathFinding.TargetPosition.position = hit.point;
            }
        }
    }

    public override void move(Vector3 start_pos, Vector3 dest_pos, TweenCallback callback = null)
    {
        //base.move(start_pos, dest_pos);
        //finalPath = 
        finalPath =  pathFinding.FindPath(start_pos,dest_pos);
        
        if (finalPath != null) {
            //Debug.Log("Move!");

            MovePlayer(callback);
        }
    }

    private void MovePlayer(TweenCallback callback = null)
    {
        Vector3[] path_ = Path2MovePath();
        if (path_ != null) {
            float distance = get_path_length(path_);
            float speed = 3f;
            float time_limit = distance / speed;
            if (now_tween != null)
            {
                now_tween.onComplete = null;
                now_tween = null;
                stop_DOTween();
            }

            if (time_limit > 0 ) {
                player.DOLocalJump(Vector3.zero, 1, (int)Mathf.Round(time_limit), time_limit).SetEase(Ease.Linear);
            }
            
            now_tween = player_container.DOPath(path_, time_limit, PathType.Linear).SetLookAt(0.25f).SetEase(Ease.Linear).SetOptions(false);
            TweenCallback action = look_user;
            if (callback != null) {
                action += callback;
            }
            now_tween.OnComplete(action);
        }        
    }
    private float get_path_length(Vector3[] path_) {
        float result = 0f;
        for (int i =0; i < path_.Length-1; i++) {
            result += (path_[i + 1] - path_[i]).magnitude;
        }
        return result;
    }
    public void look_user() {
        Tween t = player_container.DOLookAt( transform.position-Camera.main.transform.forward,1f, axisConstraint: AxisConstraint.Y,up: Vector3.up);
    }
    public void stop_DOTween() {
        DOTween.Kill(this);
    }
    private Vector3[] Path2MovePath() {
        List<Vector3> smooth_path = new List<Vector3>();

        float divide_cnt = 5.0f;
        float coeff = 1.0f / (divide_cnt*2f);
        Vector3 pivot0;
        Vector3 pivot1;
        Vector3 pivot2;
        Vector3 div0;
        Vector3 div1;
        pivot0 = transform.position - new Vector3(0, 0.5f, 0);// grid.finalPath[0].position;
        smooth_path.Add(pivot0);
        for (int i =-1; i < finalPath.Count-2; i++) {
            pivot1 = finalPath[i+1].position;
            pivot2 = finalPath[i+2].position;

            for (float j=0f; j <= divide_cnt; j++) {
                div0 = Vector3.Lerp(pivot0, pivot1, coeff*j);
                div1 = Vector3.Lerp(pivot1, pivot2, coeff*j);
                Vector3 new_point = Vector3.Lerp(div0, div1, j * coeff);
                smooth_path.Add(new_point);
                
            }
            pivot0 = smooth_path[smooth_path.Count-1];
        }
        //smooth_path.Add(pivot0);
        if (finalPath.Count >= 1) {
            smooth_path.Add(finalPath[finalPath.Count - 1].position);
        }        

        Vector3[] waypoints = new Vector3[smooth_path.Count];
        for (int i = 0; i < smooth_path.Count; i++)
        {
            waypoints[i] = smooth_path[i] + new Vector3(0, 0.5f, 0);
        }

        return waypoints;

    }

   
    private Vector3[] Path2array()
    {
        Vector3[] waypoints = new Vector3[grid.finalPath.Count];
        
        for (int i = 0; i < grid.finalPath.Count; i++)
        {
            waypoints[i] = grid.finalPath[i].position + new Vector3(0,0.5f,0);
        }

        return waypoints;
    }

    public void show_emozi_net(int id_) {
        show_emozi(id_);
        TCP_Client_Manager.instance.send_emo_request(id_);
    }
    public void show_emozi(int id_) {
        if (now_emozi_co != null)
        {
            StopCoroutine(now_emozi_co);
        }
        now_emozi_co = StartCoroutine(show_emozi_co(id_));
    }
    public IEnumerator show_emozi_co(int id_) {
        emozi_box.SetActive(true);
        emozi_image.sprite = SpriteManager.instance.Num2emozi(id_);
        yield return new WaitForSeconds(3);
        emozi_box.SetActive(false);
        now_emozi_co = null;
    }
}
