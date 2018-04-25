using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StateMachine SM;
    public TileMap map;
    private static int enemyCount; //number of foes
    public static Actor[] userTeam; // player controlled team
    public static Actor aggro; //not implemented fully yet
    public static Enemy target;
    public static bool targeted, canChangeTarget, aggroAggressive;
    public Actor weakest, nearest; //for attacking together later. not useful now
    public static int enemyNum; // current enemy in enemyList
    public static List<Enemy> enemyList; //Updated to List from array
    public static int currentEnemy = 0;
    public static float aggroRange;
    //public Actor getWeakest() { return weakest; }
    //public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    // Use this for initialization
    void Start()
    {
        EnemyInitialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDestroy()
    {
        TurnBehaviour.OnActorFinishedMove -= this.ExhaustMoves;
       // TurnBehaviour.OnUnitMoved -= this.EnemyUsedAction;
        TurnBehaviour.OnActorAttacked -= this.ExhaustMoves;
        TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStart;
        //TurnBehaviour.OnUnitMoved -= this.EnemyMoveFinished;
        TurnBehaviour.OnEnemyOutOfMoves -= this.EnemyMoveFinished;
    }
    #region primaryActions
    public void EnemyInitialize()
    {
        enemyNum = 0;
        //enemyCount = 2;
        TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStart;
        TurnBehaviour.OnActorFinishedMove += this.ExhaustMoves;
        TurnBehaviour.OnActorAttacked += this.ExhaustMoves;
        //TurnBehaviour.OnUnitMoved += this.EnemyUsedAction;
        TurnBehaviour.OnEnemyOutOfMoves += this.EnemyMoveFinished;

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
            enemyList = new List<Enemy>();
        GameObject[] tempEnemyTeam = GameObject.FindGameObjectsWithTag("Enemy");
        List<Enemy> findOrder = new List<Enemy>();
        foreach (GameObject orderChoice in tempEnemyTeam)
        {
            findOrder.Add(orderChoice.GetComponent<Enemy>());
            enemyNum++;
        }
        findOrder = AddSpecialists(findOrder) ;
        DecideOrder(findOrder);
       /* foreach (Enemy enemy in enemyList)
        {
           // enemyList[enemyNum] = enemy.GetComponent<Enemy>();
            enemy.EnemyInitialize();
            enemyList[enemyNum].setEnemyId(enemyNum);
            Debug.Log("Enemy added: " + enemyNum + ") " + enemyList[enemyNum]);
            enemyNum++;
        }*/
        //enemyCount = enemyNum - 1;
        //DecideOrder();
        GameObject[] tempPlayerTeam = GameObject.FindGameObjectsWithTag("Player");
        if (tempPlayerTeam == null)
        {
            Debug.LogError("No players");
        }
        userTeam = new Actor[tempPlayerTeam.Length];
        for (int playerNum = 0; playerNum < tempPlayerTeam.Length; playerNum++)
        {
            userTeam[playerNum] = tempPlayerTeam[playerNum].GetComponent<Actor>();
            //Debug.Log(userTeam[playerNum]);
        }

    }
    
    private List<Enemy> AddSpecialists(List<Enemy> current)
    {
         GameObject game = GameObject.FindWithTag("Decet") ;
         if(game != null) 
             current.Add(game.GetComponent<"Decet">()) ;
         game = GameObject.FindWithTag("Eery");
         if(game != null) 
             current.Add(game.GetComponent<"Eery">()) ;
         game = GameObject.FindWithTag("Causion");
         if(game != null) 
             current.Add(game.GetComponent<"Causion">()) ;
         GameObject[] games = GameObject.FindGameObjectsWithTag("Support");
         if(games != null) 
              foreach(GameObject game in games) 
                  current.Add(game.GetComponent<"Support">());
         games = GameObject.FindObjectsWithTag("Defender");
         if(games! = null)
              foreach(GameObject game in games) 
                  current.Add(game.GetComponent<"Defender">());
         games = GameObject.FindObjectsWithTag("Tank")
         if(games! = null)
              foreach(GameObject game in games) 
                  current.Add(game.GetComponent<"Tank">());
         games = GameObject.FindObjectsWithTag("Agressive")
         if(games! = null)
              foreach(GameObject game in games) 
                  current.Add(game.GetComponent<"Agressive">());

         return current;
    } 

    public Actor FindWeakestPlayer()
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

    public static Enemy FindWeakestEnemy(Enemy self)
    {
        Enemy weakling = null;
        float lowestHealth = enemyList[0].GetHealthPercent();
        foreach (Enemy enemy in enemyList)
        {
            float playerHealth = enemy.GetHealthPercent();
            if (playerHealth < lowestHealth && enemy != self)
            {
                weakling = enemy;
                lowestHealth = playerHealth;
            }
        }
        return weakling;
    }

    private void DecideOrder(List<Enemy> enemies) //decide which order the enemies attack based on dexterity and
                                                 //initialize in order
    {
        for(int x = 0; x < enemyNum; x++)
        {
            int highest = -1, chosen = 0;
            for (int y = 0; y < enemies.Count; y++)
            {
               if(enemies[y].getDexterity() > highest)
                {
                    highest = enemies[y].getDexterity();
                    chosen = y;
                }
            }
            enemyList.Add(enemies[chosen]);
            enemies.Remove(enemies[chosen]);
            enemyList[x].EnemyInitialize();
            enemyList[x].setEnemyId(x);
            Debug.Log("Enemy added: " + enemyList[x].getEnemyID() + ") " + enemyList[x]);
        }
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
        //UpdateAggro();
        EnemyAction();
    }
    #endregion

    #region eventBasedReactions
    public void EnemyMoveFinished()
    {
        if (SM.checkTurn())
            return;
        Debug.Log("Enemy " + (currentEnemy-1) + " Fisnished Turn");
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
        /*if (currentEnemy != 0 && !map.getTileAtCoord(enemyList[currentEnemy - 1].getCoords()).isOccupied())
            Debug.LogError("TILE NOT SET TO OCCUPIED");*/
        enemyList[currentEnemy].EnemyTurnStartActions();
        ExhaustMoves(SM);
    }

    public void ExhaustMoves()
    {
        ExhaustMoves(SM);
    }
    public static void ExhaustMoves(StateMachine SM)
    {
        if (currentEnemy >= enemyNum || SM.checkTurn())
            return;
        enemyList[currentEnemy].UpdateNearest();
        //Debug.Log("Actually moved to " + enemyList[currentEnemy].getCoords());
        if (enemyList[currentEnemy].getMoves() != 0)
        {
            enemyList[currentEnemy].EnemyActions();
        }
        else
            NextEnemy();
    }

      /* private void EnemyUsedAction()
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
            ExhaustMoves();
        }*/
        
    private static void NextEnemy()
    {
        Debug.Log("Enemy " + currentEnemy + " out of moves");
        currentEnemy++;
        TurnBehaviour.EnemyTurnFinished();
        return;
    }
    public static void CheckTargeted(int id)
    {
        enemyList[id].aggroScore++;
        foreach(Enemy enemy in enemyList)
        {
            if (enemy == null)
                break;
            if (!(enemyList[id].aggroScore - enemy.aggroScore <= 2) || canChangeTarget)
                return;
            else
            {
                targeted = true;
                target = enemyList[id];
            }

        }
    }
    public static bool CheckTargetChange(int id)
    {
        if(!targeted)
            return true;
        if (target.isIncapacitated() && target.getLevel() >= enemyList[id].getLevel())
            return false;
        else
            return true;
    }
    public static void UpdateAggro()
    {
        int score = 0, secondScore=0;
        Actor second = null;
        foreach (Actor player in userTeam)
        {
            if (score < player.aggroScore)
            {
                aggro = player;
            }
            else if(secondScore < player.aggroScore)
            {
                second = player;
            }
        }
        if (aggro.aggroScore - second.aggroScore > 2)// might be temp if aggro changes drastically
            aggroAggressive = true;
        else
            aggroAggressive = false;
        UpdateAggroRange();
    }
    public static void UpdateAggroRange()
    {
        aggroRange = 0;
        foreach(Ability ability in aggro.abilitySet)
        {
            if(ability.range_max > aggroRange)
            {
                aggroRange = ability.range_max;
            }
        }
    }
    /* private void EnemyMoved()
     {
         if (SM.checkTurn())
             return;

         EnemyAction();
     }*/
    #endregion

}
