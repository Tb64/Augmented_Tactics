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
    private float distanceFromAggro;
    private Actor aggro;
    private Enemy aiding;
    private Ability strongest, mostDistance;
    private bool regularMode, hasHeal;

    public override void Start()
    {
        base.Start();
        hasHeal = false;
        FindRanges();
    }
    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        GetAggroDistance();
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
        currentTarget = PlayerTooClose();
        if (currentTarget != null)
        {
            if(CheckHeal())
                return;
            else if (strongest.CanUseSkill(currentTarget.gameObject))
            {
                strongest.UseSkill(currentTarget.gameObject);
                return;
            }
            else if (AttemptAttack())
            {
                return;
            }
        }
        aiding = CheckSupport();
        if (aiding == null)
            RunAndGun();
        else
            SaveFriendly();
    }
    private Actor PlayerTooClose()
    {
        foreach(Actor player in EnemyController.userTeam)
        {
            Vector3 temp = player.getCoords();
            if (Vector3.Distance(player.getCoords(), getCoords()) <= 5)
                return player;
        }
        return null;
    }
    private void SaveFriendly()
    {
        aiding.UpdateNearest();
        currentTarget = aiding.getNearest();
        if (AttemptAttack())
            return;
        else
            FindSweetSpot();
    }
    private void RunAndGun() //Default Tactic of Support if no teammate needs help
    {
        Debug.Log(this + " is Running and Gunning");
        if (!mostDistance.SkillInRange(getCoords(),aggro.getCoords()) || distanceFromAggro - mostDistance.range_max > 10 && mostDistance.CanUseSkill(aggro.gameObject))
        {
            FindSweetSpot(); // get closer so attack is possible, or further to stay away from enemies
            return;
        }    
        else if (mostDistance.CanUseSkill(aggro.gameObject))
        {
            mostDistance.UseSkill(aggro.gameObject);
            return;
        }
        else if (mostDistance.manaCost > getManaCurrent())
        {
            Debug.Log("Mana Low. Switching to Regular Mode");
            regularMode = true;
            EnemyActions();
            return;
        }
    }
    private void FindSweetSpot()
    {
        Vector3 position;
        float xDistance = getCoords().x - aggro.getCoords().x;
        float zDistance = getCoords().z - aggro.getCoords().z;
        if (Mathf.Abs(xDistance) > mostDistance.range_max) //check which coord is keeping attack out of range and 
                                                           //move just within distance to keep striking
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
        else
        {
            if (zDistance < 0)
            {
                position = aggro.getCoords() - new Vector3(0, 0, mostDistance.range_max);
            }
            else
            {
                position = aggro.getCoords() + new Vector3(0, 0, mostDistance.range_max);
            }
        }
        Vector3 movingTo = SetPosition(position); //just in case tile is occupied
        Debug.Log("Attempting to move " + this + " from " + this.getCoords() + " to " + movingTo);
        map.moveActorAsync(gameObject, movingTo);
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
    
    private Vector3 SetPosition(Vector3 pos)
    {     
        if (map.UnitCanEnterTile(pos))
            return pos;
        else
            return PosCloseTo(pos);
    }
    private void FindRanges()
    {
        float bestRange = 0, mostRange=0;
 
        foreach (Ability ability in abilitySet)
        {
            if (ability.range_max > mostRange)
            {
                mostRange = ability.range_max;
                mostDistance = ability;
            }
            if(ability.damage > bestRange)
            {
                bestRange = ability.range_max;
                strongest = ability;
            }
            if (ability.abilityName == "Heal")
                hasHeal = true;
        }
    }

    private Enemy CheckSupport()
    {
        if (!EnemyController.targeted)
            return null;
        else
            return EnemyController.target;
    }

    private void GetAggroDistance()
    {
        distanceFromAggro = Vector3.Distance(getCoords(), aggro.getCoords());
    }
    public override bool AttemptAttack()
    {
        return true;
    }
}
