using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{

    public GameObject SM;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        SM = GameObject.FindWithTag("GameController");
       // GameObject.FindWithTag("Map").GetComponent<TileMap>().Players.Add(this.GetComponent<Actor>());
    }

    void OnEnable()
    {
        numberOfActors++;
    }

    // Update is called once per frame
    void Update () {
        drawDebugLines();
        moveUnit();
        //turnControl();
    }


    void turnControl()
    {
        if (SM.GetComponent<StateMachine>().checkTurn() == true)
        {//player turn
            
            SM.GetComponent<StateMachine>().setTurn();
        }
        else
        {//enemy turn

        }
    }

}
