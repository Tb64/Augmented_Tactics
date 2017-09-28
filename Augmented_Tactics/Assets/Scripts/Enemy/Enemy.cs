using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private GameObject[] userTeam;
    //private int callControl = 0;
	// Use this for initialization
	void Start () {
	    base.Start();
        userTeam = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();

        turnControl();
    }

    void turnControl()
    {

        //true player turn ,false enemy turn
        if (SM.GetComponent<StateMachine>().checkTurn() == false)
        {
           // if (callControl == 0)
            //{
                enemyTurn();
                //callControl++;
            //}
            drawDebugLines();
            moveUnit();
        }
       /* else
        {
            callControl = 0;
        }*/


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
        map.selectedUnit = gameObject;
        Actor nearest = findNearestPlayer().GetComponent<Actor>(), weakest = findWeakestPlayer().GetComponent<Actor>();
        if (GetHealthPercent() < nearest.GetHealthPercent())
            HealHealth(100);
        Actor target = nearest;
        Vector3 enemyPosition = new Vector3((float)tileX, (float)tileZ,0), playerPosition = new Vector3((float)target.tileX, (float)target.tileZ,0);
        float distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
        if (target != weakest)
        {
            target = weakest;
            Vector3 weakestPosition = new Vector3((float)weakest.tileX, (float)weakest.tileZ,0);
            float distanceToWeakest = Vector3.Distance(weakestPosition, enemyPosition);
            if (distanceToWeakest > moveDistance && distanceToWeakest > 2 * distanceToNearest)
            {
                target = nearest;
                map.GeneratePathTo(target.tileX, target.tileZ);
            }
            else
            {
                map.GeneratePathTo(target.tileX, target.tileZ);
            }
        }
        /*Debug.Log(target.name+" "+ target.GetComponent<Actor>().tileX+" "+ target.GetComponent<Actor>().tileZ);
        //map.GeneratePathTo(target.tileX, target.tileZ);
        //after moving, if enemy is in range attack*/
        if (Vector2.Distance(enemyPosition, playerPosition) <= 1)
            Attack(target);
        NextTurn();
    }
    private GameObject findNearestPlayer()
    {
        GameObject nearest = userTeam[0] ;
        float currentNearest = 10000000;
        foreach(GameObject user in userTeam)
        {
            Actor player = user.GetComponent<Actor>();
            //^^not 100% on this due to GetComponent being called up to 10 times. Might build array differently later: Andrew
            Vector3 playerLocation = new Vector3(player.tileX, player.tileZ,0), enemyLocation = new Vector3(tileX, tileZ, 0);
            float distanceFromPlayer = Vector3.Distance(playerLocation, enemyLocation);
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
        float lowestHealth = userTeam[0].GetComponent<Actor>().GetHealthPercent();
        foreach (GameObject user in userTeam)
        {
            Actor player = user.GetComponent<Actor>();
            //same as findNearest.
            float playerHealth = player.GetHealthPercent();
            if ( playerHealth < lowestHealth)
            {
                weakest = user;
                lowestHealth = playerHealth; 
            }
        }
        return weakest;

    }
/// <summary>
/// //////////////////////// where to add attacking
/// </summary>
/// <param name="target"></param>
    void Attack(Actor target)
    {

    }
 
}
