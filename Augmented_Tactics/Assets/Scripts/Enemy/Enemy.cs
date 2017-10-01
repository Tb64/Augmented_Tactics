using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private GameObject[] userTeam;
    //private int callControl = 0;
    private int enemyID;
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

    private Actor currentTarget;
    public static int enemyNum;
    public static Actor[] enemyList;
    // Use this for initialization
    void Start () {
	    base.Start();


        if(map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

        if (enemyNum == null)
            enemyNum = 0;
        if (enemyList == null)
            enemyList = new Actor[15];
        enemyList[enemyNum] = this;
        enemyID = enemyNum;
        Debug.Log("Enemy added: " + enemyNum + ") " + enemyList[enemyNum]);
        enemyNum++;

        abilitySet = new BasicAttack[4];  //test
        for (int i = 0; i <4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }

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
            map.drawDebugLines();
            map.moveUnit();
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

    public override void EnemyTurnStart()
    {
        base.EnemyTurnStart();

        map.selectedUnit = gameObject;
        nearest = findNearestPlayer().GetComponent<Actor>();
        weakest = findWeakestPlayer().GetComponent<Actor>();
        if (GetHealthPercent() < nearest.GetHealthPercent())
            HealHealth(100);    // just a filler #
        //Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
        Actor target = nearest;
        enemyPosition = new Vector3((float)tileX, 0, (float)tileZ);
        playerPosition = new Vector3((float)target.tileX, 0, (float)target.tileZ);
        float distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
        if (target != weakest)
            target = findTarget(weakest, distanceToNearest);
        Debug.Log("Found Target = " + target.name + " at " + target.transform.position);
        currentTarget = target;
    }

    void enemyTurn()
    {
        moveEnemy(currentTarget);
    }
    private GameObject findNearestPlayer()
    {
        GameObject nearest = userTeam[0] ;
        float currentNearest = 10000000;
        foreach(GameObject user in userTeam)
        {
            Actor player = user.GetComponent<Actor>();
            //^^not 100% on this due to GetComponent being called up to 10 times. Might build array differently later: Andrew
            playerPosition = new Vector3(player.tileX, player.tileZ, 0);
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
        Debug.Log("target = " +target.gameObject+ "\n" + abilitySet[0].abilityName);
        abilitySet[0].UseSkill(target.gameObject); //test
    }


    public int GetID()
    {
        return enemyID;
    }
}
