using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanGenTest : MonoBehaviour {
    public PlayerData player;
	// Use this for initialization
	void Start () {
        player = PlayerData.GenerateNewPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
