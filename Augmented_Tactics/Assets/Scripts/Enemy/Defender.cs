using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Enemy {
    //priority: protect targeted teammate
    //Draw incoming fire to tank
    //heal person / area being defended (defender must have heal)
    //defender should be slowest to wait for other teammates to move and then decide best course of action
    //utilize buffs on self/tank/defendee
    //check if defense necessary
    //idea: destiny bond type move that takes damage for teammate through magic
    //if(defense necessary) : implement priority
    //elseif(aggressive on team): find aggressive and go karowak on everyone's ass
    //else attampt strongest attack then move in the direction of the tank to draw fire
    //if no tank draw fire away from targeted / weakest
    private bool aidLocked, hit;
    private Ability strongest, mostDistance,lastResort, heal;
    private Enemy aiding;
    public string type;
    public Defender(string type)
    {
        this.type = type;
    }
    // Use this for initialization
    public override void Start ()
    {
        //base.Start();
        EnemyInitialize();
        GetAbilities();
        TurnBehaviour.OnEnemyOutOfMoves += this.ResetValues;
        FindRanges();
        //add buffs
        heal = GetHeal();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        TurnBehaviour.OnEnemyOutOfMoves -= this.ResetValues;
    }
    /* public override void EnemyInitialize() //temp until all abilities and items are done.  initialize attacks / stats based on lvl and archetype
     {

     }*/
    public override void EnemyInitialize()
    {
        
    }
    public override string GetArchetype()
    {
        return "defender";
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        aidLocked = false;
        hit = false;
    }

    public override void EnemyActions()
    {
        if (getMoves() == 0)
            return;
        if (aidLocked)
        {
            DrawFire();
            return;
        }
        if (CheckDefense())
        {
            DrawFire();
            return;
        }
        else if (CheckStrategy("aggressive"))
        {
            DrawFire();
            return;
        }
        else if(EnemyController.enemyList.Count > 1)
        {
            aiding = EnemyController.FindWeakestEnemy(this);
            aidLocked = true;
            aiding.aided = true;
            aiding.UpdateNearest();
            currentTarget = aiding.getNearest();
            DrawFire();
            return;
        }
        else
        {
            base.EnemyActions();
        }
    }

    public override bool CheckHeal()
    {
        if (GetHealthPercent() <= .35 && GetHealthPercent() < nearest.GetHealthPercent() || !PlayerInRange())
            return true;
        else
            return false;
    }
    private Ability GetHeal()
    {
        foreach (Ability ability in abilitySet)
            if (ability.abilityName == "Heal")
                return ability;
        Debug.LogError("Defender Loaded With No Heal Ability");
        return null;
    }

    private void FindRanges()
    {
        float bestRange = 0, mostRange = 0;

        foreach (Ability ability in abilitySet)
        {
            if (ability.range_max > mostRange)
            {
                mostRange = ability.range_max;
                mostDistance = ability;
            }
            if (ability.damage > bestRange)
            {
                bestRange = ability.range_max;
                strongest = ability;
            }
        }
    }

    private void DrawFire() //attempt to use best or any attack and run like hell in opposite direction of aiding enemy
    {
        if (hit)
        {
            FindShweetSpot();
            return;
        }

        if (AttemptAttack())
        {
            hit = true;
            return;
        }
        if(GetHealthPercent() > .50)
        {
            Support.FindShweetSpot(this, currentTarget, strongest, map);
            return;
        }
        else
        {
            Support.FindShweetSpot(this, currentTarget, mostDistance, map);
            return;
        }
            
    }

    private void FindShweetSpot() //lure attacker to other area after successful attack / aggro gain
    {
        if (aiding.CheckHeal() && Vector3.Distance(getCoords(),aiding.getCoords())<3) //add heal of person defending
        {
            if (GetHealItem())
            {
                healItem.UseItem(gameObject, aiding.gameObject);
                return;
            }

            if (heal.CanUseSkill(aiding.gameObject))
            {
                heal.UseSkill(aiding.gameObject);
                return;
            }
        }
        if (CheckHeal() || aiding.CheckHeal()) //add heal of person defending
        {
            if (GetHealItem())
            {
                healItem.UseItem(gameObject, gameObject);
                return;
            }

            if (heal.CanUseSkill(gameObject))
            {
                heal.UseSkill(gameObject);
                return;
            }  
        }
        /*if (!AggroInRange())
        {
            if (AttemptAttack())
                return;    
        }*/
        Vector3 stayAway = currentTarget.getCoords();
        Vector3 output = aiding.getCoords() - currentTarget.getCoords();
        //stayAway = stayAway.normalized;
        output = output.normalized;
        Vector3 movingTo;
        if (Mathf.Abs(output.x) > Mathf.Abs(output.z))
        {
            movingTo = Support.SetPosition(this,ClosestSafePoint(stayAway,"z"),map);
        }
        else
        {
            movingTo = Support.SetPosition(this, ClosestSafePoint(stayAway, "x"), map);
        }
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
    }
    private bool AggroInRange()
    {
        foreach(Ability ability in aggro.abilitySet)
        {
            if (ability.CanUseSkill(this.gameObject) && ability.abilityName != "Heal" || ability.CanUseSkill(this.gameObject) && ability.abilityName != "Heal")
                return true;
        }
        return false;
    }
    private bool PlayerInRange()
    {
        foreach (Ability ability in nearest.abilitySet)
        {
            if (ability.CanUseSkill(this.gameObject) && ability.abilityName != "Heal" || ability.CanUseSkill(this.gameObject) && ability.abilityName != "Heal")
                return true;
        }
        return false;
    }
    public Vector3 ClosestSafePoint(Vector3 target, string axis)
    {
        if(axis == "x")
        {
            if (Vector3.Distance(new Vector3(target.x+mostDistance.range_max,target.y,target.z), this.getCoords()) > Vector3.Distance(new Vector3(target.x - mostDistance.range_max, target.y, target.z), this.getCoords()))
            {
                return new Vector3(target.x + mostDistance.range_max, target.y, target.z);
            }
            else
            {
                return new Vector3(target.x - mostDistance.range_max, target.y, target.z);
            }
        }
        else
        {
            if (Vector3.Distance(new Vector3(target.x, target.y, target.z + mostDistance.range_max), this.getCoords()) > Vector3.Distance(new Vector3(target.x , target.y, target.z - mostDistance.range_max), this.getCoords()))
            {
                return new Vector3(target.x , target.y, target.z + mostDistance.range_max);
            }
            else
            {
                return new Vector3(target.x , target.y, target.z - mostDistance.range_max);
            }
        }
       
    }
    private bool CheckStrategy(string type)
    {
        aiding = null;
        foreach (Enemy enemy in EnemyController.enemyList)
        {
            if(enemy.getEnemyID() != this.enemyID && enemy.GetArchetype()== type)
            {
                if(aiding == null)
                {
                    aiding = enemy;
                }
                else if(aiding.getLevel()< enemy.getLevel())
                {
                    aiding = enemy;
                }
            }
        }
        if(aiding != null)
        {
            aiding.aided = true;
            aidLocked = true;
            return true;
        }
        return false;
    }

    public bool CheckDefense()
    {
        if (EnemyController.targeted && !EnemyController.target.aided) // only attach one defender per person or n supports
        {
            aiding = EnemyController.target;
            aiding.aided = true;
        }
        else
            aiding = null;
        if (aiding != null)
        {
            aidLocked = true;
            if (Vector3.Distance(aggro.getCoords(), aiding.getCoords()) < EnemyController.aggroRange)
            {
                currentTarget = aggro;
                return true;
            }
            else
            {
                aiding.UpdateNearest();
                currentTarget = aiding.getNearest();
                return true;
            }
        }
        return false;
    }

    public void GetAbilities()
    {

    }

    public void ResetValues()
    {
        if (SM.checkTurn())
            return;
        if(EnemyController.currentEnemy == enemyID)
            aiding.aided = false;
    }
}
