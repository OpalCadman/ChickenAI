using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chickens : MonoBehaviour
{
    public Transform target;
    public GameObject race;
    float speed = 4f;
    Vector3[] path;
    int target_index;
    public bool reset = false;

    private void Start()
    {
        RequestPathManager.Path_Request(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] new_Path, bool path_Sucess)
    {
        if (path_Sucess)
        {
            path = new_Path;
            StopCoroutine("Follow_Path");
            StartCoroutine("Follow_Path");
        }
    }

    IEnumerator Follow_Path()
    {
        Vector3 current_Waypoint = path[0];

        while (true)
        {
            if (transform.position == current_Waypoint)
            {
                target_index++;
                if (target_index >= path.Length)
                {
                    yield break;
                }

                current_Waypoint = path[target_index];
            }

            transform.position = Vector3.MoveTowards(transform.position, current_Waypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = target_index; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == target_index)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            reset = true;
        }
    }
}
