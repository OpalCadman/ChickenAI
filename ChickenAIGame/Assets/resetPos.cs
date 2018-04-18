using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPos : MonoBehaviour {

    Chickens chicken;
	// Use this for initialization
	void Start () {

        chicken = FindObjectOfType<Chickens>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (chicken.reset == true)
        {
            Destroy(gameObject);
        }
		
	}
}
