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

    //List<Vector3> debug_list = new List<Vector3>();

    private void Start()
    {
        grid = FindObjectOfType<Grid>();

        init();
    }

   /* private void Update()
    {
      
        for (int i=0; i < debug_list.Count-1; i++) {
            Debug.DrawLine(debug_list[i], debug_list[i+1], Color.red);
        }
        
    }*/

    public void init()
    {
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

    public Vector3 find_nearest_space(Vector3 dest_pos, Vector3 start_pos) {
        Vector3 dir = dest_pos- start_pos;
        int dx_ = Mathf.RoundToInt(dir.x);
        int dy_ = Mathf.RoundToInt(dir.z);
        if (dx_ !=0) {
            dx_ /= Mathf.Abs(dx_);
        }
        if (dy_ != 0)
        {
            dy_ /= Mathf.Abs(dy_);
        }

        Node node = NodeFromWorldPosition(dest_pos );
        //debug_list = new List<Vector3>();

        if (dx_ !=0 && dy_ !=0 ) { //대각일때
            int dx=0;
            int dy =0;

            int len = 0;
            for (int i = 0; i < 16; i++)
            {

                node = grid_node[node.gridX - dx_, node.gridY - dy_];
                
                //debug_list.Add(node.position);
                
                len = i * 2 + 3;
                for (int step = 0; step < len; step++) {
                    dx = step* dx_;
                    dy = 0;
                    //debug_list.Add(node.position + new Vector3(dx, 0, dy));
                    if (grid_node[node.gridX + dx, node.gridY + dy].iswall)
                    {
                        
                        return node.position + new Vector3(dx, 0, dy);
                    }
                    dx = 0;
                    dy = step*dy_;
                    //debug_list.Add(node.position + new Vector3(dx, 0, dy));
                    if (grid_node[node.gridX + dx, node.gridY + dy].iswall)
                    {
                        return node.position + new Vector3(dx, 0, dy);
                    }
                }
                for (int step = 1; step < len; step++)
                {
                    dx = (len-1)* dx_;
                    dy = step * dy_;
                    //debug_list.Add(node.position + new Vector3(dx, 0, dy));
                    if (grid_node[node.gridX + dx, node.gridY + dy].iswall)
                    {
                        return node.position + new Vector3(dx, 0, dy);
                    }
                    dx = step*dx_;
                    dy = (len-1) * dy_;
                    //debug_list.Add(node.position + new Vector3(dx, 0, dy));
                    if (grid_node[node.gridX + dx, node.gridY + dy].iswall)
                    {
                        return node.position + new Vector3(dx, 0, dy);
                    }
                }
            }
        }
        else{ //수직일때 
            Vector2Int right = new Vector2Int(dy_, dx_);
            Vector2Int up = new Vector2Int(dx_, dy_);

            Vector2Int delta = Vector2Int.zero;  

            for (int i = 0; i < 16; i++)
            {
                node = grid_node[node.gridX - dx_, node.gridY - dy_];
                //debug_list.Add(node.position);
                for (int step = 0; step <= i+1; step++)
                {
                    delta = right * step;
                    //debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                    delta = -right * step;
                   // debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                }
                int len = i * 2 + 3;
                for (int step = 1; step < len; step++)
                {
                    delta = right * (i+1) + up * step;
                    //debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                    delta = -right * (i + 1) + up * step;
                   // debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                }

                for (int step = 0; step <= i + 1; step++)
                {
                    delta = right * (i + 1 - step) + up * (len-1);
                    //debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                    delta = -right * (i + 1 - step) + up * (len - 1);
                    //debug_list.Add(node.position + new Vector3(delta.x, 0, delta.y));
                    if (grid_node[node.gridX + delta.x, node.gridY + delta.y].iswall)
                    {
                        return node.position + new Vector3(delta.x, 0, delta.y);
                    }
                }
            }
        }
        
        

        /*int[] dx = {-1,0,1 };
        int[] dy = {-1,0,1 };
        
        for (int i =0; i < dx.Length; i++) {
            for (int j = 0; j < dy.Length; j++) {
                if (grid_node[node.gridX + dx[i], node.gridY + dy[j]].iswall) {
                    return node.position + new Vector3(dx[i],0, dy[i]);
                }
            }
        }*/
        return dest_pos;
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

                
                if ((furnitureData!= null && !furnitureData.CanPlaceObjectAt(worldPointToCell, Vector2Int.one,is_path_finding: true)) || Physics.CheckSphere(worldPoint, nodeRadius, wallMask)) //원에 충돌하면 
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
