using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour {

    private Actor[] players;
    private Actor[] enemies;
  
    void Start () {
        TurnBehaviour.OnUnitSpawn += this.playerAdded;
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void playerAdded()
    {
        players = PlayerControlled.playerList;
        enemies = EnemyController.enemyList;
    }

    public static void addStatus(StatusEffectRPG status)
    {

    }
}
