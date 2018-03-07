using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Grid : MonoBehaviour {

    public LayerMask canWalkMask;
    public Vector2 G_worldSize;
    public float N_Radius;
    Node[,] grid;


    private void OnDrawGizmos()
    {
    }
}
