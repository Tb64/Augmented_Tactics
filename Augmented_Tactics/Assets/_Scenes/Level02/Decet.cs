using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decet : Tank {
    //Decet is a special type of tank that protects only Eery unless he dies
    private Enemy decet;
    
    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        expGiven = 200;
        buff = new BuffDebuff(gameObject, "dexterity", "", false, getWisdom() / 2, false);
        heal = SkillLoader.LoadSkill("curewounds", gameObject);
        debuff = new BuffDebuff(gameObject, "dexterity", "defense", true, getWisdom() / 2, false);
        lastResort = SkillLoader.LoadSkill("poisonarrow", gameObject);
        setManaCurrent(100);
        setMaxMana(100);
        setHealthCurrent(150);
        setMaxHealth(150);
        setWisdom(30);
        setDexterity(15);
        setConstitution(20);
        setIntelligence(25);
        FindDecet();
        closestAggro = decet;
    }

    private void FindDecet()
    {
        foreach (Enemy enemy in EnemyController.enemyList)
            if (enemy.name == "Lord Decet")
                decet = enemy;
        Debug.LogError("No Decet character. Level Not Loaded Correctly");
    }

    public override void EnemyActions()
    {
        base.EnemyActions();
    }

    public override void EnemyTurnStartActions()
    {
        if (getManaCurrent() <= 0)
        {
            setManaCurrent(30); //bosses skip a turn and replenish mana
            setNumOfActions(0);
            return;
        }
        currentTarget = decet;    
        if (decet.isDead())
        {
            base.EnemyTurnStartActions();
            regularMode = false;
        }
        inPosition = false;
        sameTurn = false;   
    }

    public override bool BuffOrDebuff() 
    {
        if (Random.Range(0, 1000) <= 500)
        {
            if (buff.UseSkill(decet.gameObject))
                return true;
            else
                return false;
        }
        else
        {
            if (debuff.UseSkill(decet.gameObject))
                return true;
            else
                return false;
        }
    }
}
