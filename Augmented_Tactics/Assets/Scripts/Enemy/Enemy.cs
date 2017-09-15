using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private GameObject[] userTeam;
	// Use this for initialization
	void Start () {
        userTeam = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public class Location
    {
        public int coordX;
        public int coordZ;

        public Location()
        {
            coordX = 0;
            coordZ = 0;
        }
        public Location(int X, int Z)
        {
            coordX = X;
            coordZ = Z;
        }
    }

    void enemyTurn()
    {
        GameObject target = findNearestPlayer();
    //    if (target == findWeakestPlayer())
      //      Attack(target);
    }
    private GameObject findNearestPlayer()
    {
        GameObject nearest = userTeam[0] ;
        float currentNearest = 10000000;
        foreach(GameObject user in userTeam)
        {
            Vector2 playerLocation = new Vector2((float)user.GetComponent<Actor>().tileX, (float)user.GetComponent<Actor>().tileZ);
            Vector2 enemyLocation = new Vector2(tileX, tileZ);
            float distanceFromPlayer = Vector2.Distance(playerLocation, enemyLocation);
            /*if (distanceFromPlayer <= 1) attack if player is in range
                Attack(user);*/
            if (distanceFromPlayer < currentNearest)
            {
                nearest = user;
                currentNearest = distanceFromPlayer;
            }
        }
        return nearest;
    }

    private GameObject findWeakestPlayer()
    {
        GameObject weakest = userTeam[0];
        float lowestHealth = userTeam[0].GetComponent<Enemy>().health_current; //until we figure out how health and damage work
        foreach (GameObject user in userTeam)
        {
            if( user.GetComponent<Enemy>().health_current < lowestHealth)
            {
                weakest = user;
                lowestHealth = user.GetComponent<Enemy>().health_current; 
            }
        }
        return weakest;

    }
}
