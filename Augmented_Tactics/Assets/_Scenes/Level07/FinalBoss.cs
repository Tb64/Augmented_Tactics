using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy {

    public override void EnemyInitialize()
    {
        base.EnemyInitialize();
        expGiven = 1000;
        abilitySet[0] = SkillLoader.LoadSkill("thunder", gameObject);
        abilitySet[1] = SkillLoader.LoadSkill("curewounds", gameObject);
        abilitySet[2] = SkillLoader.LoadSkill("counter", gameObject);
        abilitySet[3] = SkillLoader.LoadSkill("eviscerate", gameObject);
        setManaCurrent(150);
        setMaxMana(150);
        setHealthCurrent(300);
        setMaxHealth(300);
        setWisdom(75);
        setDexterity(45);
        setConstitution(60);
        setIntelligence(70);
    }

    public override void EnemyActions()
    {
        setNumOfActions(Random.Range(3,6));
    }
}
