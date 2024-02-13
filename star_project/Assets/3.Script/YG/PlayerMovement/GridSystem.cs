using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    //public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius; //노드 반지름
    public float distance;
    Vector3 bottomLeft;
    Node[,] grid_node;
    Grid grid;

    GridData furnitureData;

    public List<Node> finalPath; //최종으로 찾은 길

    float nodeDiameter; //노드 지름
    int gridSizeX, gridSizeY;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGird();
    }

    public Node NodeFromWorldPosition(Vector3 a_worldPosition)
    {
        float x_point = ((a_worldPosition.x - bottomLeft.x) / gridWorldSize.x);
        float y_point = ((a_worldPosition.z - bottomLeft.z) / gridWorldSize.y);

        x_point = Mathf.Clamp01(x_point);
        y_point = Mathf.Clamp01(y_point);

        int x = Mathf.RoundToInt((gridSizeX - 1) * x_point);
        int y = Mathf.RoundToInt((gridSizeY - 1) * y_point);

        return grid_node[x, y];
    }

    public List<Node> GetNeighboringNodes(Node a_node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right Side
        xCheck = a_node.gridX + 1;
        yCheck = a_node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid_node[xCheck, yCheck]);
            }
        }

        //Left Side
        xCheck = a_node.gridX - 1;
        yCheck = a_node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid_node[xCheck, yCheck]);
            }
        }

        //Top Side
        xCheck = a_node.gridX;
        yCheck = a_node.gridY + 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid_node[xCheck, yCheck]);
            }
        }

        //Bottom Side
        xCheck = a_node.gridX;
        yCheck = a_node.gridY - 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid_node[xCheck, yCheck]);
            }
        }

        ///////////////////////////////////////
        xCheck = a_node.gridX - 1;
        yCheck = a_node.gridY - 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                if (grid_node[xCheck, a_node.gridY].iswall && grid_node[a_node.gridX, yCheck].iswall)
                {
                    NeighboringNodes.Add(grid_node[xCheck, yCheck]);
                }

            }
        }

        xCheck = a_node.gridX + 1;
        yCheck = a_node.gridY - 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                if (grid_node[xCheck, a_node.gridY].iswall && grid_node[a_node.gridX, yCheck].iswall)
                {
                    NeighboringNodes.Add(grid_node[xCheck, yCheck]);
                }
            }
        }
        xCheck = a_node.gridX - 1;
        yCheck = a_node.gridY + 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                if (grid_node[xCheck, a_node.gridY].iswall && grid_node[a_node.gridX, yCheck].iswall)
                {
                    NeighboringNodes.Add(grid_node[xCheck, yCheck]);
                }
            }
        }
        xCheck = a_node.gridX + 1;
        yCheck = a_node.gridY + 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                if (grid_node[xCheck, a_node.gridY].iswall && grid_node[a_node.gridX, yCheck].iswall)
                {
                    NeighboringNodes.Add(grid_node[xCheck, yCheck]);
                }
            }
        }

        return NeighboringNodes;
    }

    public void CreateGird()
    {
        if (furnitureData == null)
        {
            furnitureData = FindObjectOfType<PlacementSystem>().furnitureData;
        }

        grid_node = new Node[gridSizeX, gridSizeY];
        bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool wall = true;

                Vector3Int worldPointToCell = grid.WorldToCell(worldPoint);
                Vector3Int worldPointInt = Vector3Int.FloorToInt(worldPoint);

                
                if (Physics.CheckSphere(worldPoint, nodeRadius, wallMask) || (furnitureData!= null && !furnitureData.CanPlaceObjectAt(worldPointToCell, Vector2Int.one))) //원에 충돌하면 
                {
                    if (furnitureData != null) {
                    }
                    wall = false;
                }

                grid_node[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid_node != null)
        {
            foreach (Node node in grid_node)
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
                    if (finalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
