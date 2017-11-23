using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************
Enemy
This is the parent class of all enemies
*****************/

public class Enemy : Actor
{
    private int enemyID;
    private Actor nearest, weakest;
    private Vector3 playerPosition, enemyPosition;
    public float distanceToNearest;
    public Actor getNearest() { return nearest; }
    public void setNearest(Actor nearestPlayer) { nearest = nearestPlayer; }
    public Vector3 getPlayerPosition() { return playerPosition; }
    public void setPlayerPosition(Vector3 pPosition) { playerPosition = pPosition; }
    public Vector3 getEnemyPosition() { return enemyPosition; }
    public void setEnemyPosition(Vector3 ePosition) { enemyPosition = ePosition; }
    public void setEnemyId(int id) { enemyID = id; }
    public int getEnemyID(int id) { return enemyID; }
    public Actor getWeakest() { return weakest; }
    public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    private int expGiven;

    private Actor currentTarget;
    // Use this for initialization
    new
    // Use this for initialization
    void Start()
    {
        EnemyInitialize();

        //team set to Actors instead of GameObjects  
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        TurnBehaviour.OnUnitMoved -= this.EnemyMoved;
        //TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStart;
    }

    public void EnemyInitialize()
    {
        base.Init();
        expGiven = 10;
        //TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStartActions;
       TurnBehaviour.OnUnitMoved += this.EnemyMoved;

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

        abilitySet = new BasicAttack[4];  //test
        for (int i = 0; i < 4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        turnControl();
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
        Debug.Log("Enemy " + enemyID + " moving");
        if (GetHealthPercent() == 0f)
        {
            // SM.setTurn();
            return;
        }
        //base.EnemyTurnStart();
        map.selectedUnit = gameObject;
        nearest = findNearestPlayer();
        weakest = findWeakestPlayer();
        //Debug.Log(weakest);
        enemyPosition = getCoords();
        playerPosition = weakest.getCoords();
        distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
    }
    /* if (reactToProximity(distanceToNearest))
     {
         if (getMoves() == 0)
             return;
         else
             reactToProximity(distanceToNearest);
     }*/
    //Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
    public void nonProximityActions()
    {
        if (getMoves() == 0)
            return;
        Actor target = nearest;
        if (target != weakest)
            target = findTarget(weakest, distanceToNearest);
        //Debug.Log("Found Target = " + target.name + " at " + target.transform.position + target.getCoords());
        currentTarget = target;    

        if (target == null)
        {
            Debug.LogError("no player team");
            return;
        }
           
        Vector3 movingTo = PosCloseTo(target.getCoords());
        //Debug.Log("Moving to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        Debug.Log("Move Complete");
        //Attack(currentTarget);
    }

    public void EnemyMoved()
    {

        //Debug.Log(SM.checkTurn() + " " + EnemyController.currentEnemy + " " + enemyID);
        if (SM.checkTurn() || EnemyController.currentEnemy != enemyID)
        {
            //if(!SM.checkTurn())
            //Debug.LogError("problem with eID"+ (EnemyController.currentEnemy-1)+ " " +enemyID);
            return;
        }
        else
        {
            //Debug.Log("problem with remaining moves " + getMoves());
            if (getMoves() != 0)
            attemptAttack(currentTarget);  //attack attempt after move is finished
            //SM.setTurn();           //after attacking the enemy will end its turn.
            //Debug.Log("Called");
        }
    }


    /*void enemyTurn()
    {
        //update position
        
        bool finishedMove = moveEnemy(currentTarget);
        if (finishedMove)   //finished move
        {
            attemptAttack(currentTarget);  //attack attempt after move is finished
           // SM.setTurn();           //after attacking the enemy will end its turn.
        }
        else
        {
            Debug.LogError("stupid code");
        }
    }*/

    private Actor findNearestPlayer()
    {
        Actor nearest = null;
        float currentNearest = 10000000;
        //Actor[] userTeam = EnemyController.userTeam;
        foreach (Actor user in EnemyController.userTeam)
        {
            enemyPosition = getCoords();
            if(user == null)
            {
                Debug.LogError("null user");
                return null;
            }
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

    public Actor findWeakestPlayer()
    {
        //Actor[] users = PlayerControlled.playerList;
        weakest = EnemyController.userTeam[0];
        float lowestHealth = weakest.GetHealthPercent();
        foreach (Actor user in EnemyController.userTeam)
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

    public bool reactToProximity(float distanceToNearest)
    {
        //Debug.Log(distanceToNearest);
        if (distanceToNearest <= 1.5)
        {
            Debug.Log("Attempting Attack");
            if (attemptAttack(nearest))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        //Debug.Log(target.coords);
        Vector3 weakestPosition = target.getCoords();
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
        //if (Vector3.Distance(enemyPosition, playerPosition) <= 1)
        //    Attack(target);
        //NextTurn();
        return isFinshed;
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
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z)) //attempts to get to the closest available tile then checks all other close pos'
        {
           // if (output.x > 0)
                output = new Vector3(1f, 0f, 0f);
            if (!map.UnitCanEnterTile(mapPos + output))
            {
                //  else
                output = new Vector3(-1f, 0f, 0f);
                if (!map.UnitCanEnterTile(mapPos + output))
                {
                    output = new Vector3(0f, 0f, 1f);
                    if (!map.UnitCanEnterTile(mapPos + output))
                    {
                        output = new Vector3(0f, 0f, -1f);
                    }
                }
            }
        }
        else
        {
            output = new Vector3(0f, 0f, 1f);
            if (!map.UnitCanEnterTile(mapPos + output))
            {
                output = new Vector3(0f, 0f, -1f);
                if (!map.UnitCanEnterTile(mapPos + output))
                {
                    output = new Vector3(1f, 0f, 0f);
                    if (!map.UnitCanEnterTile(mapPos + output))
                    {
                        output = new Vector3(-1f, 0f, 0f);
                    }
                }
            }
        }
        //Debug.Log("Delta "+ output + mapPos);
        output = mapPos + output;
        Debug.Log("Delta " + output);
        //Debug.Log(map.getTileAtCoord(output).isOccupied());
        if(EnemyController.currentEnemy >0)
        Debug.Log("first enemy " +EnemyController.enemyList[EnemyController.currentEnemy].getCoords());
        return output;
    }


    /// <summary>
    /// //////////////////////// where to add attacking
    /// </summary>
    /// <param name="target"></param>
    public bool attemptAttack(Actor target)
    {
        Debug.Log(this + " Attempting attack on " + target);
        if (abilitySet[0].SkillInRange(getCoords(), target.getCoords()))
        {
            //float dist = Vector3.Distance(getCoords(), target.getCoords());
            //if (!(dist <= 1.5))
              //  return;
            //Debug.Log("target = " + target.gameObject + " skill = " + abilitySet[0].abilityName + " range = " + dist);
            abilitySet[0].UseSkill(target.gameObject); //test
                                                       //status change will occur here^^
            return true;
        }
        else
        {
            Debug.Log("Out of Range " + target.getCoords() + " " + getCoords());
            return false;
        }
    }

    public int getExpGiven()
    {
        return expGiven;
    }

    public int GetID()
    {
        return enemyID;
    }
}
