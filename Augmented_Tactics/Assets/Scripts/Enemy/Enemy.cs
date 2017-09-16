using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    public Location findNearestPlayer() {
        Location nearest = Players[0] ;
        float currentNearest = 10000000;
        foreach(Object player in Players)
        {
            Vector2 playerLocation = new Vector2((float)player.X, (float)player.Z));
            Vector2 enemyLocation = new Vector2(tileX, tileZ);
            float distanceFromPlayer = Vector2.Distance(playerLocation, enemyLocation);
            if (distanceFromPlayer < currentNearest)
                nearest = player;
        }
        return nearest;
    }
    */
}
