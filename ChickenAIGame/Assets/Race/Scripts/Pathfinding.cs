using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour
{
    public Transform seaker;
    public Transform target;
    Pathfinding_Grid grid;

    void Awake()
    {
        grid = GetComponent<Pathfinding_Grid>();
    }

    void Update()
    {
            Find_Path(seaker.position, target.position);
    }

    void Find_Path(Vector3 Start_Pos, Vector3 Target_pos)
    {
        Node start_Node = grid.WorldPointNode(Start_Pos);
        Node target_Node = grid.WorldPointNode(Target_pos);

        Heap<Node> open_Set = new Heap<Node>(grid.Max_Size);
        HashSet<Node> closed_set = new HashSet<Node>();

        open_Set.Add(start_Node);

        while (open_Set.Heap_Count > 0)
        {
            Node cur_node = open_Set.Remove_First_Heap();

            closed_set.Add(cur_node);

            if (cur_node == target_Node)
            {
                RetracePath(start_Node, target_Node);
                return;
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

    void RetracePath(Node Start_Node, Node End_Node)
    {
        List<Node> path = new List<Node>();
        Node cur_node = End_Node;

        while (cur_node != Start_Node)
        {
            path.Add(cur_node);
            cur_node = cur_node.parent;
        }

        path.Reverse();

        grid.path = path;
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
