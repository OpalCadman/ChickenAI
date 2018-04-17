using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionNode : MonoBehaviour
{

    public string testValue;
    public Dictionary<float, DecisionNode> children;

    public DecisionNode(string testValue = "")
    {
        this.testValue = testValue;
        children = new Dictionary<float, DecisionNode>();
    }
    

}

	