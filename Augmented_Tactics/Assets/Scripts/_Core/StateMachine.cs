using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    private bool firstTurn;
    GameObject[] player = new GameObject[10];
    GameObject[] enemy = new GameObject[20];

    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player");
        enemy = GameObject.FindGameObjectsWithTag("Player");
        playerTurn = true;
        firstTurn = true;
        TurnBehavoir.Initialize(playerTurn);
    }

    public void setTurn()
    {
        if (GameObject.FindWithTag("Map").GetComponent<TileMap>() == null)
        {
            return;
        }
        TileMap map = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        Actor unit = map.selectedUnit.GetComponent<Actor>();

        if (GameObject.FindWithTag("Player") == null || GameObject.FindWithTag("Enemy") == null)
        {
            return;
        } 

        if (firstTurn == true)
        {
            playerTurn = false;
            firstTurn = false;
            
        }

        //Change to arrays in future to hold multiple players/enemies
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (playerTurn == true)
        {
            //Player turn
            changeTurn(false);
           
            Debug.Log("PLAYER TURN");

            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Player Turn";
        
            map.selectedUnit = player;
            map.selectedUnit.GetComponent<Actor>().setMoves(1);
            map.getMapArray()[unit.tileX, unit.tileZ].setOccupiedTrue();

            
        }
        else
        {
            //enemy turn
            changeTurn(true);
                        
            Debug.Log("ENEMY TURN");
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Enemy Turn";

            map.selectedUnit = enemy;
            map.selectedUnit.GetComponent<Actor>().setMoves(1);
            map.getMapArray()[unit.tileX, unit.tileZ].setOccupiedTrue();
        }
    }
   
    //true player false enemy

    void changeTurn(bool tf)
    {
        playerTurn = tf;
        TurnBehavoir.newTurn(playerTurn);
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
