using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decet : Tank {
    //Decet is a special type of tank that protects only Eery unless he dies
    private Enemy eery;
    private bool supportMode;
    
    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        base.Init();
        aggroScore = 0;
  
        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }

        expGiven = 200;
        name = "Lord Decet";
        setManaCurrent(30);
        setMaxMana(30);
        setHealthCurrent(40);
        setMaxHealth(40);
        setWisdom(10);
        setDexterity(3);
        setConstitution(5);
        setIntelligence(10);
        buff = new BuffDebuff(gameObject, "dexterity", "", false, true, getWisdom(), false);
        heal = SkillLoader.LoadSkill("curewounds", gameObject);
        debuff = new BuffDebuff(gameObject, "dexterity", "defense", true, true, getWisdom(), false);
        lastResort = SkillLoader.LoadSkill("fire", gameObject);
        supportMode = false;
        regularMode = false;
    }

    public override bool IsBoss()
    {
        return true;
    }

    private void FindEery()
    {
        foreach (Enemy enemy in EnemyController.enemyList)
            if (enemy.name == "Lord Eery")
            {
                eery = enemy;
                return;
            }
               
        Debug.LogError("No Eery character. Level Not Loaded Correctly");
    }

    public override void EnemyActions()
    {
        base.EnemyActions();
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
        FindEery();
        if (eery == null)
        {
            Debug.LogError("Eery not Instatiating Correctly");
            supportMode = true;
        }
            
        if (eery != null && eery.isDead() || eery.isIncapacitated())
            supportMode = true;
        if (supportMode)
        {
            return;
        }

        closestAggro = eery;
        currentTarget = eery.getNearest();
        if (getManaCurrent() <= 0)
        {
            setManaCurrent(30); //bosses skip a turn and replenish mana
            setNumOfActions(0);
            TurnBehaviour.EnemyTurnFinished();
            return;
        }
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
