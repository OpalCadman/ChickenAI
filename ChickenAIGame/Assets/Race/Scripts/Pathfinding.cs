using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    Pathfinding_Grid grid;

    void Awake()
    {
        grid = GetComponent<Pathfinding_Grid>();
    }

    void Find_Path(Vector3 Start_Pos, Vector3 Target_pos)
    {
        Node start_Node = grid.WorldPointNode(Start_Pos);
        Node target_Node = grid.WorldPointNode(Target_pos);

        List<Node> open_Set = new List<Node>();
        HashSet<Node> closed_set = new HashSet<Node>();

        open_Set.Add(start_Node);
    }
}
