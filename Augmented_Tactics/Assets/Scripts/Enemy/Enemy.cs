using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private Actor[] userTeam;
    //private int callControl = 0;
    public static int enemyNum;
    public static Actor[] enemyList;
    private Actor nearest, weakest;
    private Vector3 playerPosition, enemyPosition;
    //private float distanceToNearest, distanceToWeakest;
    public Actor getNearest() { return nearest; }
    public void setNearest(Actor nearestPlayer){nearest = nearestPlayer; }
    public Actor getWeakest() { return weakest; }
    public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    public Vector3 getPlayerPosition() { return playerPosition; }
    public void setPlayerPosition(Vector3 pPosition) { playerPosition = pPosition; }
    public Vector3 getEnemyPosition() { return enemyPosition; }
    public void setEnemyPosition(Vector3 ePosition) { enemyPosition = ePosition; } 



    // Use this for initialization
    void Start () {
	    base.Start();


        if (enemyNum == null)
            enemyNum = 0;
        if (enemyList == null)
            enemyList = new Actor[15];
        enemyList[enemyNum] = this;
        Debug.Log("Player added: " + enemyNum + ") " + enemyList[enemyNum]);
        enemyNum++;

        abilitySet = new BasicAttack[4];  //test
        for (int i = 0; i <4; i++)
        {
            abilitySet[i] = new BasicAttack();
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
       // map.selectedUnit = gameObject;
        nearest = findNearestPlayer().GetComponent<Actor>();
        weakest = findWeakestPlayer().GetComponent<Actor>();
        enemyPosition = new Vector3((float)tileX, 0, (float)tileZ);
        playerPosition = new Vector3((float)nearest.tileX, 0, (float)nearest.tileZ);
        float distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
        reactToProximity(distanceToNearest);
        //Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
        Actor target = nearest;
        
        if (target != weakest)
            target = findTarget(weakest, distanceToNearest);
        //Debug.Log("found target");
        moveEnemy(target);
    }
    private Actor findNearestPlayer()
    {
        Actor nearest = userTeam[0] ;
        float currentNearest = 10000000;
        foreach(Actor user in userTeam)
        {
            //Actor player = user.GetComponent<Actor>();
            //^^not 100% on this due to GetComponent being called up to 10 times. Might build array differently later: Andrew
            playerPosition = new Vector3(user.tileX, user.tileZ, 0);
            enemyPosition = new Vector3(tileX, tileZ, 0);
            float distanceFromPlayer = Vector3.Distance(playerPosition, enemyPosition);
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
        Vector3 weakestPosition = new Vector3((float)weakest.tileX, 0, (float)weakest.tileZ);
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
        map.GeneratePathTo(target.tileX, target.tileZ);
        /*Debug.Log(target.name+" "+ target.GetComponent<Actor>().tileX+" "+ target.GetComponent<Actor>().tileZ);
        //after moving, if enemy is in range attack*/
        if (Vector3.Distance(enemyPosition, playerPosition) <= 1)
            Attack(target);
        NextTurn();
        return true;
    }

    private Actor findWeakestPlayer()
    {
        Actor weakest = userTeam[0];
        float lowestHealth = userTeam[0].GetComponent<Actor>().GetHealthPercent();
        foreach (Actor user in userTeam)
        {
            //Actor player = user.GetComponent<Actor>();
            //same as findNearest.
            float playerHealth = user.GetHealthPercent();
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
        Debug.Log("target = " +target.gameObject+ "\n" + abilitySet[0].abilityName);
        abilitySet[0].UseSkill(target.gameObject); //test
    }
 
}
