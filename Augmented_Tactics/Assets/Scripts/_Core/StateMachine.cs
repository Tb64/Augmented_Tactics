using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    
    void Start()
    {
        playerTurn = true;
        TurnBehavoir.Initialize(playerTurn);
    }

    public void setTurn()
    {
        if (GameObject.FindWithTag("Map").GetComponent<TileMap>() == null)
        {
            return;
        }
        TileMap GO = GameObject.FindWithTag("Map").GetComponent<TileMap>();
        
        if (GameObject.FindWithTag("Player") == null || GameObject.FindWithTag("Enemy") == null)
        {
            return;
        }
        
        //Change to arrays in future to hold multiple players/enemies
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (playerTurn == true)
        {
            //enemy turn

            playerTurn = false;
            TurnBehavoir.newTurn(playerTurn);
            Debug.Log("ENEMY TURN");
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Enemy Turn";

            GO.selectedUnit = enemy;
            GO.selectedUnit.GetComponent<Actor>().setMoves(1);
        }
        else
        {
            //player turn
            Debug.Log("PLAYER TURN");
            playerTurn = true;
            TurnBehavoir.newTurn(playerTurn);
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Player Turn";

            GO.selectedUnit = player;
            GO.selectedUnit.GetComponent<Actor>().setMoves(1);

        }
    }
   


    public bool checkTurn()
    {
        return playerTurn;
    }

	void Update () {
		
	}
}
