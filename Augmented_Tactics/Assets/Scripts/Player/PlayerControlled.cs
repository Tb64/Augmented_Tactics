using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

   
    

    // Use this for initialization
    void Start ()
    {
        base.Start();
        
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
        
        //turnControl();
    }



    void turnControl()
    {

        //true player turn ,false enemy turn
        if (SM.GetComponent<StateMachine>().checkTurn() == true)
        {
            drawDebugLines();
            moveUnit();
        }

    }




}
