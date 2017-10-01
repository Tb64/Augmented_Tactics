using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    private bool firstTurn;
    
    void Start()
    {

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

        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (firstTurn == true)
        {
            playerTurn = false;
            firstTurn = false;
            map.getMapArray()[enemy.GetComponent<Actor>().tileX, 
                enemy.GetComponent<Actor>().tileZ].setOccupiedTrue();
            map.getMapArray()[player.GetComponent<Actor>().tileX, 
                player.GetComponent<Actor>().tileZ].setOccupiedTrue();
        }

        //Change to arrays in future to hold multiple players/enemies


        if (playerTurn == true)
        {
            
            //Player turn
            Debug.Log("PLAYER TURN");

            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Player Turn";
            TurnBehavoir.newTurn(playerTurn);
           
            map.selectedUnit = player;
            map.selectedUnit.GetComponent<Actor>().setMoves(1);
            map.getMapArray()[unit.tileX, unit.tileZ].setOccupiedTrue();

            playerTurn = false;
        }
        else
        {
            //enemy turn
            
            playerTurn = true;
            TurnBehavoir.newTurn(playerTurn);
            
            Debug.Log("ENEMY TURN");
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Enemy Turn";

            map.selectedUnit = enemy;
            map.selectedUnit.GetComponent<Actor>().setMoves(1);
            map.getMapArray()[unit.tileX, unit.tileZ].setOccupiedTrue();
        }
    }
   
    //true player false enemy
    public bool checkTurn()
    {
        return playerTurn;
    }

	void Update () {
		
	}
}
