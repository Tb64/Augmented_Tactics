using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Enemy {

    //get distance from highest aggro
    //check if support of a teammate is necessary
    //if: turn to hit greatest threat to teammate
    //else: hit most aggro
    //stay barely in range of best distance / strongest attack
    //stay away and out of most aggro's path
    //only use close range attacks when absolutely necessary even if stronger
    protected float distanceFromAggro;
    //private Actor aggro;
    protected Enemy aiding;
    protected Ability strongest,backup, mostDistance,heal; //backup's range should ideally be in between strongest and mostDistance and require less mana
    protected bool regularMode, hasHeal,aidLocked;
    public string type;
    public Support(string type)
    {
        this.type = type;
    }
    public override void Start()
    {
        //base.Start();
        EnemyInitialize();
        hasHeal = false;
        TurnBehaviour.OnEnemyOutOfMoves += this.ResetValues;
        GetAbilities();
        FindRanges();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        TurnBehaviour.OnEnemyOutOfMoves -= this.ResetValues;
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        GetAggroDistance();
        aidLocked = false;
    }

    public override void EnemyActions()
    {
        if (getMoves() == 0)
            return;

        if (regularMode)
        {
            base.EnemyActions();
            return;
        }

        if (targetLocked && !currentTarget.isDead() && !currentTarget.isIncapacitated())
        {
            RunAndGun();
            return;
        }

        if (aidLocked)
        {
            SaveFriendly();
            return;
        }

        if (!targetLocked && !aidLocked)
        {
            currentTarget = PlayerTooClose();
            if (currentTarget != null)
            {
                targetLocked = true;
                if (CheckHeal())
                    return;
                else if (TryStrongestAndBackup())
                {
                    return;
                }
                /*else if (AttemptAttack())
                {
                    return;
                }*/
                else
                {
                    FindShweetSpot(this,currentTarget,mostDistance,map);
                    return;
                }
            }
            aiding = CheckSupport();
            if (aiding == null)
            {
                currentTarget = aggro;
                targetLocked = true;
                RunAndGun();
            }
            else
            {
                aiding.aided = true;
                SaveFriendly();
            }
                
        }
        else
        {
            targetLocked = false;
            EnemyController.ExhaustMoves(SM);
        }
        
    }

    public override string GetArchetype()
    {
        return "support";
    }

    public void ResetValues()
    {
        if(aiding != null)
            aiding.aided = false;
    }

    protected Actor PlayerTooClose()
    {
        foreach(Actor player in EnemyController.userTeam)
        {
            Vector3 temp = player.getCoords();
            if (Vector3.Distance(player.getCoords(), getCoords()) <= 5 && !player.isDead() && !player.isIncapacitated())
                return player;
        }
        return null;
    }

    protected void SaveFriendly()
    {
        Debug.Log("Saving " + aiding);
        if (!aidLocked || !currentTarget.isDead() && !currentTarget.isIncapacitated())
        {
            aiding.UpdateNearest();
            currentTarget = aiding.getNearest();
            aidLocked = true;
        }
        if (TryStrongestAndBackup())
        {
            return;
        }
        else
        {
            RunAndGun();
        }
            
    }

    protected bool TryStrongestAndBackup()
    {
        if (Enemy.AttemptAbility(strongest,currentTarget))
        {
            return true;
        }
        else if (Enemy.AttemptAbility(backup,currentTarget))
        {
            return true;
        }
        else
            return false;
    }

    protected void RunAndGun() //Default Tactic of Support if no teammate needs help
    {
        Debug.Log(this + " is Running and Gunning");
        if (!mostDistance.SkillInRange(getCoords(),aggro.getCoords()) /*|| distanceFromAggro - mostDistance.range_max > 5 && mostDistance.CanUseSkill(currentTarget.gameObject)*/)
        {
            Debug.Log("Finding Shweet Shpot");
            FindShweetSpot(this,currentTarget,mostDistance,map); // get closer so attack is possible, or further to stay away from enemies
            return;
        }    
        else if (mostDistance.CanUseSkill(currentTarget.gameObject))
        {
            mostDistance.UseSkill(currentTarget.gameObject);
            return;
        }
        else if (mostDistance.manaCost > getManaCurrent())
        {
            Debug.Log("Mana Low. Switching to Regular Mode");
            regularMode = true;
            EnemyActions();
            return;
        }
        Debug.LogError("out of bounds run and gun");
    }
    public static bool FindShweetSpot(Enemy self,Actor currentTarget, Ability mostDistance, TileMap map )
    {
        Vector3 position = (self.getCoords()-currentTarget.getCoords()).normalized;
        float xDistance = position.x;
        float zDistance = position.z;
        if (Mathf.Abs(xDistance) > Mathf.Abs(zDistance)) //check which coord is keeping attack out of range and 
                                                           //move just within distance to keep striking
        {
            if (xDistance < 0)
            {
                position = currentTarget.getCoords() - new Vector3(mostDistance.range_max, 0, 0);
            }
            else
            {
                position = currentTarget.getCoords() + new Vector3(mostDistance.range_max, 0, 0);
            }
        }
        else
        {
            if (zDistance < 0)
            {
                position = currentTarget.getCoords() - new Vector3(0, 0, mostDistance.range_max);
            }
            else
            {
                position = currentTarget.getCoords() + new Vector3(0, 0, mostDistance.range_max);
            }
        }
        Debug.Log(position);
        Vector3 movingTo = SetPosition(self,position, map); //just in case tile is occupied
        Debug.Log("Attempting to move " + self + " from " + self.getCoords() + " to " + movingTo);
        map.moveActorAsync(self.gameObject, movingTo);
        //self.setNumOfActions(1);
        return true;
    }
   /* private void StepBack()
    {
        Vector3 position;
        float xDistance = getCoords().x - aggro.getCoords().x;
        float zDistance = getCoords().z - aggro.getCoords().z;
        if (Mathf.Abs(xDistance)+10 < mostDistance.range_max)
        {
            if (xDistance < 0)
            {
                position = aggro.getCoords() - new Vector3(mostDistance.range_max, 0, 0);
            }
            else
            {
                position = aggro.getCoords() + new Vector3(mostDistance.range_max, 0, 0);
            }
        }
    }*/
    
    public static Vector3 SetPosition(Enemy self,Vector3 pos, TileMap map)
    {     
        if (map.UnitCanEnterTile(pos))
            return pos;
        else
            return PosCloseTo(self,pos,map);
    }

    protected void FindRanges()
    {
        float bestRange = 0, mostRange=0;
 
        foreach (Ability ability in abilitySet)
        {
            if (ability.range_max > mostRange)
            {
                backup = mostDistance;
                mostRange = ability.range_max;
                mostDistance = ability;
            }
            if(ability.damage > bestRange)
            {
                bestRange = ability.range_max;
                strongest = ability;
            }
            if (ability.canHeal)
            {
                hasHeal = true;
                heal = ability;
            }
                
        }
        if(backup == strongest)
        {
            bestRange = 0;
            foreach (Ability ability in abilitySet)
            {
                if(ability != strongest && ability != mostDistance && ability.damage > bestRange)
                {
                    bestRange = ability.damage;
                    backup = ability;
                }
            }
        }
    }

    protected Enemy CheckSupport()
    {
        if (!EnemyController.targeted)
            return null;
        else
            return EnemyController.target;
    }

    private void GetAggroDistance()
    {
        aggro = EnemyController.aggro;
        if (aggro != null && !aggro.isDead() && !aggro.isIncapacitated())
            distanceFromAggro = Vector3.Distance(getCoords(), aggro.getCoords());
        else
        {
            aggro = nearest;
            distanceFromAggro = Vector3.Distance(getCoords(), aggro.getCoords());
        }
    }

    public void GetAbilities()
    {

    }
    /*public override bool AttemptAttack()
    {
        return true;
    }*/
}
