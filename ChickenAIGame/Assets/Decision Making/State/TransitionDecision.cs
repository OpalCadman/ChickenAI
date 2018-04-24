using UnityEngine;
using System.Collections;

public class TransitionDecision : MonoBehaviour
{
    public DecisionTreeNode root;

    public StateMachine GetState()
    {
        ActionState action;
        action = root.MakeDecision() as ActionState;
        return action.state;
    }
}