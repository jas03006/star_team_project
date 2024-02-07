using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : Player_Network_TG
{
    public LayerMask hitLayers;
    public List<Node> finalPath;

    public Transform player;
    public Transform player_container;

    public PathFinding pathFinding;
    public GridSystem grid;

    private void OnEnable()
    {
        find_grid();
    }
    public void find_grid() {
        Debug.Log("finding grid system");
        pathFinding = FindObjectOfType<PathFinding>();
        grid = FindObjectOfType<GridSystem>();
    }
    protected override void Update()
    {
        base.Update();
        SetStartandTargetPos();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            move(pathFinding.StartPosition.position,pathFinding.TargetPosition.position);
        }
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

    public override void move(Vector3 start_pos, Vector3 dest_pos)
    {
        //base.move(start_pos, dest_pos);
        //finalPath = 
        finalPath =  pathFinding.FindPath(start_pos,dest_pos);
        
        if (finalPath != null) {
            //Debug.Log("Move!");
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3[] path_ = Path2MovePath();
        if (path_ != null) {
            float distance = get_path_length(path_);
            float speed = 3f;
            float time_limit = distance / speed;

            player.DOLocalJump(Vector3.zero, 1, (int)Mathf.Round(time_limit), time_limit).SetEase(Ease.Linear);
            Tween t = player_container.DOPath(path_, time_limit, PathType.Linear).SetLookAt(0.25f).SetEase(Ease.Linear).SetOptions(false);
            TweenCallback action = look_user;
            t.OnComplete(action);
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
        DOTween.KillAll();
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
        smooth_path.Add(finalPath[finalPath.Count - 1].position);

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
}
