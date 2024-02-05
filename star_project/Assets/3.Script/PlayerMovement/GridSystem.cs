using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius; //노드 반지름
    public float distance;

    Node[,] grid;

    public List<Node> finalPath;

    float nodeDiameter; //노드 지름
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGird();
    }

    public Node NodeFromWorldPosition(Vector3 a_worldPosition)
    {
        float x_point = ((a_worldPosition.x * gridWorldSize.x / 2) / gridWorldSize.x);
        float y_point = ((a_worldPosition.z * gridWorldSize.y / 2) / gridWorldSize.y);

        x_point = Mathf.Clamp01(x_point);
        y_point = Mathf.Clamp01(y_point);

        int x = Mathf.RoundToInt((gridSizeX - 1) * x_point);
        int y = Mathf.RoundToInt((gridSizeY - 1) * y_point);

        return grid[x, y];
    }

    private void CreateGird()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall = true;

                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMask)) //원에 충돌하면 
                {
                    wall = false;
                }

                grid[y, x] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.iswall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (finalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
