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
    protected Ability strongest,backup, mostDistance,heal,arrow; //backup's range should ideally be in between strongest and mostDistance and require less mana
    protected bool regularMode, hasHeal,aidLocked,arrowMode;
    //public string type;

    /*public Support(string type)
    {
        this.type = type;
    }

    public Support()
    {

    }*/

    public override void Start()
    {
       // boss = false;
    }

    public override void EnemyInitialize()
    {
        archetype = "support";
        hasHeal = false;
        if (!boss)
        {
            base.EnemyInitialize();
            GetAbilities();
            FindRanges();
        }
        TurnBehaviour.OnEnemyOutOfMoves += this.ResetValues; 
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

    public override bool EnemyActions()
    {
        if (getMoves() == 0)
            return false;

        if (arrowMode)
        {
            if (ManaReplenish())
            {
                arrowMode = false;
                Ability temp = arrow;
                arrow = mostDistance;
                mostDistance = temp;
            }
                
        }

        /*if (regularMode)
        {
            base.EnemyActions();
            return;
        }*/

        if (targetLocked && !currentTarget.isDead() && !currentTarget.isIncapacitated())
        {
            return RunAndGun();
        }

        if (aidLocked)
        {
            return SaveFriendly();
        }
       // Debug.Log(targetLocked + " " + aidLocked);
        if (!targetLocked && !aidLocked)
        {
            currentTarget = PlayerTooClose();
            if (currentTarget != null)
            {
                targetLocked = true;
                if (CheckHeal() && HealSelf())
                    return true;
                else if (TryStrongestAndBackup())
                {
                    return true;
                }
                /*else if (AttemptAttack())
                {
                    return;
                }*/
                else
                {
                    return FindShweetSpot(this,currentTarget,mostDistance,map);
                }
            }
            aiding = CheckSupport();
            if (aiding == null)
            {
                currentTarget = aggro;
                targetLocked = true;
                return RunAndGun();
            }
            else
            {
                aiding.aided = true;
                return SaveFriendly();
            }
                
        }
        else
        {
            targetLocked = false;
            //EnemyController.ExhaustMoves(SM); //probably caused crash
            Debug.Log("Probably the trap");
            return false;
        }
        
    }

    protected bool HealSelf()
    {
        if (heal.CanUseSkill(gameObject))
            return heal.UseSkill(gameObject);
        else if (GetHealItem())
        {
            if (healItem.CanUseItem(gameObject,gameObject))
                return healItem.UseItem(gameObject, gameObject);
            else
                return false;
        }
        else
            return false;
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

    protected bool SaveFriendly()
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
            return true;
        }
        else
        {
            return RunAndGun();
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

    protected bool RunAndGun() //Default Tactic of Support if no teammate needs help
    {
        if (getMoves() == 0)
            return false;
        Debug.Log(this + " is Running and Gunning "+ getMoves());
        if (!mostDistance.SkillInRange(gameObject,currentTarget.gameObject) /*|| distanceFromAggro - mostDistance.range_max > 5 && mostDistance.CanUseSkill(currentTarget.gameObject)*/)
        {
            Debug.Log("Finding Shweet Shpot");
            //Debug.Log(map);
            return FindShweetSpot(this,currentTarget,mostDistance,map); // get closer so attack is possible, or further to stay away from enemies
        }    
        else if (mostDistance.CanUseSkill(currentTarget.gameObject))
        {
            return mostDistance.UseSkill(currentTarget.gameObject);
        }
        else if (mostDistance.manaCost > getManaCurrent())
        {
            Debug.Log("Mana Low. Switching to Arrow Mode");
            arrowMode = true;
            Ability temp = mostDistance;
            mostDistance = arrow;
            arrow = temp;
            //regularMode = true;
            return false;
        }
        else
        {
            Debug.LogError(getMoves());
            return false;
            //TurnBehaviour.EnemyTurnFinished();
        }
    }
    public static bool FindShweetSpot(Enemy self,Actor currentTarget, Ability mostDistance, TileMap map )
    {
        if (self.getMoves() == 0)
            return false;
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
        Debug.Log("Attempting to move " + self + " from " + self.getCoords() + " to " + movingTo + " with " +self.getMoves()+ " actions");
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
        //Debug.LogError("support abilities set" + " " + abilitySet[3]);
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
        abilitySet = new Ability[4];
        string[] possibles = SkillLoader.ClassSkills(3);
        arrow = abilitySet[0] = new Arrow(gameObject);
        if (Random.Range(0, 10) < 7)
            abilitySet[1] = new Steal(gameObject);
        else
            abilitySet[1] = new Heal(gameObject);
        int first = Random.Range(1, 7), second = Random.Range(1, 7);
        abilitySet[2] = SkillLoader.LoadSkill(possibles[first], gameObject);
        if (first == second)
        {
            if (first != 7)
                abilitySet[3] = SkillLoader.LoadSkill(possibles[second + 1], gameObject);
            else
                abilitySet[3] = SkillLoader.LoadSkill(possibles[second - 1], gameObject);
        }
        else
            abilitySet[3] = SkillLoader.LoadSkill(possibles[second], gameObject);
    }
    /*public override bool AttemptAttack()
    {
        return true;
    }*/
}
