using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour
{
    RequestPathManager requestManager;
    Pathfinding_Grid grid;

    void Awake()
    {
        grid = GetComponent<Pathfinding_Grid>();
        requestManager = GetComponent<RequestPathManager>();
    }

    public void StartFindPath(Vector3 start_Pos, Vector3 target_Pos)
    {
        StartCoroutine(Find_Path(start_Pos, target_Pos));
    }

    IEnumerator Find_Path(Vector3 Start_Pos, Vector3 Target_pos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathsucess = false;

        Node start_Node = grid.WorldPointNode(Start_Pos);
        Node target_Node = grid.WorldPointNode(Target_pos);

        if (start_Node.canWalk && target_Node.canWalk)
        {
            Heap<Node> open_Set = new Heap<Node>(grid.Max_Size);
            HashSet<Node> closed_set = new HashSet<Node>();

            open_Set.Add(start_Node);

            while (open_Set.Heap_Count > 0)
            {
                Node cur_node = open_Set.Remove_First_Heap();

                closed_set.Add(cur_node);

                if (cur_node == target_Node)
                {
                    pathsucess = true;
                    break;
                }

                foreach (Node Neighbours in grid.Neighbours(cur_node))
                {
                    if (!Neighbours.canWalk || closed_set.Contains(Neighbours))
                    {
                        continue;
                    }

                    int NewCostToNeighbour = cur_node.G_Cost + GetNodeDistance(cur_node, Neighbours);
                    if (NewCostToNeighbour < Neighbours.G_Cost || !open_Set.contain(Neighbours))
                    {
                        Neighbours.G_Cost = NewCostToNeighbour;
                        Neighbours.H_Cost = GetNodeDistance(Neighbours, target_Node);
                        Neighbours.parent = cur_node;

                        if (!open_Set.contain(Neighbours))
                        {
                            open_Set.Add(Neighbours);
                        }
                    }
                }
            }
        }

        yield return null;
        if (pathsucess)
        {
            waypoints = RetracePath(start_Node, target_Node);
        }

        requestManager.processingFinished(waypoints, pathsucess);
    }

    Vector3[] RetracePath(Node Start_Node, Node End_Node)
    {
        List<Node> path = new List<Node>();
        Node cur_node = End_Node;

        while (cur_node != Start_Node)
        {
            path.Add(cur_node);
            cur_node = cur_node.parent;
        }

        Vector3[] waypoints = SimplifiedPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }

    Vector3[] SimplifiedPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 Old_Direction = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 New_Direction = new Vector2(path[i-1].X_Grid - path[i].X_Grid, path[i - 1].Y_Grid - path[i].Y_Grid);
            if (New_Direction != Old_Direction)
            {
                waypoints.Add(path[i].WorldPos);
            }
            Old_Direction = New_Direction;
        }

        return waypoints.ToArray();
    }
    int GetNodeDistance(Node getNodeA, Node getNodeB)
    {
        int Distance_X = Mathf.Abs(getNodeA.X_Grid - getNodeB.X_Grid);
        int Distance_Y = Mathf.Abs(getNodeA.Y_Grid - getNodeB.Y_Grid);

        if (Distance_X > Distance_Y)
        {
            return 14 * Distance_Y + 10 * (Distance_X - Distance_Y);
        }
        else
        {
            return 14 * Distance_X + 10 * (Distance_Y - Distance_X);
        }
    }
}
