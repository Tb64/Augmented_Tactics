using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    
    void Start()
    {
        playerTurn = true;
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
            playerTurn = false;
            Actor Unit;
            Unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit.GetComponent<Actor>();
            //Unit.setMoves(1);

            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Enemy Turn";
            GO.selectedUnit = enemy;
            enemy.GetComponent<Actor>().setMovement(enemy.GetComponent<Actor>().moveDistance);
        }
        else
        {
            playerTurn = true;
            GameObject.Find("EndTurn").GetComponentInChildren<Text>().text = "Player Turn";
            GO.selectedUnit = player;
        }
    }
   


    public bool checkTurn()
    {
        return playerTurn;
    }

	void Update () {
		
	}
}
