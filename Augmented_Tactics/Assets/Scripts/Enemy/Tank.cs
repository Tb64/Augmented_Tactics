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
    protected List<Vector3> cantMove;
    //protected UsableItem healItem;
    protected bool regularMode, inPosition, sameTurn,firstMove, firstDebuffed,healMode,healPossible;
    protected Enemy closestAggro;
    protected Ability buff, debuff, heal, lastResort; //needs one buff, one debuff, heal(multiple if possible),basic attack or similar
                                                    //should be slow and attack last so allies are in position
    public override void Start()
    {
        base.Start();
        regularMode = false;
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
    }

    public override void EnemyActions()
    {

        if (Random.Range(0, 1000) <= 500 && GetHealthPercent() < 35 || closestAggro.GetHealthPercent() < 45 && healPossible)
            healMode = true;

        if (healMode)
        {
            if (closestAggro.GetHealthPercent() < 45)
            {
                if (HealSelfOrPartner(0))
                {
                    if (GetHealthPercent() > 45)
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
        if (regularMode && sameTurn && CheckManaReplenish())
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
                if(getManaCurrent() <= buff.manaCost && getManaCurrent()<= debuff.manaCost && !CheckManaReplenish())
                {
                    regularMode = true;
                    sameTurn = true;
                    return;
                }
                else
                {
                    setNumOfActions(0);
                    return;
                }
                
            }
        }


    }

    private bool HealSelfOrPartner(int choice)
    {
        if (choice == 0)
        {
            if (heal.CanUseSkill(closestAggro.gameObject))
                return heal.UseSkill(closestAggro.gameObject);
            else if (GetHealItem())
            {
                if (closestAggro.GetHealthPercent() < 45 && healItem.CanUseItem(gameObject, closestAggro.gameObject))
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

    private bool CheckInPosition()
    {
        if (Vector3.Distance(getCoords(), closestAggro.getCoords()) <= buff.range_max)
            return true;
        else
            return false;
    }
    private void GetInPosition()
    {
        Vector3 output = closestAggro.getCoords() - currentTarget.getCoords();
        output = output.normalized;
        Debug.Log(output);
        Vector3 movingTo;
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z) /*&& !cantMove.Contains(new Vector3(output.x - 3, output.y, output.z)) || !cantMove.Contains(new Vector3(output.x + 3, output.y, output.z))*/)
        {
            if (output.x > 0 && !cantMove.Contains(new Vector3(output.x - 3, output.y, output.z)))
                movingTo = new Vector3(output.x-3,output.y,output.z);
            else
                movingTo = new Vector3(output.x + 3, output.y, output.z);
        }
        else
        {
            if (output.z > 0 && !cantMove.Contains(new Vector3(output.x, output.y, output.z - 3)))
                movingTo = new Vector3(output.x, output.y, output.z-3);
            else
                movingTo = new Vector3(output.x, output.y, output.z+3);
        }
        Vector3 temp = Support.SetPosition(this, movingTo, map);
        if(temp != new Vector3(-1,-1,-1))
            movingTo = temp;
        else
        {
            cantMove.Add(temp);
            GetInPosition();
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
        if (buff.CanUseSkill(closestAggro.gameObject))
        {
            buff.UseSkill(closestAggro.gameObject);
            return true;
        }
        else if (debuff.CanUseSkill(currentTarget.gameObject))
        {
            debuff.UseSkill(currentTarget.gameObject);
            return true;
        }
        else if (closestAggro.GetHealthPercent() < 60 && heal.CanUseSkill(closestAggro.gameObject))
        {
            heal.UseSkill(closestAggro.gameObject);
            return true;
        }
        else
            return false;
    }
    private void FindAggroCluster()
    {
        float closest = 1000,secondClosest,thirdClosest;
        Enemy second = null, third = null;        
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
    private bool CheckTeamStrat() // see if searching for clusters is possible
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
    private bool CheckManaReplenish()
    {
        if (getManaCurrent() >= buff.manaCost)
            return true;
        else
        {
            if (!ManaReplenish())
                return false;
            else
                return true;
        }
            
    }

    private bool ManaReplenish()
    {
        foreach (UsableItem usable in usableItems)
            if (usable.name.Equals("Large Mana Tonic") || usable.name.Equals("Medium Mana Tonic") || usable.name.Equals("Small Mana Tonic"))
            {
                usable.UseItem(gameObject, gameObject);
                return true;
            }
        foreach (Ability ability in abilitySet)
            if (ability.abilityName == "Mana Replenish" && ability.CanUseSkill(gameObject))//ability has to have this name in future
            {
                ability.UseSkill(gameObject);
                return true;
            }   
        return false;
                
    }

    public override string GetArchetype()
    {
        return "tank";
    }
    private void GetAbilities()
    {
        buff = abilitySet[0];
        debuff = abilitySet[1];
        heal = abilitySet[2];
        lastResort = abilitySet[3];
    }
}
