using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Created node class for A* Pathfinding. These will be used to create a grid of nodes to allow them to be 
into consideration when creating the grid and pathfinding. We need to know if you can walk on it to 
distinguish whether or not it is an obstacle and we need to know where it is in the world.*/
public class Node
{
    public bool canWalk;
    public Vector3 WorldPos;

    public Node(bool can_walk, Vector3 World_Pos)
    {
        canWalk = can_walk;
        WorldPos = World_Pos;
    }
}
