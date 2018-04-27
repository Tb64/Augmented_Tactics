using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead_Knight : Enemy
{
    public float setHealth;
    public float setMana;

    public string skill1;
    public string skill2;
    public string skill3;
    public string skill4;
    // Use this for initialization
    void Start () {
        Init();
        EnemyInitialize();

        health_current = setHealth;
        health_max = setHealth;

        mana_current = setMana;
        mana_max = setMana;

        LoadSkills();
    }

    void LoadSkills()
    {
        if (skill1.Length > 0)
            abilitySet[0] = SkillLoader.LoadSkill(skill1, gameObject);
        if (skill2.Length > 0)
            abilitySet[1] = SkillLoader.LoadSkill(skill2, gameObject);
        if (skill3.Length > 0)
            abilitySet[2] = SkillLoader.LoadSkill(skill3, gameObject);
        if (skill4.Length > 0)
            abilitySet[3] = SkillLoader.LoadSkill(skill4, gameObject);
    }
	
}
