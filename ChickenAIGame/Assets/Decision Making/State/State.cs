using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//got to use this as reference for statemachine
public class State : MonoBehaviour
{
    public List<Transition> transitions;

    public virtual void Awake()
    {
        transitions = new List<Transition>();
        // TO-DO
        // setup your transitions here
    }
    public virtual void OnEnable()
    {
        // TO-DO
        // develop state's initialization here
    }

    public virtual void OnDisable()
    {
        // TO-DO
        // develop state's finalization here
    }
    public virtual void Update()
    {
        // TO-DO
        // develop behaviour here
    }

    //late update is needed to branch to mine. 
    public void LateUpdate()
    {
        foreach (Transition t in transitions)
        {
            if (t.condition.Test())
            {
                t.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
}

