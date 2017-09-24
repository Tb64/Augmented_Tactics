using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private GameObject[] userTeam;
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
            enemyTurn();
            drawDebugLines();
            moveUnit();
        }

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
        if (GetHealthPercent() < findNearestPlayer().GetComponent<Actor>().GetHealthPercent())
            HealHealth(100);
        GameObject target = findNearestPlayer();
       if (target == findWeakestPlayer())
        {
            map.GeneratePathTo(target.GetComponent<Actor>().tileX, target.GetComponent<Actor>().tileZ);
            if (Vector2.Distance(new Vector2((float)target.GetComponent<Actor>().tileX, (float)target.GetComponent<Actor>().tileZ), new Vector2(tileX, tileZ)) <= 1)
            {
                Attack(target);
            }
            NextTurn();
            return;
        }
        else if (Vector2.Distance(new Vector2((float)target.GetComponent<Actor>().tileX, (float)target.GetComponent<Actor>().tileZ), new Vector2(tileX, tileZ)) > moveDistance && Vector2.Distance(new Vector2((float)target.GetComponent<Actor>().tileX, (float)target.GetComponent<Actor>().tileZ), new Vector2(tileX, tileZ)) > Vector2.Distance(new Vector2((float)findNearestPlayer().GetComponent<Actor>().tileX, (float)findNearestPlayer().GetComponent<Actor>().tileZ), new Vector2(tileX, tileZ)))
            target = findNearestPlayer();
        //Debug.Log(target.name+" "+ target.GetComponent<Actor>().tileX+" "+ target.GetComponent<Actor>().tileZ);
        map.GeneratePathTo(target.GetComponent<Actor>().tileX, target.GetComponent<Actor>().tileZ);
        //after moving, if enemy is in range attack
        if (Vector2.Distance(new Vector2((float)target.GetComponent<Actor>().tileX, (float)target.GetComponent<Actor>().tileZ), new Vector2(tileX, tileZ)) < 1)
            Attack(target);
        NextTurn();
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
            if( user.GetComponent<Actor>().GetHealthPercent() < lowestHealth)
            {
                weakest = user;
                lowestHealth = user.GetComponent<Actor>().GetHealthPercent(); 
            }
        }
        return weakest;

    }
/// <summary>
/// //////////////////////// where to add attacking
/// </summary>
/// <param name="target"></param>
    void Attack(GameObject target)
    {

    }
 
}
