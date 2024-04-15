using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// pathfinding 시 사용하는 노드 클래스.
/// </summary>
public class Node
{
    public int gridX;
    public int gridY;

    public bool iswall;
    public Vector3 position;

    public Node Parent;

    public int g_cost;
    public int h_cost;

    public int f_cost { get { return g_cost + h_cost; } }

    public Node(bool _is_wall, Vector3 _position, int _gridX, int _gridY)
    {
        iswall = _is_wall;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

}
