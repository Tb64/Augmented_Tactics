using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // I added this to avoid loading issues that randomly happen with bosses. points to the correct script before init
    public Enemy script;
}
