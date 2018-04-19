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
    public bool combatOn = true;
    public static int playerNum;
    public static Actor[] playerList;
    public static GameObject[] playerObjs;
    private int playerID;
    // Use this for initialization
    new void Start ()
    {
        if (!combatOn)
            return;
        PlayerInitialize();
    }

    public void PlayerInitialize()
    {
        Debug.Log("Player Init");
        base.Init();
        TurnBehaviour.OnPlayerTurnStart += this.OnPlayerTurnStart;

        abilitySet = new Ability[4];  //test
        for (int i = 0; i < 4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }
        // GameObject.FindWithTag("Map").GetComponent<TileMap>().Players.Add(this.GetComponent<Actor>());

        if (GameObject.Find("Map") != null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }
        else
        {
            return;
        }

        if (playerNum == null)
            playerNum = 0;
        if (playerList == null)
            playerList = new Actor[4];
        if (playerObjs == null)
            playerObjs = new GameObject[4];

        if(playerNum == 4)
        {
            Debug.Log("More than 4 players attempting to add: " + name);
        }
        playerObjs[playerNum] = gameObject;
        playerList[playerNum] = this;
        playerID = playerNum;
        Debug.Log("Player added: " + playerNum + ") " + playerList[playerNum]);
        playerNum++;

        TurnBehaviour.NewPlayerAdded();

        if (map.IsValidCoord(coords) == true)
        {
            Debug.Log("Coords: " + coords);
            map.GetTileAt(coords).setOccupiedTrue(gameObject);
            Debug.Log("Occupied = " + map.GetTileAt(coords).isOccupied());
        }
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

    // Update is called once per frame
    void Update ()
    {
        base.Update();
        //turnControl();
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
        if(numOfActions != 0)
            rangeMarker.Move_Marker_On(getCoords(), this.moveDistance);
    }

    public void refresh()
    {
       
    }

    public override void TakeDamage(float damage, GameObject attacker)
    {
        base.TakeDamage(damage, attacker);
        HealthBarUIManager.updateHealth();
    }

    public int GetID()
    {
        return playerID;
    }
}
