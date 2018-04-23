using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eery : Support {

    private Ability sneak, steal;
    private bool sneakCoolDown;
    private int sneakCountDown;
    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        hasHeal = false;
        sneakCoolDown = false;
        expGiven = 1000;
        strongest = SkillLoader.LoadSkill("vortexarrow", gameObject);
        mostDistance = backup = SkillLoader.LoadSkill("poisonarrow", gameObject);
        sneak = SkillLoader.LoadSkill("sneak", gameObject);
        steal = SkillLoader.LoadSkill("steal", gameObject);
        name = "Lord Eery";
        setManaCurrent(110);
        setMaxMana(110);
        setHealthCurrent(90);
        setMaxHealth(90);
        setWisdom(30);
        setDexterity(35);
        setConstitution(20);
        setIntelligence(30);
    }

    public override void EnemyActions()
    {
        if (getMoves() == 0)
            return;

        if(getManaCurrent() <= 0)
        {
            setManaCurrent(30);
            setNumOfActions(0);
            return;
        }

        if (sneakCoolDown)
        {
            sneakCountDown--;
            if (sneakCountDown <= 0)
                sneakCoolDown = false;
        }
        Actor temp = AbilityInRange(steal);
        if (GetHealthPercent() > 35 && temp != null && Random.Range(0,100)<20) //special cases to use sneak and steal attack. boss ability
        {
            steal.CanUseSkill(temp.gameObject);
            return;
        }

        if(GetHealthPercent() < 35 && sneak.CanUseSkill(gameObject) && !sneakCoolDown)
        {
            sneak.UseSkill(gameObject);
            sneakCoolDown = true;
            sneakCountDown = 4;
            return;
        }

        base.EnemyActions();
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
    }
}
