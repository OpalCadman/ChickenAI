using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RequestPathManager : MonoBehaviour
{
    Queue<RequestPath> QueueRequestPath = new Queue<RequestPath>();
    RequestPath currentRequestPath;

    static RequestPathManager instance;
    Pathfinding path_finding;

    bool pathisProcessing;


    void Awake()
    {
        instance = this;
        path_finding = GetComponent<Pathfinding>();
    }
    public static void Path_Request(Vector3 Start_Path, Vector3 End_Path, Action<Vector3[], bool> waitingTime)
    {
        RequestPath RequestNew = new RequestPath(Start_Path, End_Path, waitingTime);
        instance.QueueRequestPath.Enqueue(RequestNew);
        instance.TryNextProcess();
    }

    public void processingFinished(Vector3[] path, bool success)
    {
        currentRequestPath.waitingTime(path, success);
        pathisProcessing = false;
        TryNextProcess();
    }

    void TryNextProcess()
    {
        if (!pathisProcessing && QueueRequestPath.Count > 0)
        {
            currentRequestPath = QueueRequestPath.Dequeue();
            pathisProcessing = true;
            path_finding.StartFindPath(currentRequestPath.startPath, currentRequestPath.endPath);
        }
    }

    struct RequestPath
    {
        public Vector3 startPath;
        public Vector3 endPath;
        public Action<Vector3[], bool> waitingTime;

        public RequestPath(Vector3 _startPath, Vector3 _endPath, Action<Vector3[], bool> _waitingTime)
        {
            startPath = _startPath;
            endPath = _endPath;
            waitingTime = _waitingTime;

        }
    }

}
