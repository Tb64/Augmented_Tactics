using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decet : Tank {
    //Decet is a special type of tank that protects only Eery unless he dies
    private Enemy eery;
    
    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        expGiven = 200;
        buff = new BuffDebuff(gameObject, "dexterity", "", false, getWisdom() / 2, false);
        heal = SkillLoader.LoadSkill("curewounds", gameObject);
        debuff = new BuffDebuff(gameObject, "dexterity", "defense", true, getWisdom() / 2, false);
        lastResort = SkillLoader.LoadSkill("poisonarrow", gameObject);
        name = "Lord Decet";
        setManaCurrent(100);
        setMaxMana(100);
        setHealthCurrent(150);
        setMaxHealth(150);
        setWisdom(30);
        setDexterity(15);
        setConstitution(20);
        setIntelligence(25);
        closestAggro = eery;
    }

    private void FindEery()
    {
        foreach (Enemy enemy in EnemyController.enemyList)
            if (enemy.name == "Lord Eery")
                eery = enemy;
        Debug.LogError("No Eery character. Level Not Loaded Correctly");
    }

    public override void EnemyActions()
    {
        base.EnemyActions();
    }

    public override void EnemyTurnStartActions()
    {
        FindEery();
        if (getManaCurrent() <= 0)
        {
            setManaCurrent(30); //bosses skip a turn and replenish mana
            setNumOfActions(0);
            return;
        }
        currentTarget = eery;    
        if (eery.isDead())
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
            if (buff.UseSkill(eery.gameObject))
                return true;
            else
                return false;
        }
        else
        {
            if (debuff.UseSkill(eery.gameObject))
                return true;
            else
                return false;
        }
    }
}
