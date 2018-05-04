﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressive : Enemy {
    //priority: stay in attack mode and go for closest or most aggressive dps / support characters
    //identify type of player by behavior. 
    //contemporary: check for aggressive healing / buffing
    //attack player with most aggro with strongest attack
    //stay near tank for buffing / debuffing
    //stay out of range of support characters

    private bool regularMode, getRange, inRange;
    private Ability strongest, backup, range, buff; //aggressive must have each type of attack. including something to buff attack power or defense
    public string type;

    public Aggressive(string type)
    {
        this.type = type;

    }

    public Aggressive()
    {

    }

    public override void Start ()
    {
        //base.Start();
        EnemyInitialize();
        GetAbilities();
        regularMode = false;
	}

    public override void EnemyInitialize() //temp. changing soon w/ attacks and items etc
    {
        base.EnemyInitialize();
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        getRange = false;
        inRange = false;
    }
    public override void EnemyActions()
    {
        if (regularMode && CheckManaReplenish())
            regularMode = false;
        if (regularMode)
        {
            base.EnemyActions();
            return;
        }
        if (getMoves() == 0)
            return;
        if (getRange)
        {
            Support.FindShweetSpot(this, aggro, strongest, map);
            return;
        }
        if (inRange)
        {
            if (AttemptAbility(strongest, aggro))
                return;
            else if (AttemptAbility(backup, aggro))
                return;
            else
            {
                Debug.LogError("Issue With Algorithm. Attack Should Not Fail");
                return;
            }
                
        }
        if (EnemyController.aggroAggressive)
        {
            EmployStrategy(aggro);
            return;
        }
        else
        {
            EmployStrategy(nearest);
            return;
        }
    }

    private bool CheckManaReplenish()
    {
        if (getManaCurrent() >= strongest.manaCost)
            return true;
        else
            return false;
    }

    private void EmployStrategy(Actor aggro)
    {
        if (strongest.CanUseSkill(aggro.gameObject) && getMoves() > 1)
        {
            if (AttemptAbility(buff, aggro))
                return;
        }
        if (AttemptAbility(strongest, aggro))
        {
            return;
        }
        else if (strongest.manaCost > getManaCurrent())
        {
            //check for mana replenishing item
            //if: use
            //else: revert to basic
            regularMode = true;
            return;
        }
        else
        {
            if (strongest.range_max > 2) //don't want to be in range of direct attack
            {
                if (strongest.range_max < Vector3.Distance(getCoords(), aggro.getCoords()) - moveDistance)
                {
                    Support.FindShweetSpot(this, aggro, strongest, map);
                    getRange = true;
                    return;
                }
                else //You're now in range. R.I.P.
                {
                    Support.FindShweetSpot(this, aggro, strongest, map);
                    inRange = true;
                    return;
                }
            }
        }
    }
    
    private void GetAbilities()
    {
        //add which types
        float bestRange = 0, mostRange = 0;
        buff = abilitySet[3]; // Testing for a buff seems tedious so by default let the buff for an aggressive be in the last slot
        foreach (Ability ability in abilitySet)
        {
            if (ability.range_max > mostRange)
            {
                mostRange = ability.range_max;
                range = ability;
            }
            if (ability.damage > bestRange)
            {
                backup = strongest;
                bestRange = ability.range_max;
                strongest = ability;
            }

        }
        if (backup == range)
        {
            bestRange = 0;
            foreach (Ability ability in abilitySet)
            {
                if (ability != strongest && ability != range && ability.damage > bestRange)
                {
                    bestRange = ability.damage;
                    backup = ability;
                }
            }
        }
    }



}
