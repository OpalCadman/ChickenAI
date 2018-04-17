using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : DecisionTreeNode
{
    public StateMachine state;

    public override DecisionTreeNode MakeDecision()
    {
        return this;
    }
}


