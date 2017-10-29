using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    private bool firstTurn;
    GameObject[] player = new GameObject[10];
    GameObject[] enemy = new GameObject[20];
    private static bool firstRun = true;
    private static int numberOfTurns;

    private bool hasRunOnce = false;    

    void Start()
    {
        TurnBehaviour.OnGameStart += this.GameStartActions;
        TurnBehaviour.OnTurnStart += this.TurnStartActions;
        TurnBehaviour.OnTurnEnd += this.TurnEndActions;
        TurnBehaviour.GStart();
        player = GameObject.FindGameObjectsWithTag("Player");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        playerTurn = true;
        firstTurn = true;
        TurnBehaviour.Initialize(playerTurn);
        
    }

    private void OnDestroy()
    {
        TurnBehaviour.OnGameStart -= this.GameStartActions;
        TurnBehaviour.OnTurnStart -= this.TurnStartActions;
        TurnBehaviour.OnTurnEnd -= this.TurnEndActions;
    }

    // Game Start Event - Put any actions you want when game starts in here
    public void GameStartActions()
    {
        Debug.Log("GAME STARTED");

    }
    // Turn Start Event - Put any actions you want when a turn starts in here
    public void TurnStartActions()
    {
        Debug.Log("Turn STARTED");

    }
    // Turn End Event - Put any actions you want when a turn Ends in here
    public void TurnEndActions(bool playerturn)
    {
        Debug.Log("Turn ENDED");
        firstRun = true;
        numberOfTurns++;
    }

    public void setTurn()
    {

        if (GameObject.FindWithTag("Map").GetComponent<TileMap>() == null)
        {
            return;
        }
        TileMap map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        Actor unit = map.selectedUnit.GetComponent<Actor>();

        if(map.getEndOfMove() == false)
        {
            return;
        }

        if (GameObject.FindWithTag("Player") == null || GameObject.FindWithTag("Enemy") == null)
        {
            return;
        } 

        //Change to arrays in future to hold multiple players/enemies
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (playerTurn == false) //if it is enemy's turn aka false, we switch it to players turn aka true
        {
            //Player turn
            changeTurn(true);
           
            Debug.Log("PLAYER TURN");

            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Player Turn";

            unit = player.GetComponent<Actor>();
            map.selectedUnit = player;
     
            map.getMapArray()[(int)unit.getCoords().x,
                (int)unit.getCoords().y, 
                (int)unit.getCoords().z].setOccupiedTrue();

            
        }
        else    //if it is player's turn aka true, we switch to enemy turn aka false
        {
            //enemy turn
            changeTurn(false);
                        
            Debug.Log("ENEMY TURN");
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Enemy Turn";

            unit = enemy.GetComponent<Actor>();
            map.selectedUnit = enemy;
           
            map.getMapArray()[(int)unit.getCoords().x, (int)unit.getCoords().y,(int)unit.getCoords().z].setOccupiedTrue();
        }
    }

    void EventTrigger()
    {
        if(!hasRunOnce) //runs only once per turn
        {
            //trigger TurnStart event
            //trigger Player Turn Start event
            //trigger Enemy Turn Start event
        }
    }
   
    //true player false enemy

    void changeTurn(bool tf)
    {
        playerTurn = tf;
        TurnBehaviour.NextTurnEventTrigger(tf);
    }

    public bool checkTurn()
    {
        return playerTurn;
    }

    public bool getFirstTurn()
    {
        return firstTurn;
    }


}
