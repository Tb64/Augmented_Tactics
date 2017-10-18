using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : Actor
{


    public static int playerNum;
    public static Actor[] playerList;
    private int playerID;
    // Use this for initialization
    void Start ()
    {
        base.Start();
        if (playerNum == null)
            playerNum = 0;
        if (playerList == null)
            playerList = new Actor[4];
        playerList[playerNum] = this;
        playerID = playerNum;
        TurnBehavior.NewPlayerAdded();
       // Debug.Log("Player added: " + playerNum + ") " + playerList[playerNum]);
        playerNum++;

        abilitySet = new BasicAttack[4];  //test
        for (int i = 0; i < 4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }
        // GameObject.FindWithTag("Map").GetComponent<TileMap>().Players.Add(this.GetComponent<Actor>());

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

    }

    void OnEnable()
    {
        numberOfActors++;
    }

    public void OnMouseUp()
    {
        base.OnMouseUp();
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();

        Debug.Log("click test");
        GO.selectedUnit = gameObject;
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
            map.drawDebugLines();
            map.moveUnit();
        }


    }

    public void MoveSelected()
    {
        NextTurn();
        if(numOfMoves != 0)
            rangeMarker.Marker_On(getMapPosition(), this.moveDistance);
    }




    public int GetID()
    {
        return playerID;
    }
}
