using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StateMachine SM;
    public TileMap map;
    public static Actor[] userTeam;
    public Actor weakest, nearest; //for attacking together later. not useful now
    public static int enemyNum;
    public static Enemy[] enemyList;
    public static int currentEnemy = 0;
    //public Actor getWeakest() { return weakest; }
    //public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    // Use this for initialization
    void Start () {
        EnemyInitialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDestroy()
    {
        TurnBehaviour.OnUnitMoved -= this.EnemyUsedAction;
        TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStart;
        //TurnBehaviour.OnUnitMoved -= this.EnemyMoveFinished;
        TurnBehaviour.OnEnemyOutOfMoves -= this.EnemyMoveFinished;
        TurnBehaviour.OnActorAttacked -= this.EnemyUsedAction;
    }

    public void EnemyInitialize()
    {
        enemyNum = 0;
        TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStart;
        TurnBehaviour.OnUnitMoved += this.EnemyUsedAction;
        TurnBehaviour.OnEnemyOutOfMoves += this.EnemyMoveFinished;
        TurnBehaviour.OnActorAttacked += this.EnemyUsedAction;

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }
        if (GameObject.FindWithTag("GameController") == null)
        {
            Debug.LogError("Missing Game Controller, add in scene hierarchy");
            return;
        }
        SM = GameObject.FindWithTag("GameController").GetComponent<StateMachine>();
        if (enemyList == null)
            enemyList = new Enemy[15];
        GameObject[] tempEnemyTeam = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in tempEnemyTeam)
        {
            enemyList[enemyNum] = enemy.GetComponent<Enemy>();
            enemyList[enemyNum].EnemyInitialize();
            enemyList[enemyNum].setEnemyId(enemyNum);
            Debug.Log("Enemy added: " + enemyNum + ") " + enemyList[enemyNum]);
            enemyNum++;   
        }
        GameObject[] tempPlayerTeam = GameObject.FindGameObjectsWithTag("Player");
        if (tempPlayerTeam == null)
        {
            Debug.LogError("No players");
        }
        userTeam = new Actor[tempPlayerTeam.Length];
        for (int playerNum = 0; playerNum < tempPlayerTeam.Length; playerNum++ )
        {
            userTeam[playerNum] = tempPlayerTeam[playerNum].GetComponent<Actor>();
            //Debug.Log(userTeam[playerNum]);
        }
    }

    public Actor findWeakestPlayer()
    {
        //Actor[] users = PlayerControlled.playerList;
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

    public void EnemyTurnStart()
    {
       /* if (SM.checkTurn())
        {
            Debug.Log("Error with enemy #");
            return;
        }*/
        Debug.Log("1EnemyTurnStart");
        //int[] attackOrder = DecideOrder()
        //also need to add some advanced decision making for attacking a target together #notfirstplayable
        EnemyAction();
    }

    public void EnemyMoveFinished()
    {
        Debug.Log("Enemy " + (currentEnemy - 1) + " Fisnished Turn");
        EnemyAction();
    }
    private void EnemyAction()
    {
        //Debug.Log("called " + EnemyController.enemyNum + " " + currentEnemy);
        if (enemyList == null || currentEnemy >= EnemyController.enemyNum || enemyList[currentEnemy] == null)
        {
            Debug.Log("No more enemies");
            currentEnemy = 0;
            SM.setTurn();
            return;
        }
        if (currentEnemy != 0 && !map.getTileAtCoord(enemyList[currentEnemy - 1].getCoords()).isOccupied())
           Debug.LogError("TILE NOT SET TO OCCUPIED");
        enemyList[currentEnemy].EnemyTurnStartActions();
        EnemyUsedAction();
        /*while (enemyList[currentEnemy].getMoves() != 0)
        {
            while (enemyList[currentEnemy].reactToProximity(enemyList[currentEnemy].distanceToNearest))
            {
                if (enemyList[currentEnemy].getMoves() == 0)
                {
                    Debug.Log("Enemy " + currentEnemy + " out of moves");
                    break;
                }
            }
            enemyList[currentEnemy].nonProximityActions();
        }*/
        //currentEnemy++;
        //TurnBehaviour.EnemyTurnFinished();
    }

    private void EnemyUsedAction()
    {
        if (SM.checkTurn())
            return;
        //Debug.Log("USED ACTION WORKING");
        if (enemyList[currentEnemy].getMoves() == 0)
        {
            Debug.Log("Enemy " + currentEnemy + " out of moves");
            NextEnemy();
            return;
        } 
        if (enemyList[currentEnemy].reactToProximity(enemyList[currentEnemy].distanceToNearest))
            return;
        else
            enemyList[currentEnemy].nonProximityActions();
    }

    private void NextEnemy()
    {
        if (SM.checkTurn() || enemyList[currentEnemy].getMoves() != 0)
            return;
        currentEnemy++;
        TurnBehaviour.EnemyTurnFinished();
    }
   /* private void EnemyMoved()
    {
        if (SM.checkTurn())
            return;
        
        EnemyAction();
    }*/
    private int[] DecideOrder() //decide which order the enemies attack in and return array of enemyID
    {
        int[] attackOrder = new int[enemyList.Length];
        return attackOrder;
    }
}
