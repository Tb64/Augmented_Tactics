using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    private bool enemyTurn;

	public void setTurn()
    {
        if (playerTurn == true)
        {
            playerTurn = false;
            enemyTurn = true;
        }
        else
        {
            enemyTurn = false;
            playerTurn = true;
        }
    }
   

    public bool checkTurn()
    {
        return playerTurn;
    }

	void Update () {
		
	}
}
