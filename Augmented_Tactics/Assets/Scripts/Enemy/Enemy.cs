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
        //TurnBehaviour.OnUnitMoved -= this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyUnitAttack -= this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStart;
    }

    public void EnemyInitialize()
    {
        base.Init();
        expGiven = 10;
        //TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStartActions;
        TurnBehaviour.OnUnitMoved += this.EnemyMoved;
        //TurnBehaviour.OnUnitMoved += this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyUnitAttack += this.EnemyUsedAction;


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
       // turnControl();
    }

    /*void turnControl()
    {
        //true player turn ,false enemy turn
        if (SM.checkTurn() == false)
        {
            //enemyTurn();
            //map.drawDebugLines();
        }
    }*/

    public virtual void EnemyTurnStartActions()
    {
        Debug.Log("Enemy " + enemyID + " turn started");
        if (GetHealthPercent() == 0f)
        {
            //SM.setTurn();
            return;
        }
        //base.EnemyTurnStart();
        //map.selectedUnit = gameObject;
        nearest = findNearestPlayer();
        weakest = findWeakestPlayer();
        //Debug.Log(weakest);
        enemyPosition = getCoords();
        playerPosition = weakest.getCoords();
        distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);

        //if (reactToProximity(distanceToNearest))
        //{
        //    SM.setTurn();
        //    return;
        //}
        ////Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
        //Actor target = nearest;

        //if (target != weakest)
        //    target = findTarget(weakest, distanceToNearest);
        ////Debug.Log("Found Target = " + target.name + " at " + target.transform.position + target.getCoords());
        //currentTarget = target;    

        //if (target == null)
        //{
        //    Debug.LogError("no player team");
        //    return;
        //}
           

        //Vector3 movingTo = PosCloseTo(target.getCoords());
        ////Debug.Log("Moving to " + movingTo);
        //map.moveActorAsync(gameObject, movingTo);
        ////Attack(currentTarget);

        //if (map.IsValidCoord(coords) == true)
        //{
        //    Debug.Log("Coords: " + coords);
        //    map.GetTileAt(coords).setOccupiedTrue();
        //    Debug.Log("Occupied = " + map.GetTileAt(coords).isOccupied());
        //}

    }

    public void nonProximityActions()
    {
        Debug.Log("NON-PROXIMITY");
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
        Vector3 movingTo = PosCloseTo(target.getCoords(), 0);
        if (movingTo == new Vector3(0, 0, 0))
        {
            movingTo = PosCloseTo(target.getCoords(), 1);
            if (movingTo == new Vector3(0, 0, 0))
            {
                Debug.Log("No possible move available, switching target."); 
                //need to add contingency for enemy surrounded or unavailable
                return;
            }
                
        }
        Debug.Log("Moving " + this + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        UpdateNearest();
        //Debug.Log("Move Complete\t" + currentTarget);
    
    }

    public void EnemyMoved()
    {

        //Debug.Log(EnemyController.currentEnemy + " " + enemyID);
        if (SM.checkTurn() || EnemyController.currentEnemy != enemyID)
        {
            return;
        }
        else
        {
            if (getMoves() != 0)
                attemptAttack(currentTarget);  //attack attempt after move is finished
            //SM.setTurn();           //after attacking the enemy will end its turn.
            //Debug.Log("Called");
        }
    }


    //void enemyTurn()
    //{
    //    //update position
        
    //    bool finishedMove = moveEnemy(currentTarget);
    //    if (finishedMove)   //finished move
    //    {
    //        Attack(currentTarget);  //attack attempt after move is finished
    //        SM.setTurn();           //after attacking the enemy will end its turn.
    //    }
    //    else
    //    {
    //        Debug.LogError("stupid code");
    //    }
    //}

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
            if (distanceFromPlayer < currentNearest && !user.isDead())
            {
                nearest = user;
                currentNearest = distanceFromPlayer;
            }
        }
        return nearest;
    }
    private void UpdateNearest()
    {
        findNearestPlayer();
        distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);
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
            if (playerHealth < lowestHealth && !user.isDead())
            {
                weakest = user;
                lowestHealth = playerHealth;
            }
        }
        return weakest;
    }

    public bool reactToProximity(float distanceToNearest)
    {
       // Debug.Log(distanceToNearest);
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
            Debug.Log("Healing");
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
        Vector3 movingTo = PosCloseTo(target.getCoords(), 0);
        if (movingTo == new Vector3(0, 0, 0))
        {
            movingTo = PosCloseTo(target.getCoords(), 1);
            if (movingTo == new Vector3(0, 0, 0))
                return false;
        }
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
    /* private Vector3 PosCloseTo(Vector3 mapPos)
     {
         Vector3 output = getCoords() - mapPos;
         output = output.normalized;
         float absX = Mathf.Abs(output.x), absZ = Mathf.Abs(output.z);

         if (absX > absZ) //attempts to get to the closest available tile then checks all other close pos'
         {
             // if (output.x > 0)
             if (absX < mapPos.x)
             {
                 output = new Vector3(1f, 0f, 0f);
                 if (!map.UnitCanEnterTile(mapPos + output))
                 {
                     if (absZ < mapPos.z)
                     {
                         output = new Vector3(0f, 0f, 1f);
                         if (!map.UnitCanEnterTile(mapPos + output))
                         {
                             output = new Vector3(0f, 0f, -1f);
                             if (!map.UnitCanEnterTile(mapPos + output))
                             {
                                 output = new Vector3(-1f, 0f, 0f);
                             }
                         }
                     }
                 }
             }
             else
             {
                 output = new Vector3(-1f, 0f, 0f);
                 if (!map.UnitCanEnterTile(mapPos + output))
                 {
                     if (absZ < mapPos.z)
                     {
                         output = new Vector3(0f, 0f, 1f);
                         if (!map.UnitCanEnterTile(mapPos + output))
                         {
                             output = new Vector3(0f, 0f, -1f);
                             if (!map.UnitCanEnterTile(mapPos + output))
                             {
                                 output = new Vector3(1f, 0f, 0f);
                             }
                         }
                     }
                 }
             }
         }
         else
         {
             //closer to Z
             if (absZ < mapPos.z)
             {
                 output = new Vector3(0f, 0f, 1f);
                 if (!map.UnitCanEnterTile(mapPos + output))
                 {
                     if (absX < mapPos.x)
                     {
                         output = new Vector3(1f, 0f, 0f);
                         if (!map.UnitCanEnterTile(mapPos + output))
                         {
                             output = new Vector3(-1f, 0f, 0f);
                             if (!map.UnitCanEnterTile(mapPos + output))
                             {
                                 output = new Vector3(0f, 0f, -1f);
                             }
                         }
                     }
                 }
             }
             else
             {
                 output = new Vector3(0f, 0f, -1f);
                 if (!map.UnitCanEnterTile(mapPos + output))
                 {
                     if (absX < mapPos.x)
                     {
                         output = new Vector3(1f, 0f, 0f);
                         if (!map.UnitCanEnterTile(mapPos + output))
                         {
                             output = new Vector3(-1f, 0f, 0f);
                             if (!map.UnitCanEnterTile(mapPos + output))
                             {
                                 output = new Vector3(0f, 0f, 1f);
                             }
                         }
                     }
                 }
             }
         }

         //Debug.Log("Delta "+ output + mapPos);
         output = mapPos + output;
         Debug.Log("Delta " + output);
         //Debug.Log(map.getTileAtCoord(output).isOccupied());
         if (EnemyController.currentEnemy > 0)
             Debug.Log("first enemy " + EnemyController.enemyList[EnemyController.currentEnemy].getCoords());
         return output;
     }*/
    private Vector3 PosCloseTo(Vector3 mapPos, int attemptNum)
    {
        Vector3 output = getCoords() - mapPos;
        output = output.normalized;
        if (output.x > 0 && attemptNum != 1)
        {
            if (output.z > 0 )
                return PosCloseTo("rightup", mapPos);
            else
                return PosCloseTo("rightdown", mapPos);
        }
        else
        {
            if (output.z > 0)
                return PosCloseTo("leftup", mapPos);
            else
                return PosCloseTo("leftdown", mapPos);
        }
    }

    private Vector3 PosCloseTo(string directions, Vector3 pos)
    {
        if(directions == "rightup")
           return checkDirections(pos + new Vector3(1f, 0f, 0f), pos + new Vector3(0f, 0f, 1f));
        else if (directions == "rightdown")
            return checkDirections(pos + new Vector3(1f, 0f, 0f), pos + new Vector3(0f, 0f, -1f));
        else if (directions == "leftup")
            return checkDirections(pos + new Vector3(-1f, 0f, 0f), pos + new Vector3(0f, 0f, 1f));
        else
            return checkDirections(pos + new Vector3(-1f, 0f, 0f), pos + new Vector3(0f, 0f, -1f));
    }

    private Vector3 checkDirections(Vector3 firstDir, Vector3 secDir)
    {
        if (map.UnitCanEnterTile(firstDir))
            return firstDir;
        else if (map.UnitCanEnterTile(secDir))
            return secDir;
        else
            return new Vector3(0,0,0); //no move available
    }
        /*private void EnemyUsedAction()
        {
            if (SM.checkTurn())
                return;
            //Debug.Log("USED ACTION WORKING");
            if (EnemyController.enemyList[EnemyController.currentEnemy].getMoves() == 0)
            {
                Debug.Log("Enemy " + EnemyController.currentEnemy + " out of moves");
                EnemyController.NextEnemy(SM);
                return;
            }
            EnemyController.ExhaustMoves();
        }*/
        /// <summary>
        /// //////////////////////// where to add attacking
        /// </summary>
        /// <param name="target"></param>
        public bool attemptAttack(Actor target)
    {
        if (SM.checkTurn() || EnemyController.currentEnemy != enemyID)
            return false;
        Debug.Log(this + " Attempting attack on " + target);
        int bestAttack = 0, choice = 0;
        bool chosen = false;
        for (int ability = 0; ability < 4; ability++)
        {
            if (abilitySet[ability].damage > bestAttack && abilitySet[ability].SkillInRange(getCoords(), target.getCoords()))
            {
                bestAttack = abilitySet[ability].damage;
                choice = ability;
                chosen = true;
            }
        }
        //float dist = Vector3.Distance(getCoords(), target.getCoords());
        //if (!(dist <= 1.5))
        //  return;
        //Debug.Log("target = " + target.gameObject + " skill = " + abilitySet[0].abilityName + " range = " + dist);
        if (chosen)
        { 
            abilitySet[choice].UseSkill(target.gameObject); //test
            TurnBehaviour.EnemyHasJustAttacked();
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
