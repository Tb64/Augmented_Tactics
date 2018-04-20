using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decet : Tank {

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
    }
}
