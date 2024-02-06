using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask hitLayers;
    public List<Node> finalPath;

    public PathFinding pathFinding;

    private void Start()
    {
        pathFinding = FindObjectOfType<PathFinding>();
    }
    private void Update()
    {
        SetStartandTargetPos();
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

    private void MovePlayer()
    {
        //transform.DOPath()
    }
}
