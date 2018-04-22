using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eery : Support {

    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        expGiven = 1000;
        abilitySet[0] = SkillLoader.LoadSkill("vortexarrow", gameObject);
        abilitySet[1] = SkillLoader.LoadSkill("poisonarrow", gameObject);
        abilitySet[2] = SkillLoader.LoadSkill("sneak", gameObject);
        abilitySet[3] = SkillLoader.LoadSkill("steal", gameObject);
        setManaCurrent(110);
        setMaxMana(110);
        setHealthCurrent(90);
        setMaxHealth(90);
        setWisdom(30);
        setDexterity(35);
        setConstitution(20);
        setIntelligence(30);
    }

    public override void EnemyTurnStartActions()
    {
        base.EnemyTurnStartActions();
    }
}
