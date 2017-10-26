using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************
PlayerControlled
This is the parent class 
for all player controlled actors
********************/


public class PlayerControlled : Actor
{
    public static int playerNum;
    public static Actor[] playerList;
    private int playerID;
    // Use this for initialization
    new void Start ()
    {
        base.Init();

        TurnBehaviour.OnPlayerTurnStart += this.OnPlayerTurnStart;
        if (playerNum == null)
            playerNum = 0;
        if (playerList == null)
            playerList = new Actor[4];
        playerList[playerNum] = this;
        playerID = playerNum;
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

        TurnBehaviour.NewPlayerAdded();

    }

    public void OnDestroy()
    {
        base.OnDestroy();
        TurnBehaviour.OnPlayerTurnStart -= this.OnPlayerTurnStart;
    }

    public virtual void OnPlayerTurnStart()
    {
        refresh();
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
            //map.moveUnit();
        }


    }

    public void MoveSelected()
    {
        
        if(numOfMoves != 0)
            rangeMarker.Marker_On(getCoords(), this.moveDistance);
    }

    public void refresh()
    {
        remainingMovement = moveDistance;
    }


    public int GetID()
    {
        return playerID;
    }
}
