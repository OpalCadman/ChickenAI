using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour
{
    public bool DisplayGridGizmos;
    public LayerMask canWalkMask;
    public Vector2 G_worldSize;
    public float N_Radius;
    Node[,] grid;

    float Diameter_Node;
    int gridX;
    int gridY;

    void Awake()
    {
        Diameter_Node = N_Radius * 2;
        gridX = Mathf.RoundToInt(G_worldSize.x / Diameter_Node);
        gridY = Mathf.RoundToInt(G_worldSize.y / Diameter_Node);
        CreateGrid();
    }

    public int Max_Size
    {
        get {
            return gridX * gridY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridX, gridY];
        Vector3 BottomleftPos = transform.position - Vector3.right * G_worldSize.x / 2 - Vector3.forward * G_worldSize.y / 2;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 W_Point = BottomleftPos + Vector3.right * (x * Diameter_Node + N_Radius) + Vector3.forward * (y * Diameter_Node + N_Radius);
                bool canWalk = !(Physics.CheckSphere(W_Point, N_Radius, canWalkMask));
                grid[x, y] = new Node(canWalk, W_Point, x, y);
            }
        }
    }

    public List<Node> Neighbours(Node node)
    {
        List<Node> Neighbours = new List<Node>();

        for (int x = -1; x <= 1; ++x)
        {
            for (int y = -1; y <= 1; ++y)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int x_check = node.X_Grid + x;
                int y_check = node.Y_Grid + y;

                if (x_check >= 0 && x_check < gridX && y_check >= 0 && y_check < gridY)
                {
                    Neighbours.Add(grid[x_check, y_check]);
                }
            }
        }

        return Neighbours;
    }

    public Node WorldPointNode(Vector3 worldPos)
    {
        float percX = (worldPos.x + G_worldSize.x / 2) / G_worldSize.x;
        float percY = (worldPos.z + G_worldSize.y / 2) / G_worldSize.y;

        percX = Mathf.Clamp01(percX);
        percY = Mathf.Clamp01(percY);

        int x = Mathf.RoundToInt((gridX - 1) * percX);
        int y = Mathf.RoundToInt((gridY - 1) * percY);
        return grid[x, y];

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(G_worldSize.x, 1, G_worldSize.y));

        if (grid != null && DisplayGridGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.canWalk) ? Color.white : Color.red;
                Gizmos.DrawCube(n.WorldPos, Vector3.one * (Diameter_Node - 0.1f));
            }
        }
    }
}
