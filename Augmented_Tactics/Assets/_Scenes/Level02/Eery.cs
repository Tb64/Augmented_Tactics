using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eery : Support {

    private Ability sneak, steal;
    private bool sneakCoolDown;
    private int sneakCountDown;
  
    public override void EnemyInitialize()
    {
        boss = true;
        base.EnemyInitialize();
        base.Init();
        aggroScore = 0;

        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<TileMap>();
        }
        hasHeal = false;
        sneakCoolDown = false;
        expGiven = 1000;
        strongest = SkillLoader.LoadSkill("vortexarrow", gameObject);
        mostDistance = backup = SkillLoader.LoadSkill("poisonarrow", gameObject);
        sneak = SkillLoader.LoadSkill("sneak", gameObject);
        steal = SkillLoader.LoadSkill("steal", gameObject);
        name = "Lord Eery";
        setManaCurrent(30);
        setMaxMana(30);
        setHealthCurrent(30);
        setMaxHealth(30);
        setWisdom(8);
        setDexterity(15);
        setConstitution(5);
        setIntelligence(5);
    }

    public override bool IsBoss()
    {
        return true;
    }

    public override void EnemyActions()
    {
        Debug.Log("Eery Moves: " + getMoves());
        if (getMoves() == 0)
            return;
        if(getManaCurrent() <= 0)
        {
            setManaCurrent(30);
            setNumOfActions(0);
            TurnBehaviour.EnemyTurnFinished();
            return;
        }

        if (sneakCoolDown)
        {
            sneakCountDown--;
            if (sneakCountDown <= 0)
                sneakCoolDown = false;
        }
        //Actor temp = AbilityInRange(steal);
        /*if (GetHealthPercent() > 35 && temp != null && Random.Range(0,100)<20) //special cases to use sneak and steal attack. boss ability
        {
            steal.CanUseSkill(temp.gameObject);
            return;
        } taking this out only for 4/26 presentation. relies on usableItem
        if(GetHealthPercent() < .35 && sneak.CanUseSkill(gameObject) && !sneakCoolDown)
        {
            sneak.UseSkill(gameObject);
            sneakCoolDown = true;
            sneakCountDown = 4;
            return;
        }*/ //removed for pres

        base.EnemyActions();
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
    }
}
