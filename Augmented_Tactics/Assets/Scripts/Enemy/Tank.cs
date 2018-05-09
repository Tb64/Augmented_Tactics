using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy{
    //take damage for aggressives and defend
    //utilize buffs and debuffs to save friendlies and weaken enemies
    //stay on front lines at all costs
    // find biggest collective of teammates near aggro
    // get between them and their closest enemies
    //buff defense on health low
    //else: buff offense
    //use last resort action if all else fails
    public string type;
    protected List<Vector3> cantMove;
    //protected UsableItem healItem;
    protected bool regularMode, inPosition, sameTurn,firstMove, firstDebuffed,healMode,healPossible, buffCool, debuffCool;
    protected int buffCount, debuffCount;
    protected Enemy closestAggro;
    protected Ability buff, debuff, heal, lastResort; //needs one buff, one debuff, heal(multiple if possible),basic attack or similar
                                                    //should be slow and attack last so allies are in position
    public override void Start()
    {
       
        
    }

    public override void EnemyInitialize()
    {
        archetype = "tank";
        if (!boss)
        {
            base.EnemyInitialize();
            GetAbilities();
            SetAbilities();
        }
            
        regularMode = false;
        buffCool = false;
        debuffCool = false;
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        
        FindAggroCluster();
        currentTarget = aggro;
        inPosition = false;
        sameTurn = false;
        firstMove = true;
        firstDebuffed = false;
        healMode = false;
        healPossible = true;
        cantMove = new List<Vector3>();

        if (buffCool)
        {
            buffCount--;
            if (buffCount <= 0)
                buffCool = false;
        }

        if (debuffCool)
        {
            debuffCount--;
            if (debuffCount <= 0)
                debuffCool = false;
        }

    }

    public override void EnemyActions()
    {

        if (getMoves() == 0)
            return;

        if (Random.Range(0, 1000) <= 500 && GetHealthPercent() < .35 || closestAggro.GetHealthPercent() < .45 && healPossible)
            healMode = true;

        if (healMode)
        {
            if (closestAggro.GetHealthPercent() < .45)
            {
                if (HealSelfOrPartner(0))
                {
                    if (GetHealthPercent() > .45)
                    {
                        healMode = false;
                        return;
                    }
                    else
                        return;
                }
                else
                    healPossible = false;
            }
            else
            {
                if (HealSelfOrPartner(1))
                {
                    healMode = false;
                    return;
                }
                else
                {
                    healMode = false;
                    healPossible = false;
                    return;
                }
            }       
        }


        if (firstDebuffed)
        {
            if (lastResort.CanUseSkill(nearest.gameObject))
            {
                lastResort.UseSkill(nearest.gameObject);
                return;
            }
            else if (debuff.CanUseSkill(nearest.gameObject))
            {
                debuff.UseSkill(nearest.gameObject);
                return;
            }
            else
            {
                setNumOfActions(0);
                TurnBehaviour.EnemyTurnFinished();
                return;
            }
        }
        if (inPosition && CheckInPosition())
        {
            inPosition = true;
        }
        else
        {
            inPosition = false;
        }
        if (regularMode && sameTurn && CheckManaReplenish(buff))
            regularMode = false;
        if (regularMode)
        {
            base.EnemyActions();
            return;
        }
        if (!inPosition)
        {
            GetInPosition();
            return;
        }   
        else if (BuffOrDebuff())
        {
            if (firstMove)
            {
                Debug.Log("Buffing or Debuffing");
                firstDebuffed = true;
                firstMove = false;
            }
            return;
        }
        else
        {
            if (AttemptAbility(lastResort, nearest))
            {
                if (firstMove)
                {
                    firstMove = false;
                }
                return;
            }
            else
            {
                if(getManaCurrent() <= buff.manaCost && getManaCurrent()<= debuff.manaCost && !CheckManaReplenish(buff))
                {
                    regularMode = true;
                    //Debug.LogError("Tank Regular Mode");
                    sameTurn = true;
                    Debug.Log("Possible Cause of Crash");
                    //EnemyController.ExhaustMoves(SM);
                    return;
                }
                else
                {
                    Debug.LogError("settings actions to 0");
                    setNumOfActions(0);
                    TurnBehaviour.EnemyTurnFinished();
                    return;
                }
                
            }
        }


    }

    protected bool HealSelfOrPartner(int choice)
    {
        if (choice == 0)
        {
            if (heal.CanUseSkill(closestAggro.gameObject))
                return heal.UseSkill(closestAggro.gameObject);
            else if (GetHealItem())
            {
                if (closestAggro.GetHealthPercent() < .45 && healItem.CanUseItem(gameObject, closestAggro.gameObject))
                    return healItem.UseItem(gameObject, closestAggro.gameObject);
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            if (heal.CanUseSkill(gameObject))
                return heal.UseSkill(gameObject);
            else if (GetHealItem())
            {
                if (healItem.CanUseItem(gameObject, gameObject))
                    return healItem.UseItem(gameObject, gameObject);
            }
            else
                return false;
        }
        return false;
    }

    protected bool CheckInPosition()
    {
        if ((Vector3.Distance(getCoords(), closestAggro.getCoords()) <= buff.range_max) || SamePlane())
            return true;
        else
            return false;
    }

    protected bool SamePlane()
    {
        Vector3 myCoords = getCoords(), closeCoords = closestAggro.getCoords(), playerCoords = closestAggro.getNearest().getCoords();
        if ((myCoords.x == closestAggro.getCoords().x && myCoords.x == playerCoords.x) || (myCoords.z == closestAggro.getCoords().z && myCoords.x == playerCoords.z))
            return true;
        else
            return false;
    }

    protected void GetInPosition()
    {
        if (getMoves() == 0)
            return;
        Debug.Log(closestAggro + " " + closestAggro.getCoords() + " " + currentTarget + " " + currentTarget.getCoords());
        Vector3 cAPos = closestAggro.getCoords();
        Vector3 output = closestAggro.getCoords() - currentTarget.getCoords();
        Vector3 movingTo;
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z) && !cantMove.Contains(new Vector3(output.x - buff.range_max, output.y, output.z)) || !cantMove.Contains(new Vector3(output.x + buff.range_max, output.y, output.z)))
        {
            if (output.z > 0 && !cantMove.Contains(new Vector3(output.x, output.y, output.z - buff.range_max)))
                movingTo = new Vector3(cAPos.x, 0, cAPos.z - buff.range_max);
            else
                movingTo = new Vector3(cAPos.x, 0, cAPos.z + buff.range_max);
        }
        else
        {
            if (output.x > 0 && !cantMove.Contains(new Vector3(output.x - buff.range_max, output.y, output.z)))
                movingTo = new Vector3(cAPos.x - buff.range_max, 0, cAPos.z);
            else
                movingTo = new Vector3(cAPos.x + buff.range_max, 0, cAPos.z);
        }
        Debug.Log(movingTo);
        Vector3 temp = Support.SetPosition(this, movingTo, map);
        if(temp != new Vector3(-1,-1,-1))
            movingTo = temp;
        else
        {
            cantMove.Add(temp);
            //GetInPosition();
            return;
        }
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        if (firstMove)
            firstMove = false;
        inPosition = true;
    }
    public virtual bool BuffOrDebuff() //I'm not 100% on this, just a basic algorithm. starting with just closest instead of AOE buff / debuff
    {
        if (buff.CanUseSkill(closestAggro.gameObject) && !buffCool)
        {
            buff.UseSkill(closestAggro.gameObject);
            return true;
        }
        else if (debuff.CanUseSkill(currentTarget.gameObject))
        {
            debuff.UseSkill(currentTarget.gameObject);
            return true;
        }
        else if (closestAggro.GetHealthPercent() < .60 && heal.CanUseSkill(closestAggro.gameObject))
        {
            heal.UseSkill(closestAggro.gameObject);
            return true;
        }
        else
            return false;
    }
    protected void FindAggroCluster()
    {
        float closest = 1000,secondClosest,thirdClosest;
        Enemy second = null, third = null;
        aggro = EnemyController.aggro;
        if (aggro == null)
            aggro = getNearest();
        foreach (Enemy enemy in EnemyController.enemyList)
        {
            if (enemy.getEnemyID() != enemyID)
            {
                float clusterToAggro = Vector3.Distance(enemy.getCoords(), aggro.getCoords());
                if (clusterToAggro < closest)
                {
                    closest = clusterToAggro;
                    closestAggro = enemy;
                }
            }
        }
        Debug.Log("First Found");
        targetLocked = true;
        if (!CheckTeamStrat()) // just protect person closest to aggro
            return;
        else
        {
            secondClosest = 1000;
            foreach (Enemy enemy in EnemyController.enemyList)
            {
                if (enemy.getEnemyID() != enemyID && enemy != closestAggro)
                {
                    float clusterToAggro = Vector3.Distance(enemy.getCoords(), aggro.getCoords());
                    if (clusterToAggro < closest)
                    {
                        secondClosest = clusterToAggro;
                        second = enemy;
                    }
                }
            }
            thirdClosest = 1000;
            foreach (Enemy enemy in EnemyController.enemyList)
            {
                if (enemy.getEnemyID() != enemyID)
                {
                    float clusterToAggro = Vector3.Distance(enemy.getCoords(), aggro.getCoords());
                    if (clusterToAggro < closest && enemy != closestAggro && enemy!= second)
                    {
                        thirdClosest = clusterToAggro;
                        third = enemy;
                    }
                }
            }
        }

        if (secondClosest + thirdClosest < 2 * closest)
            closestAggro = second;

        targetLocked = true;
        
    }
    protected bool CheckTeamStrat() // see if searching for clusters is possible
    {
        if(EnemyController.enemyList.Count < 4)
            return false;
        int aliveCount=0;
        foreach(Enemy enemy in EnemyController.enemyList)
        {
            if (!enemy.isDead())
                aliveCount++;
        }
        if (aliveCount >= 4)
            return true;
        else
            return false;
    }
    

    public override string GetArchetype()
    {
        return archetype;
    }

    protected void SetAbilities() 
    {
        buff = abilitySet[0];
        debuff = abilitySet[1];
        heal = abilitySet[2];
        lastResort = abilitySet[3];
        //Debug.LogError("tank abilities set" + " " + abilitySet[3]);
    }

    protected void GetAbilities()
    {
        string[] possibles = BuffDebuff.GetStatCalls();
        int first = Random.Range(0, possibles.Length),second = Random.Range(0, possibles.Length);
        if (second == first && first != 0)
            second = first - 1;
        else if (second == first)
            second = first +1;
        if (Random.Range(0, 100) < 20)
            abilitySet[0] = new BuffDebuff(gameObject, possibles[first], possibles[second], true,true,getWisdom(), false);
        else
            abilitySet[0] = new BuffDebuff(gameObject, possibles[first], null, false, true, getWisdom(), false);

        if (Random.Range(0, 100) < 20)
            abilitySet[1] = new BuffDebuff(gameObject, possibles[second], possibles[first], true,false, getWisdom(), false);
        else
            abilitySet[1] = new BuffDebuff(gameObject, possibles[second], null, false, false, getWisdom(), false);

        abilitySet[2] = new DivineFavor(gameObject);

        if (Random.Range(0, 1) == 0)
            abilitySet[3] = new Vengeance(gameObject);
        else
            abilitySet[3] = new Smite(gameObject);
    }
}
