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
    protected bool regularMode, inPosition, sameTurn;
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
    }

    public override void EnemyActions()
    {
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
            return;
        else
        {
            if (AttemptAbility(lastResort, nearest)) //need ability to pass on turn for this to be optimal
                return;
            else
            {
                regularMode = true;
                sameTurn = true;
                return;
            }
        }


    }
    private bool CheckInPosition()
    {
        if (Vector3.Distance(getCoords(), closestAggro.getCoords()) <= 5)
            return true;
        else
            return false;
    }
    private void GetInPosition()
    {
        Vector3 output = closestAggro.getCoords() - currentTarget.getCoords();
        output = output.normalized;
        Vector3 movingTo;
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z))
        {
            if (output.x > 0)
                movingTo = new Vector3(output.x-3,output.y,output.z);
            else
                movingTo = new Vector3(output.x + 3, output.y, output.z);
        }
        else
        {
            if (output.z > 0)
                movingTo = new Vector3(output.x, output.y, output.z-3);
            else
                movingTo = new Vector3(output.x, output.y, output.z+3);
        }
        Vector3 temp = Support.SetPosition(this, movingTo, map);
        if(temp != new Vector3(-1,-1,-1))
            movingTo = temp;
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
        inPosition = true;
    }
    private bool BuffOrDebuff() //I'm not 100% on this, just a basic algorithm. starting with just closest instead of AOE buff / debuff
    {
        if (buff.CanUseSkill(closestAggro.gameObject))
        {
            buff.UseSkill(closestAggro.gameObject);
            return true;
        }
        else if (closestAggro.GetHealthPercent() < 60 && heal.CanUseSkill(closestAggro.gameObject))
        {
            heal.UseSkill(closestAggro.gameObject);
            return true;
        }
        else if (debuff.CanUseSkill(currentTarget.gameObject))
        {
            debuff.UseSkill(currentTarget.gameObject);
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
