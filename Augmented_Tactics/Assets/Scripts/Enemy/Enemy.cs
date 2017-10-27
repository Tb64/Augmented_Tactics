﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************
Enemy
This is the parent class of all enemies
*****************/

public class Enemy : Actor
{
    private Actor[] userTeam;
    //private int callControl = 0;
    private int enemyID;
    public static int enemyNum;
    public static Actor[] enemyList;
    private Actor nearest, weakest;
    private Vector3 playerPosition, enemyPosition;
    //private float distanceToNearest, distanceToWeakest;
    public Actor getNearest() { return nearest; }
    public void setNearest(Actor nearestPlayer) { nearest = nearestPlayer; }
    public Actor getWeakest() { return weakest; }
    public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    public Vector3 getPlayerPosition() { return playerPosition; }
    public void setPlayerPosition(Vector3 pPosition) { playerPosition = pPosition; }
    public Vector3 getEnemyPosition() { return enemyPosition; }
    public void setEnemyPosition(Vector3 ePosition) { enemyPosition = ePosition; }

    private Actor currentTarget;
    // Use this for initialization
    new
    // Use this for initialization
    void Start()
    {
        base.Init();
        TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStartActions;
        TurnBehaviour.OnUnitMoved += this.EnemyMoved;

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

        if (enemyList == null)
            enemyList = new Actor[15];
        enemyList[enemyNum] = this;
        enemyID = enemyNum;
        Debug.Log("Enemy added: " + enemyNum + ") " + enemyList[enemyNum]);
        enemyNum++;

        abilitySet = new BasicAttack[4];  //test
        for (int i = 0; i < 4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }

        GameObject[] tempTeam = GameObject.FindGameObjectsWithTag("Player");
        if (tempTeam != null)
        {
            userTeam = new Actor[tempTeam.Length];
            for (int i = 0; i < tempTeam.Length; i++)
                userTeam[i] = tempTeam[i].GetComponent<Actor>();
        }

        //team set to Actors instead of GameObjects  
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        turnControl();
    }

    public void OnDestroy()
    {
        TurnBehaviour.OnUnitMoved -= this.EnemyMoved;
        TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStartActions;
    }

    void turnControl()
    {
        //true player turn ,false enemy turn
        if (SM.checkTurn() == false)
        {
            //enemyTurn();
            //map.drawDebugLines();
        }
    }

    public virtual void EnemyTurnStartActions()
    {
        Debug.Log("1EnemyTurnStart");
        //base.EnemyTurnStart();
        map.selectedUnit = gameObject;
        nearest = findNearestPlayer();
        weakest = findWeakestPlayer();
        enemyPosition = getCoords();
        playerPosition = nearest.getCoords();
        float distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
        reactToProximity(distanceToNearest);
        //Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
        Actor target = nearest;

        if (target != weakest)
            target = findTarget(weakest, distanceToNearest);
        Debug.Log("Found Target = " + target.name + " at " + target.transform.position);
        currentTarget = target;

   
        setMoves(1);

        if (target == null)
            return;

        Vector3 movingTo = PosCloseTo(target.getCoords());
        map.moveActorAsync(gameObject, movingTo);

        //Attack(currentTarget);
    }

    public void EnemyMoved()
    {
        if (SM.checkTurn())
        {
            return;
        }

        Attack(currentTarget);  //attack attempt after move is finished
        SM.setTurn();           //after attacking the enemy will end its turn.
    }


    void enemyTurn()
    {
        //update position
        
        bool finishedMove = moveEnemy(currentTarget);
        if (finishedMove)   //finished move
        {
            Attack(currentTarget);  //attack attempt after move is finished
            SM.setTurn();           //after attacking the enemy will end its turn.
        }
        else
        {

        }
    }

    private Actor findNearestPlayer()
    {
        Actor nearest = userTeam[0];
        float currentNearest = 10000000;
        foreach (Actor user in userTeam)
        {
            //Actor player = user.GetComponent<Actor>();
            //^^not 100% on this due to GetComponent being called up to 10 times. Might build array differently later: Andrew
            enemyPosition = getCoords();
            playerPosition = user.getCoords();
            float distanceFromPlayer = Vector3.Distance(playerPosition, enemyPosition);
            //Debug.Log("Dist = " + distanceFromPlayer + " " + enemyPosition + playerPosition);
            if (distanceFromPlayer < currentNearest)
            {
                nearest = user;
                currentNearest = distanceFromPlayer;
            }
        }
        return nearest;
    }

    private bool reactToProximity(float distanceToNearest)
    {
        if (distanceToNearest <= 1)
        {
            Attack(nearest);
            return true;
        }
        else if (GetHealthPercent() < nearest.GetHealthPercent() && distanceToNearest < moveDistance)
        {
            HealHealth(100);    // just a filler #
            return true;
        }
        else
            return false;
    }

    private Actor findTarget(Actor target, float distanceToNearest)
    {
        Vector3 weakestPosition = weakest.getCoords();
        float distanceToWeakest = Vector3.Distance(weakestPosition, enemyPosition);
        if (distanceToWeakest > moveDistance && distanceToWeakest > 2 * distanceToNearest)
            target = nearest;
        //the idea here is to attack the weakest person unless the nearest person is much closerthan the weakest
        //this is only a greenlight method since range etc will be added
        return target;
    }

    private bool moveEnemy(Actor target)
    {
        if (target == null)
            return false;

        Vector3 movingTo = PosCloseTo(target.getCoords());
        bool isFinshed = map.moveActor(gameObject, movingTo);
        //Debug.Log(target.name+" "+ " " + getMapPosition() + movingTo);
        //after moving, if enemy is in range attack
        //Debug.Log("Dist = " + Vector3.Distance(enemyPosition, playerPosition) + " " + getMapPosition() + movingTo);
        if (Vector3.Distance(enemyPosition, playerPosition) <= 1)
            Attack(target);
        //NextTurn();
        return isFinshed;
    }

    private Actor findWeakestPlayer()
    {
        Actor weakest = userTeam[0];
        float lowestHealth = userTeam[0].GetHealthPercent();
        foreach (Actor user in userTeam)
        {
            //Actor player = user.GetComponent<Actor>();
            //same as findNearest.
            float playerHealth = user.GetHealthPercent();
            if (playerHealth < lowestHealth)
            {
                weakest = user;
                lowestHealth = playerHealth;
            }
        }
        return weakest;
    }

    /// <summary>
    /// TEMP - Calculates the closest map position to target.  Can not move to occupied tile. BUG - does not check if it can walk on returned path.
    /// </summary>
    /// <param name="mapPos">The map/tile position of occupied tile</param>
    /// <returns>Returns closest map/tile position to mapPos, that is not mapPos</returns>
    private Vector3 PosCloseTo(Vector3 mapPos)
    {
        Vector3 output = getCoords() - mapPos;
        output = output.normalized;
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z))
        {
            if (output.x > 0)
                output = new Vector3(1f, 0f, 0f);
            else
                output = new Vector3(-1f, 0f, 0f);
        }
        else
        {
            if (output.z > 0)
                output = new Vector3(0f, 0f, 1f);
            else
                output = new Vector3(0f, 0f, -1f);
        }
        //Debug.Log("Delta "+ output + mapPos);
        output = mapPos + output;
        //Debug.Log("Delta " + output + mapPos);
        return output;
    }


    /// <summary>
    /// //////////////////////// where to add attacking
    /// </summary>
    /// <param name="target"></param>
    void Attack(Actor target)
    {
        float dist = Vector3.Distance(getCoords(), target.getCoords());
        if (!(dist <= 1))
            return;
        //Debug.Log("target = " + target.gameObject + " skill = " + abilitySet[0].abilityName + " range = " + dist);
        abilitySet[0].UseSkill(target.gameObject); //test
    }


    public int GetID()
    {
        return enemyID;
    }
}
