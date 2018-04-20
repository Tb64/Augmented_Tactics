using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimActor : PlayerControlled
{
    public float setHealth;
    public float setMana;

    public string ability1;
    public string ability2;
    public string ability3;
    public string ability4;

    // Use this for initialization
    public void Start()
    {
        Debug.Log("CombatOn = " + combatOn);
        if (!combatOn)
            return;
        PlayerInitialize();

        health_current = setHealth;
        health_max = setHealth;

        mana_current = setMana;
        mana_max = setMana;

        Debug.Log("Abilities = " + abilitySet.Length);

        if (ability1.Length != 0
            || ability2.Length != 0
            || ability3.Length != 0
            || ability4.Length != 0)
        {
            abilitySet[0] = SkillLoader.LoadSkill(ability1, gameObject);
            abilitySet[1] = SkillLoader.LoadSkill(ability2, gameObject);
            abilitySet[2] = SkillLoader.LoadSkill(ability3, gameObject);
            abilitySet[3] = SkillLoader.LoadSkill(ability4, gameObject);
        }
        else
        {
            abilitySet[0] = SkillLoader.LoadSkill("basicattack", gameObject);
            abilitySet[1] = SkillLoader.LoadSkill("heal", gameObject);
            abilitySet[2] = SkillLoader.LoadSkill("fire", gameObject);
            abilitySet[3] = SkillLoader.LoadSkill("combo", gameObject);
        }

        if (data != null)
            LoadStatsFromData(data);
    }

    /***************
     *  Get/Set 
     ***************/

    #region GetSets

    #endregion

}
