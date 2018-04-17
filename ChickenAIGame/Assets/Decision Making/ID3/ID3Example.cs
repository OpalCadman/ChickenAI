using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ID3Action
// All possible actions for the chickens to perform.
{
    STOP, WALK, RUN, JUMP
}


public class ID3Example : MonoBehaviour
{
    public ID3Action action;
    public Dictionary<string, float> values;

    public float GetValue(string attribute)
    {
        return values[attribute];
    }
}

