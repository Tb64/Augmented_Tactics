using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy {

    private int skillChoice;
    private bool firstrun, threatened,firstheal, healing;
    private List<Actor> attackList;
    private List<Enemy> healList;
    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        expGiven = 1000;
        abilitySet[0] = SkillLoader.LoadSkill("ice", gameObject); //until more long range magic abilities are made
        abilitySet[1] = SkillLoader.LoadSkill("curewounds", gameObject);
        abilitySet[2] = SkillLoader.LoadSkill("counter", gameObject);
        abilitySet[3] = SkillLoader.LoadSkill("thunder", gameObject);
        setManaCurrent(250);
        setMaxMana(250);
        setHealthCurrent(300);
        setMaxHealth(300);
        setWisdom(75);
        setDexterity(45);
        setConstitution(60);
        setIntelligence(70);
        StatusEffectsController.AddEffect(new ArmySpawn(0, this, this, true));
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        setNumOfActions(Random.Range(3, 6));
        attackList = new List<Actor>();
        healList = new List<Enemy>();
        firstrun = true;
        firstheal = true;
        threatened = false;
    }

    public override bool EnemyActions()
    {
        if (getManaCurrent() <= 0)
        {
            setManaCurrent(50);
            setNumOfActions(0);
            return false;
            //boss can recover mana but it ends his turn. Thinking about ending the entire enemy turn here
        }
            
        //all of the bools return true only a move is wasted
        if (getMoves() == 0)
            return false;

        if (firstrun)
        {
            CheckThreats();
            firstrun = false;
        }

        if (threatened)
        {
            return HitThreats();
        }

        if (firstheal)
        {
            CheckGuards();
            firstheal = false;
        }

        if (healing)
        {
            return HealGuards();
        }
        if (GetHealthPercent() < .40)
            return abilitySet[1].UseSkill(gameObject);

        //if (UseMoves())
        //    return;
        //else
        // {
        setNumOfActions(1); //no more moves after counter
        return abilitySet[2].UseSkill(this.gameObject); //all else has either passed or failed so counter as a last resort counter
        
        //}*/
            
        
    }

    /*private bool UseMoves()//assess if an attack is possible and in range or moves to a location where
        //attack is possible if enough moves are left. Sets currentTarget
        //decided not to implement
    {
        if (getMoves() == 0)
            return true;

        if (getMoves() > 2)
        {
            foreach(Actor player in EnemyController.userTeam)
            {
                currentTarget = player;
                if (TargetInRange())
                {
                    if(Punish(player)) //keep attacking with rest of actions
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        else
        {
            return false;
        }        
    }*/

    private void CheckThreats()//assess if players are too close or danger is in range.
        //decides to either move away or eliminate the threat
    {
        threatened = false;
        foreach (Actor player in EnemyController.userTeam)
        {
            if (Vector3.Distance(getCoords(), player.getCoords()) < abilitySet[0].range_max)//update to range of attacks
            {
                attackList.Add(player);
                threatened = true;
            }
        }
    }

    private bool HitThreats()//hit each person on previously made hit-list. updating for using correct attack against player
    {
        bool hit = abilitySet[0].UseSkill(attackList[0].gameObject);
        attackList.Remove(attackList[0]);
        if (attackList.Count == 0)
        {
            threatened = false;
            Debug.Log("No Longer Threatened, Aggressive Mode Off");
            return true;
        }
        return hit;    
    }

    private bool HealGuards() // heals allies that are close enough and need it including self
    {
        bool healed = abilitySet[1].UseSkill(healList[0].gameObject);
        healList.Remove(healList[0]);
        if (healList.Count == 0)
        {
            healing = false;
            Debug.Log("No Longer Healing, Aggressive Mode Off");
        }
        return healed;
    }

    private bool CheckGuards()
    {
        threatened = false;
        foreach (Enemy enemy in EnemyController.enemyList)
        {
            if (enemy.GetHealthPercent() < .35 && Vector3.Distance(getCoords(), enemy.getCoords()) < abilitySet[1].range_max)
            {
                healList.Add(enemy);
                healing = true;
            } 
        }
        return false;
    }
}
