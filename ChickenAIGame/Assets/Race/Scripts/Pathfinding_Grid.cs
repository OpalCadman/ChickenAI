using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour {

    public LayerMask canWalkMask;
    public Vector2 G_worldSize;
    public float N_Radius;
    Node[,] grid;

    float Diameter_Node;
    int gridX;
    int gridY;

    void Start()
    {
        Diameter_Node = N_Radius * 2;
        gridX = Mathf.RoundToInt(G_worldSize.x / Diameter_Node);
        gridY = Mathf.RoundToInt(G_worldSize.y / Diameter_Node);
        CreateGrid();
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
                grid[x, y] = new Node(canWalk, W_Point);
            }
        }
    }

    /*public Node WorldPointNode(Vector3 worldPos)
    {
        float percX = (worldPos.x + G_worldSize.x / 2) / G_worldSize.x);
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(G_worldSize.x, 1, G_worldSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.canWalk) ? Color.white : Color.red;
                Gizmos.DrawCube(n.WorldPos, Vector3.one * (Diameter_Node - 0.1f));
            }
        }
    }
}
