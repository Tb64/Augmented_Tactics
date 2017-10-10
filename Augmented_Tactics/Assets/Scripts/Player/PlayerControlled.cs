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
        health_current = 100;
        Debug.Log("Player added: " + playerNum + ") " + playerList[playerNum]);
        playerNum++;

        abilitySet = new BasicAttack[4];
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






    public int GetID()
    {
        return playerID;
    }
}
