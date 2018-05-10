using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************
Enemy
This is the parent class of all enemies
*****************/

public class Enemy : Actor
{
    protected int enemyID;
    protected string archetype, type;
    protected Actor nearest, weakest, aggro;
    protected Vector3 playerPosition, enemyPosition;
    public float distanceToNearest;
    public Actor getNearest() { return nearest; }
    public void setNearest(Actor nearestPlayer) { nearest = nearestPlayer; }
    public Vector3 getPlayerPosition() { return playerPosition; }
    public void setPlayerPosition(Vector3 pPosition) { playerPosition = pPosition; }
    public Vector3 getEnemyPosition() { return enemyPosition; }
    public void setEnemyPosition(Vector3 ePosition) { enemyPosition = ePosition; }
    public void setEnemyId(int id) { enemyID = id; }
    public int getEnemyID() { return enemyID; }
    public Actor getWeakest() { return weakest; }
    public void setWeakest(Actor weakestPlayer) { weakest = weakestPlayer; }
    protected int expGiven;
    protected List<Actor> cantTarget;
    protected UsableItem healItem;
    protected bool targetLocked;
    public bool aided, boss, loaded;
    public static bool loadRegulars;

    public Actor currentTarget;
    // Use this for initialization
    // Use this for initialization
    new public virtual void Start()
    {
        /*LoadPlayer();
        if(archetype == "regular")
            EnemyInitialize();*/
        
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

    public virtual void EnemyInitialize()
    {
        base.Init();
        //Debug.LogError(archetype + " " + abilitySet[3]);
        expGiven = GetExpGiven();
        aggroScore = 0;
        //TurnBehaviour.OnEnemyTurnStart += this.EnemyTurnStartActions;
        //TurnBehaviour.OnUnitMoved += this.EnemyMoved;
        //TurnBehaviour.OnUnitMoved += this.EnemyUsedAction;
        //TurnBehaviour.OnEnemyUnitAttack += this.EnemyUsedAction;
        

        if(archetype != "regular")
            abilitySet = new Ability[4];

        if(!IsBoss())
        {
            mana_max = (wisdom+intelligence)*5;
            setManaCurrent(mana_max);
            health_max = (wisdom + intelligence + level) * 5;
            health_current = health_max;
        }

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }


        /*updating for using varied attacks
         update for specific character needs to be added to every
         type of enemy as they are created to load correct attacks*/
        /* if(GetArchetype() == "regular")
         {
             LoadPlayer();
         }*/
        //Debug.Log(abilitySet[3]);
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
        Debug.Log("Enemy " + enemyID + " "+ gameObject+ " turn started");
        aggro = EnemyController.aggro;
        if (GetHealthPercent() == 0f)
        {
            //SM.setTurn();
            return;
        }
        //base.EnemyTurnStart();
        //map.selectedUnit = gameObject;
        nearest = FindNearestPlayer();
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

    public virtual string GetArchetype()
    {
        return archetype;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        //added for defense classes. They will automatically protect an incapacitated teammate
        //aggressives are the priority, and the highest level aggressive gets even more priority
        if (this.GetArchetype() == "aggressive" && EnemyController.CheckTargetChange(getEnemyID()))
        {
            EnemyController.targeted = true;
            EnemyController.target = this;
            EnemyController.canChangeTarget = false;
        }
        if (EnemyController.canChangeTarget)
        {
            EnemyController.targeted = true;
            EnemyController.target = this;
        }

    }

    public virtual bool EnemyActions()
    {
        //Debug.Log("NON-PROXIMITY");
        if (getMoves() == 0)
            return false;

        if (targetLocked && (currentTarget.isDead() || currentTarget.isIncapacitated()))
        {
            targetLocked = false;
        }

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
            return false;
        }
        if (AttemptAttack())
            return true;
        Vector3 movingTo = PosCloseTo(this, currentTarget.getCoords(), map);
        if (movingTo == new Vector3(-1, -1, -1))
        {
            // movingTo = PosCloseTo(currentTarget.getCoords());
            //if (movingTo == new Vector3(0, 0, 0))
            //{
            Debug.Log("No possible move available, switching currentTarget.");
            cantTarget.Add(currentTarget);
            return false;
            //}
        }
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        return true;
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
    protected Enemy FindNearestEnemy()
    {
        Enemy nearest = null;
        float currentNearest = 10000000;
        foreach (Enemy user in EnemyController.enemyList)
        {
            enemyPosition = getCoords();
            if (user == null)
            {
                Debug.LogError("null user");
                return null;
            }
            playerPosition = user.getCoords();
            float distanceFromPlayer = Vector3.Distance(playerPosition, enemyPosition);
            if (distanceFromPlayer < currentNearest && !user.isDead())
            {
                nearest = user;
                currentNearest = distanceFromPlayer;
            }
        }
        return nearest;
    }
    protected Actor FindNearestPlayer()
    {
        Actor nearest = null;
        float currentNearest = 10000000;
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
            if (distanceFromPlayer < currentNearest && !user.isDead() && !user.isIncapacitated())
            {
                nearest = user;
                currentNearest = distanceFromPlayer;
            }
        }
        return nearest;
    }

    public void UpdateNearest()
    {
        FindNearestPlayer();
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

    protected void findTarget()
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
            foreach (Actor user in EnemyController.userTeam)
            {
                if (!cantTarget.Contains(user) && !user.isDead() && !user.isIncapacitated())
                {
                    currentTarget = user;
                    targetLocked = true;
                    return;
                }
            }
            Debug.LogError("Absolutely No Good Move Available. Skipping Turn");
        }
    }



    /// <summary>
    /// TEMP - Calculates the closest map position to currentTarget.  Can not move to occupied tile. BUG - does not check if it can walk on returned path.
    /// </summary>
    /// <param name="mapPos">The map/tile position of occupied tile</param>
    /// <returns>Returns closest map/tile position to mapPos, that is not mapPos</returns>
    public static Vector3 PosCloseTo(Actor self, Vector3 mapPos, TileMap map)
    {
        Vector3 output = self.getCoords() - mapPos;
        output = output.normalized;
        if (output.x >= 0)
        {
            if (output.z >= 0)
            {
                Vector3 temp = PosCloseTo("rightup", mapPos, map);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("leftdown");
                    return PosCloseTo("leftdown", mapPos, map);
                }
                else
                {
                    Debug.Log("rightup");
                    return temp;
                }

            }
            else
            {
                Vector3 temp = PosCloseTo("rightdown", mapPos, map);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("leftup");
                    return PosCloseTo("leftup", mapPos, map);

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
                Vector3 temp = PosCloseTo("leftup", mapPos, map);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("rightdown");
                    return PosCloseTo("rightdown", mapPos, map);
                }
                else
                {
                    Debug.Log("leftup");
                    return temp;
                }
            }
            else
            {
                Vector3 temp = PosCloseTo("leftdown", mapPos, map);
                if (temp == new Vector3(-1, -1, -1))
                {
                    Debug.Log("rightup");
                    return PosCloseTo("rightup", mapPos, map);
                }
                else
                {
                    Debug.Log("leftdown");
                    return temp;
                }
            }
        }
    }

    public static Vector3 PosCloseTo(string directions, Vector3 pos, TileMap map)
    {
        if (directions == "rightup")
            return checkDirections(pos + new Vector3(1f, 0f, 0f), pos + new Vector3(0f, 0f, 1f), map);
        else if (directions == "rightdown")
            return checkDirections(pos + new Vector3(1f, 0f, 0f), pos + new Vector3(0f, 0f, -1f), map);
        else if (directions == "leftup")
            return checkDirections(pos + new Vector3(-1f, 0f, 0f), pos + new Vector3(0f, 0f, 1f), map);
        else
            return checkDirections(pos + new Vector3(-1f, 0f, 0f), pos + new Vector3(0f, 0f, -1f), map);
    }

    public static Vector3 checkDirections(Vector3 firstDir, Vector3 secDir, TileMap map)
    {
        if (map.UnitCanEnterTile(firstDir))
            return firstDir;
        else if (map.UnitCanEnterTile(secDir))
            return secDir;
        else
            return new Vector3(-1, -1, -1); //no move available
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
    public virtual bool AttemptAttack() //thinking of changing to attemptAction. Also covers heal
    {
        if (SM.checkTurn() || EnemyController.currentEnemy != enemyID)
            return false;
        Debug.Log(this + " Attempting attack on " + currentTarget + " at " + currentTarget.getCoords());
        int bestAttack = 0, choice = 0;
        bool chosen = false;
        //Debug.Log(abilitySet[2]);
        //Debug.Log(archetype);
        for (int ability = 0; ability < 4; ability++)
        {
            // Debug.Log(abilitySet[ability].SkillInRange(getCoords(), currentTarget.getCoords()));
            //Debug.Log(ability);
            Debug.Log(abilitySet[ability]);
            if (abilitySet[ability].canHeal && CheckHeal() && abilitySet[ability].CanUseSkill(gameObject))
            {
                abilitySet[ability].UseSkill(gameObject);
                Debug.Log(this + " Healed");
                return true;
            }

            if (!abilitySet[ability].canHeal && abilitySet[ability].damage > bestAttack && abilitySet[ability].CanUseSkill(currentTarget.gameObject))
            {
                bestAttack = (int)abilitySet[ability].damage;
                choice = ability;
                chosen = true;
            }
        }
        
        if (chosen)
        {
            Debug.Log(this + " using skill: " + abilitySet[choice] + " on " + currentTarget);
            abilitySet[choice].UseSkill(currentTarget.gameObject); 
            return true;
        }
        else
        {
            Debug.Log("No Possible Attacks");
            return false;
        }
    }

    protected bool CheckManaReplenish(Ability ability)
    {
        if (getManaCurrent() >= ability.manaCost)
            return true;
        else
        {
            if (!ManaReplenish())
                return false;
            else
                return true;
        }

    }

    protected bool ManaReplenish()
    {
        if (usableItems != null && usableItems.Count != 0)
            foreach (UsableItem usable in usableItems)
                if (usable.isManaItem)
                {
                    usable.UseItem(gameObject, gameObject);
                    return true;
                }
        foreach (Ability ability in abilitySet)
            if (ability.manaRestore)//ability has to have this name in future
            {
                ability.UseSkill(gameObject);
                return true;
            }
        return false;

    }

    public Enemy LoadPlayer()
    {
        abilitySet = new Ability[4];
        for (int x = 0; x < 4; x++)
            abilitySet[x] = new BasicAttack(gameObject);
        if (loadRegulars)
        {
            return LoadRegular();
        }
        string scene = SceneManager.GetActiveScene().name;
        scene = scene.ToLower();
        switch (scene)
        {
            case "battle1": //need to change back to battle1 after testing
                if (Random.Range(0, 1000) < 350)
                {
                    if (Random.Range(0, 1000) < 500)
                    {
                        
                        return LoadThief();
                    }
                    else
                    {

                        return LoadBrawler();
                    }
                }
                else
                {
                    //Debug.LogError("Loaded Regular");
                    return LoadRegular();
                }
          

            case "level02":
                if (Random.Range(0, 1000) < 350)
                {
                    if (Random.Range(0, 1000) < 500)
                    {
                        return LoadThief();
                    }
                    else
                    {
                        return LoadPaladin();
                    }
                }
                else
                {
                    return LoadRegular();
                }
                

            case "battle3":
                if (Random.Range(0, 1000) < 350)
                {
                    if (Random.Range(0, 1000) < 500)
                    {
                        return LoadThief();
                    }
                    else
                    {
                        return LoadPaladin();
                    }
                }
                else
                {
                    return LoadRegular();
                }
                

            case "battle4":
                if (Random.Range(0, 1000) < 500)
                {
                    int random = Random.Range(0, 1000);
                    if (random < 333)
                    {
                        return LoadWizard();
                    }
                    else if(random >=333 && random <666)
                    {
                        return LoadPaladin();
                    }
                    else
                    {
                        return LoadCleric();
                    }
                }
                else
                {
                    return LoadRegular();
                }
                

            case "battle5":
                if (Random.Range(0, 1000) < 400)
                {
                    int random = Random.Range(0, 1000);
                    if (random < 333)
                    {
                        return LoadBrawler();
                    }
                    else if (random >= 333 && random < 666)
                    {
                        return LoadPaladin();
                    }
                    else
                    {
                        return LoadWizard();
                    }
                }
                else
                {
                    return LoadRegular();
                }
                

            case "battle6":
                if (Random.Range(0, 1000) < 350)
                {
                    if (Random.Range(0, 1000) < 500)
                    {
                        return LoadWizard();
                    }
                    else
                    {
                        return LoadDarkKnight();
                    }
                }
                else
                {
                    return LoadRegular();
                }
               

            default: 
                if (Random.Range(0, 1000) < 400)
                {
                    int random = Random.Range(0, 1000);
                    if (random < 200)
                    {
                        return LoadBrawler();
                    }
                    else if (random >= 200 && random < 400)
                    {
                        return LoadPaladin();
                    }
                    else if(random >= 400 && random < 600)
                    {
                        return LoadWizard();
                    }
                    else if (random >= 600 && random < 800)
                    {
                        return LoadThief();
                    }
                    else
                    {
                        return LoadDarkKnight();
                    }
                }
                else
                {
                    return LoadRegular();
                }
        }
    }

    protected PlayerData SetDifficulty(PlayerData level,int playerLevel)
    {
        for(int x = playerLevel-2; x > 0; x--)
        {
            PlayerData.LevelUp(level,true);
        }
        return level;
    }

    public Enemy LoadRegular()
    {
        archetype = "regular";
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        Debug.Log("Player avg Lvl: " + EnemyController.playerLevel);
        level = SetDifficulty(level, /*EnemyController.playerLevel*/3);
        LoadStatsFromData(level);
        //Debug.LogError("archetype set to " + archetype);
        abilitySet = new Ability[4];
        abilitySet[0] = new BasicAttack(this.gameObject);
        abilitySet[1] = new Fire(this.gameObject);
        string[] possibles = { "heal", "curewounds", "sap", "gutpunch", "poisonarrow", "eviscerate", "vengeance", "lifeleech" };
        int first = Random.Range(0, possibles.Length-1);
        abilitySet[2] = SkillLoader.LoadSkill(possibles[first], this.gameObject);
        int second = Random.Range(0, possibles.Length-1);
        if (second == first)
        {
            if (second == possibles.Length-1)
                second--;
            else
                second++;
        }
        abilitySet[3] = SkillLoader.LoadSkill(possibles[second], this.gameObject);
        //Debug.Log("loaded Regular Abilities " + abilitySet[3]);
        return this;
    }


    public Enemy LoadBrawler()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[0]);
        GameObject spawned = Instantiate(enemyObj);
        //Destroy(spawned.GetComponent<Enemy>()); // might be trouble
        Aggressive newEnemy= spawned.AddComponent<Aggressive>();
        newEnemy.setMoveDistance(7);
        newEnemy.setSpeed(3);
        newEnemy.loaded = true;
        newEnemy.type = "brawler";
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.BrawlerKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        newEnemy.LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }

    public Enemy LoadThief()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[2]);
        GameObject spawned = Instantiate(enemyObj);
        Support newEnemy = spawned.AddComponent<Support>();
        newEnemy.setMoveDistance(3);
        newEnemy.setSpeed(1);
        newEnemy.type = "thief";
        newEnemy.loaded = true;
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.ThiefKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }

    public Enemy LoadCleric()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[1]);
        GameObject spawned = Instantiate(enemyObj);
        Defender newEnemy = spawned.AddComponent<Defender>();
        newEnemy.setMoveDistance(4);
        newEnemy.setSpeed(1);
        newEnemy.type = "cleric";
        newEnemy.loaded = true;
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.ClericKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        newEnemy.LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }

    public Enemy LoadDarkKnight()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[0]);
        GameObject spawned = Instantiate(enemyObj);
        Aggressive newEnemy = spawned.AddComponent<Aggressive>();
        newEnemy.setMoveDistance(4);
        newEnemy.setSpeed(1);
        newEnemy.type = "darkknight";
        newEnemy.loaded = true;
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.DarkKnightKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        newEnemy.LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }

    public Enemy LoadPaladin()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[1]);
        GameObject spawned = Instantiate(enemyObj);
        Tank newEnemy = spawned.AddComponent<Tank>();
        newEnemy.setMoveDistance(4);
        newEnemy.setSpeed(1);
        newEnemy.type = "paladin";
        newEnemy.loaded = true;
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.PaladinKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        newEnemy.LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }

    public Enemy LoadWizard()
    {
        GameObject enemyObj = Resources.Load<GameObject>(CharacterClasses.EnemyPrefabPath[1]);
        GameObject spawned = Instantiate(enemyObj);
        Aggressive newEnemy = spawned.AddComponent<Aggressive>();
        newEnemy.setMoveDistance(4);
        newEnemy.setSpeed(1);
        newEnemy.type = "wizard";
        newEnemy.loaded = true;
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        PlayerData level = PlayerData.GenerateNewPlayer(CharacterClasses.MageKey);
        level = SetDifficulty(level, EnemyController.playerLevel);
        newEnemy.LoadStatsFromData(level);
        Destroy(gameObject);
        return newEnemy;
    }



    protected static bool AttemptAbility(Ability strongest, Actor currentTarget)
    {
        if (strongest == null)
            return false;
        if (currentTarget == null)
            return false;
        if (strongest.CanUseSkill(currentTarget.gameObject))
        {
            strongest.UseSkill(currentTarget.gameObject);
            return true;
        }
        else
            return false;
    }

    public override void TakeDamage(float damage, GameObject attacker)
    {
        base.TakeDamage(damage, attacker);
        EnemyController.CheckTargeted(enemyID);
        attacker.GetComponent<Actor>().aggroScore++; //updating for actual function based on action / damage
    }

    public virtual bool CheckHeal()
    {
        if (GetHealthPercent() < .40 && GetHealthPercent() < nearest.GetHealthPercent() && !TargetInRange())
            return true;
        else
            return false;
    }

    protected bool GetHealItem()
    {
        if (usableItems == null || usableItems.Count == 0)
            return false;
        foreach (UsableItem item in usableItems)
        {
            if (item.isHealItem)
            {
                healItem = item;
                return true;
            }
        }
        return false;
    }

    protected Actor AbilityInRange(Ability ability)
    {
        foreach (Actor player in EnemyController.userTeam)
            if (ability.CanUseSkill(player.gameObject))
                return player;         
        return null;
    }

    public bool TargetInRange()
    {
        foreach(Ability ability in abilitySet)
        {
            if (!ability.canHeal && !ability.manaRestore && ability.CanUseSkill(currentTarget.gameObject))
                return true;
        }
        return false;
    }

    public int GetExpGiven()
    {
        switch (getLevel())
        {
            case 1:
                if (GetArchetype() == "regular")
                    return 25;
                else if (GetArchetype() == "defender")
                    return 50;
                else
                    return 75;

            case 2:
                if (GetArchetype() == "regular")
                    return 50;
                else if (GetArchetype() == "defender")
                    return 100;
                else
                    return 150;
            case 3:
                if (GetArchetype() == "regular")
                    return 75;
                else if (GetArchetype() == "defender")
                    return 150;
                else
                    return 225;

            case 4:
                if (GetArchetype() == "regular")
                    return 125;
                else if (GetArchetype() == "defender")
                    return 250;
                else
                    return 375;

            case 5:
                if (GetArchetype() == "regular")
                    return 250;
                else if (GetArchetype() == "defender")
                    return 500;
                else
                    return 750;

            case 6:
                if (GetArchetype() == "regular")
                    return 300;
                else if (GetArchetype() == "defender")
                    return 600;
                else
                    return 900;

            case 7:
                if (GetArchetype() == "regular")
                    return 350;
                else if (GetArchetype() == "defender")
                    return 750;
                else
                    return 1100;

            default:
                if (GetArchetype() == "regular")
                    return 450;
                else if (GetArchetype() == "defender")
                    return 900;
                else
                    return 1400;

        }
    }

    public virtual bool IsBoss()
    {
        return false;
    }

    public int GetID()
    {
        return enemyID;
    }
}
