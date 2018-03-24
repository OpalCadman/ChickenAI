using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Created node class for A* Pathfinding. These will be used to create a grid of nodes to allow them to be 
into consideration when creating the grid and pathfinding. We need to know if you can walk on it to 
distinguish whether or not it is an obstacle and we need to know where it is in the world.*/
public class Node : Iheap_Item<Node>
{
    public bool canWalk;
    public Vector3 WorldPos;
    public int X_Grid;
    public int Y_Grid;

    public int G_Cost;
    public int H_Cost;
    public Node parent;
    int heap_Ind;

    public Node(bool can_walk, Vector3 World_Pos, int _XGrid, int _YGrid)
    {
        canWalk = can_walk;
        WorldPos = World_Pos;
        X_Grid = _XGrid;
        Y_Grid = _YGrid;

    }

    public int F_Cost
    {
        get
        {
            return G_Cost + H_Cost;
        }
    }

    public int Index_Heap
    {
        get
        {
            return heap_Ind;
        }

        set
        {
            heap_Ind = value;
        }
    }

    public int CompareTo(Node Compare_Node)
    {
        int compare = F_Cost.CompareTo(Compare_Node.F_Cost);
        if (compare == 0)
        {
            compare = H_Cost.CompareTo(Compare_Node.H_Cost);
        }
        return -compare;
    }
}
