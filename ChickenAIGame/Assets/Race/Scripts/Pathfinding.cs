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

        while (open_Set.Count > 0)
        {
            Node cur_node = open_Set[0];
            for (int i = 1; i < open_Set.Count; ++i)
            {
                if (open_Set[i].F_Cost < cur_node.F_Cost || open_Set[i].F_Cost == cur_node.F_Cost && open_Set[i].H_Cost < cur_node.H_Cost)
                {
                    cur_node = open_Set[i];
                }
            }

            open_Set.Remove(cur_node);
            closed_set.Add(cur_node);

            if (cur_node == target_Node)
            {
                return;
            }

            /*MINUTE 7:52 ON VIDEO*/
        }
    }
}
