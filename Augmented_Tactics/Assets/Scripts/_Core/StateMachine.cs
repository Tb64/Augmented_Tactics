using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private bool playerTurn;
    
    void Start()
    {
        playerTurn = true;
    }

    public void setTurn()
    {
        if (playerTurn == true)
        {
            playerTurn = false;
        }
        else
        {
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
