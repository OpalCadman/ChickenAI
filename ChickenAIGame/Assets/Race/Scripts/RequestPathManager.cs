using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RequestPathManager : MonoBehaviour
{
    Queue<RequestPath> QueueRequestPath = new Queue<RequestPath>();
    RequestPath currentRequestPath;

    static RequestPathManager instance;


    void Awake()
    {
        instance = this;   
    }
    public static void Path_Request(Vector3 Start_Path, Vector3 End_Path, Action<Vector3[], bool> waitingTime)
    {
        RequestPath RequestNew = new RequestPath(Start_Path, End_Path, waitingTime);
        instance.QueueRequestPath.Enqueue(RequestNew);
        instance.TryNextProcess();
    }

    void TryNextProcess()
    {
        /*5:51 Units episode 6*/
    }

    struct RequestPath
    {
        public Vector3 startPath;
        public Vector3 endPath;
        Action<Vector3[], bool> waitingTime;

        public RequestPath(Vector3 _startPath, Vector3 _endPath, Action<Vector3[], bool> _waitingTime)
        {
            startPath = _startPath;
            endPath = _endPath;
            waitingTime = _waitingTime;

        }
    }

}
