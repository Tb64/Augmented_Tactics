﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        drawDebugLines();

        moveUnit();
    }


    void turnControl()
    {
        if (GetComponent<StateMachine>().checkTurn() == true)
        {//player turn

        }
        else
        {//enemy turn

        }
    }

}
