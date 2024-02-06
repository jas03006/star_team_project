using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GridSystem grid;
    public Transform StartPosition;
    public Transform TargetPosition;

    private void Awake()
    {
        grid = GetComponent<GridSystem>();
    }

    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    private void FindPath(Vector3 a_startPos, Vector3 a_targetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_startPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_targetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> CloseList = new HashSet<Node>(); //hashset = 중복비허용/순서보장X인 List. List보다 빠르다고함.

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].f_cost < CurrentNode.f_cost || OpenList[i].f_cost == CurrentNode.f_cost && OpenList[i].h_cost < CurrentNode.h_cost)
                {
                    CurrentNode = OpenList[i];
                }
            }

            OpenList.Remove(CurrentNode);
            CloseList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
                return;
            }

            foreach (Node neighbornode in grid.GetNeighboringNodes(CurrentNode))
            {
                if (!neighbornode.iswall || CloseList.Contains(neighbornode))
                {
                    continue;
                }

                int movecost = CurrentNode.g_cost + GetManhattenDistance(CurrentNode, neighbornode);

                if (movecost < neighbornode.g_cost || !OpenList.Contains(neighbornode))
                {
                    neighbornode.g_cost = movecost;
                    neighbornode.h_cost = GetManhattenDistance(neighbornode, TargetNode);
                    neighbornode.Parent = CurrentNode;

                    if (!OpenList.Contains(neighbornode))
                    {
                        OpenList.Add(neighbornode);
                    }
                }
            }
        }
    }

    private int GetManhattenDistance(Node currentNode, Node neighbornode)
    {
        int ix = Mathf.Abs(currentNode.gridX - neighbornode.gridX);
        int iy = Mathf.Abs(currentNode.gridY - neighbornode.gridY);
        int min = Math.Min(ix, iy);
        int Max = Math.Max(ix, iy);
        return min * 14 +(Max-min)*10;
    }

    private void GetFinalPath(Node a_startNode, Node a_endNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_endNode;

        while (CurrentNode != a_startNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Reverse();
        grid.finalPath = FinalPath;
    }
}
