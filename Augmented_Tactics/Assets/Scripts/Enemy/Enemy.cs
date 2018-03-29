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
    private List<Actor> cantTarget;
    private bool targetLocked;

    public Actor currentTarget;
    // Use this for initialization
    // Use this for initialization
    new public virtual void Start()
    {
        EnemyInitialize();

        //team set to Actors instead of GameObjects  
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        //TurnBehaviour.OnUnitMoved -= this.EnemyMoved;
        //TurnBehaviour.OnUnitMoved -= this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyUnitAttack -= this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyTurnStart -= this.EnemyTurnStart;
    }

    public void EnemyInitialize()
    {
        base.Init();
        expGiven = 10;
        //TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStartActions;
        //TurnBehaviour.OnUnitMoved += this.EnemyMoved;
        //TurnBehaviour.OnUnitMoved += this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyUnitAttack += this.EnemyUsedAction;


        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

        abilitySet = new Ability[4];
        /*updating for using varied attacks
         update for specific character needs to be added to every
         type of enemy as they are created to load correct attacks*/
         
        //FOR DEMO ONLY
        abilitySet[0] = SkillLoader.LoadSkill("basicattack", gameObject);
        abilitySet[1] = SkillLoader.LoadSkill("heal", gameObject);
        abilitySet[2] = SkillLoader.LoadSkill("fire", gameObject);
        abilitySet[3] = SkillLoader.LoadSkill("combo", gameObject);
        setManaCurrent(30);
        setMaxMana(30);
        setHealthCurrent(20);
        setMaxHealth(20);
        setWisdom(15);
        setDexterity(25);
        setCharisma(10);
        setConstitution(14);
        setIntelligence(30);
        //FOR DEMO ON:Y

        /*for (int i = 0; i < 4; i++)
        {
            abilitySet[i] = new BasicAttack(gameObject);
        }*/
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
        cantTarget = new List<Actor>();
        targetLocked = false;
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
        playerPosition = nearest.getCoords();
        distanceToNearest = Vector3.Distance(playerPosition, enemyPosition);

        //if (reactToProximity(distanceToNearest))
        //{
        //    SM.setTurn();
        //    return;
        //}
        ////Debug.Log(nearest.tileX + " " + nearest.tileZ+ " " + weakest.tileX + " "+ weakest.tileZ);
        //Actor currentTarget = nearest;

        //if (currentTarget != weakest)
        //    currentTarget = findTarget(weakest, distanceToNearest);
        ////Debug.Log("Found Target = " + currentTarget.name + " at " + currentTarget.transform.position + currentTarget.getCoords());
        //currentTarget = currentTarget;    

        //if (currentTarget == null)
        //{
        //    Debug.LogError("no player team");
        //    return;
        //}


        //Vector3 movingTo = PosCloseTo(currentTarget.getCoords());
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

    public virtual void EnemyActions()
    {
        //Debug.Log("NON-PROXIMITY");
        if (getMoves() == 0)
            return;
        //add tree 
        if (!targetLocked)
        {
            currentTarget = nearest;
            if (currentTarget != weakest || cantTarget.Contains(currentTarget) || currentTarget.isIncapacitated() || currentTarget.isDead())
                findTarget();
            //Debug.Log("Found Target = " + currentTarget.name + " at " + currentTarget.transform.position + currentTarget.getCoords());
            //currentTarget = currentTarget;
        }
 
        if (currentTarget == null)
        {
            Debug.LogError("no player team");
            return;
        }
        if (AttemptAttack())
            return;
        Vector3 movingTo = PosCloseTo(currentTarget.getCoords());
        if (movingTo == new Vector3(-1, -1, -1))
        {
            // movingTo = PosCloseTo(currentTarget.getCoords());
            //if (movingTo == new Vector3(0, 0, 0))
            //{
            Debug.Log("No possible move available, switching currentTarget.");
            cantTarget.Add(currentTarget);
            EnemyActions();
            return;
            //}

        }
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        
        //Debug.Log("Move Complete\t" + currentTarget);

    }

    /*public void EnemyMoved()
    {

        Debug.Log(EnemyController.currentEnemy + " " + enemyID);
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
            EnemyController.ExhaustMoves(SM);
        }
    }*/


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
            if (user == null)
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
    public void UpdateNearest()
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

    /*public bool reactToProximity(float distanceToNearest)
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
    }*/

    private void findTarget()
    {
        //Debug.Log(currentTarget.coords);
        currentTarget = weakest;
        Vector3 weakestPosition = currentTarget.getCoords();
        float distanceToWeakest = Vector3.Distance(weakestPosition, enemyPosition);
        if (!nearest.isDead() && !nearest.isIncapacitated() && !cantTarget.Contains(nearest) && (distanceToWeakest > moveDistance || distanceToWeakest > 1.2 * distanceToNearest))
        {
            currentTarget = nearest;
            targetLocked = true;
            Debug.Log("Targeted Nearest");
            return;
        }
        else if (!cantTarget.Contains(weakest) && !weakest.isDead() && !weakest.isIncapacitated())
        {
            currentTarget = weakest;
            targetLocked = true;
            Debug.Log("Targeted Weakest");
            return;
        }
        else
        {
            Actor[] userTeam = EnemyController.userTeam;
            for (int player = 0; player < 4; player++)
            {
                if (!cantTarget.Contains(userTeam[player]) && !userTeam[player].isDead() && !userTeam[player].isIncapacitated())
                {
                    currentTarget = userTeam[player];
                    targetLocked = true;
                    return;
                }
            }
            Debug.LogError("Absolutely No Good Move Available. Skipping Turn");
        }
    }

    /*private bool moveEnemy()
    {
        if (currentTarget == null)
            return false;
        Vector3 movingTo = PosCloseTo(currentTarget.getCoords());
        if (movingTo == new Vector3(-1, -1, -1))
        {
            movingTo = PosCloseTo(currentTarget.getCoords());
            if (movingTo == new Vector3(-1, -1, -1))
                return false;
        }
        bool isFinshed = map.moveActor(gameObject, movingTo);
        //Debug.Log(currentTarget.name+" "+ " " + getMapPosition() + movingTo);
        //after moving, if enemy is in range attack
        //Debug.Log("Dist = " + Vector3.Distance(enemyPosition, playerPosition) + " " + getMapPosition() + movingTo);
        //if (Vector3.Distance(enemyPosition, playerPosition) <= 1)
        //    Attack(currentTarget);
        //NextTurn();
        return isFinshed;
    }
    */


    /// <summary>
    /// TEMP - Calculates the closest map position to currentTarget.  Can not move to occupied tile. BUG - does not check if it can walk on returned path.
    /// </summary>
    /// <param name="mapPos">The map/tile position of occupied tile</param>
    /// <returns>Returns closest map/tile position to mapPos, that is not mapPos</returns>
    /* {
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
    public Vector3 PosCloseTo(Vector3 mapPos)
    {
        Vector3 output = getCoords() - mapPos;
        output = output.normalized;
        if (output.x >= 0)
        {
            if (output.z >= 0)
            {
                Vector3 temp = PosCloseTo("rightup", mapPos);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("leftdown");
                    return PosCloseTo("leftdown", mapPos);
                }
                else
                {
                    Debug.Log("rightup");
                    return temp;
                }
                    
            }
            else
            {
                Vector3 temp = PosCloseTo("rightdown", mapPos);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("leftup");
                    return PosCloseTo("leftup", mapPos);

                }
                else
                {
                    Debug.Log("rightdown");
                    return temp;
                }
                    
            }
        }
        else
        {
            if (output.z >= 0)
            {
                Vector3 temp = PosCloseTo("leftup", mapPos);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("rightdown");
                    return PosCloseTo("rightdown", mapPos);
                }
                else
                {
                    Debug.Log("leftup");
                    return temp;
                }
            }
            else
            {
                Vector3 temp = PosCloseTo("leftdown", mapPos);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("rightup");
                    return PosCloseTo("rightup", mapPos);
                }
                else
                {
                    Debug.Log("leftdown");
                    return temp;
                }
            }
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
            return new Vector3(-1,-1,-1); //no move available
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
        /// <param name="currentTarget"></param>
        public virtual bool AttemptAttack() //thinkiing of changing to attemptAction. Also covers heal
        {
            if (SM.checkTurn() || EnemyController.currentEnemy != enemyID)
                return false;
            Debug.Log(this + " Attempting attack on " + currentTarget + " at " + currentTarget.getCoords());
            int bestAttack = 0, choice = 0;
            bool chosen = false;
            for (int ability = 0; ability < 4; ability++)
            {
            // Debug.Log(abilitySet[ability].SkillInRange(getCoords(), currentTarget.getCoords()));
                if (abilitySet[ability].abilityName == "Heal" && CheckHeal() && abilitySet[ability].CanUseSkill(this.gameObject))
                {
                    abilitySet[ability].UseSkill(gameObject);
                    Debug.Log(this + " Healed");    
                    return true;
                }   
                    
                if (abilitySet[ability].damage > bestAttack && abilitySet[ability].CanUseSkill(currentTarget.gameObject))
                {
                    bestAttack = (int)abilitySet[ability].damage;
                    choice = ability;
                    chosen = true;
                }
            }
            //float dist = Vector3.Distance(getCoords(), currentTarget.getCoords());
            //if (!(dist <= 1.5))
            //  return;
            //Debug.Log("currentTarget = " + currentTarget.gameObject + " skill = " + abilitySet[0].abilityName + " range = " + dist);
            if (chosen)
            {
                Debug.Log(this + " using skill: " + abilitySet[choice] + " on " + currentTarget);
                abilitySet[choice].UseSkill(currentTarget.gameObject); //test
                // testing statusEffect
               // Burn burn = new Burn(getWisdom(),this, currentTarget,false);
               // TurnBehaviour.EnemyHasJustAttacked();
                return true;
            }
            else
            {
                Debug.Log("No Possible Attacks");
                return false;
            }
    }

    public override void TakeDamage(float damage, GameObject attacker)
    {
        base.TakeDamage(damage, attacker);
        EnemyController.CheckTargeted(enemyID);
        attacker.GetComponent<Actor>().aggroScore++;
    }
    public bool CheckHeal()
    {
        if (GetHealthPercent() < 50 && GetHealthPercent() < nearest.GetHealthPercent() && !TargetInRange())
            return true;
        else
            return false;
    }

    public bool TargetInRange()
    {
        foreach(Ability ability in abilitySet)
        {
            if (ability.abilityName != "Heal" && ability.CanUseSkill(currentTarget.gameObject))
                return true;
        }
        return false;
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
