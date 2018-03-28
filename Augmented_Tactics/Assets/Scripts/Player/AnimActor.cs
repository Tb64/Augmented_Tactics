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
    new void Start()
    {
        if (!combatOn)
            return;
        PlayerInitialize();

        animHit = "Attack5Trigger";
        animDmg = "GetHit1Trigger";
        animDeath = "Death1Trigger";

        health_current = setHealth;
        health_max = setHealth;

        mana_current = setMana;
        mana_max = setMana;

        leftHand = transform.Find("Bip01/Bip01_Spine/Bip01_Spine1/Bip01_Spine2/Bip01_Spine3/Bip01_L_Clavicle/Bip01_L_UpperArm/Bip01_L_Forearm/Bip01_L_Hand/Bip01_L_Weapon");
        rightHand = transform.Find("Bip01/Bip01_Spine/Bip01_Spine1/Bip01_Spine2/Bip01_Spine3/Bip01_R_Clavicle/Bip01_R_UpperArm/Bip01_R_Forearm/Bip01_R_Hand/Bip01_R_Weapon");

        leftFoot = transform.Find("Bip01/Bip01_Pelvis/Bip01_L_Thigh/Bip01_L_Calf/Bip01_L_Foot/Bip01_L_Toe0");
        rightFoot = transform.Find("Bip01/Bip01_Pelvis/Bip01_R_Thigh/Bip01_R_Calf/Bip01_R_Foot/Bip01_R_Toe0");

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

    }

    /***************
     *  Get/Set 
     ***************/

    #region GetSets

    #endregion

}
