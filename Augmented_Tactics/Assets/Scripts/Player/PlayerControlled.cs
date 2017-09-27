﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

    // Use this for initialization
    void Start ()
    {
        base.Start();
        abilitySet = new BasicAttack[4];
       // GameObject.FindWithTag("Map").GetComponent<TileMap>().Players.Add(this.GetComponent<Actor>());
    }

    void OnEnable()
    {
        numberOfActors++;
    }


    
    // Update is called once per frame
    void Update () {
        base.Update();

        turnControl();
    }



    void turnControl()
    {

        //true player turn ,false enemy turn
        if (SM.GetComponent<StateMachine>().checkTurn() == true)
        {
            
            drawDebugLines();
            moveUnit();

            if(currentPath == null)
            {
                remainingMovement = 0;
            }

        }

    }
}
