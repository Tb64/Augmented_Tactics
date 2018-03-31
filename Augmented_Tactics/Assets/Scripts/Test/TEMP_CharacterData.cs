using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TEMP_CharacterData {

    public string name;
    public string classType;

    public float health_current;    // temporary for debugging purposes(should be protected)
    public float health_max;
    public float mana_current;
    public float mana_max;
    public float move_speed;
    public float armor_class;

    public int level;            //current actor's level

    public int pDefense;
    public int mDefense;

    public int pAttack_min;
    public int pAttack_max;
    public int mAttack_min;
    public int mAttack_max;

    public int strength;         //measuring physical power
    public int dexterity;        //measuring agility
    public int constitution;     //measuring endurance
    public int intelligence;     //measuring reasoning and memory (Magic Damage)
    public int wisdom;           //measuring perception and insight (Resistance/Healing)
//    public int charisma;         //measuring force of personality (Buffs and Debuffs)

    public int experience;



    public string icon;
    public string prefab;
    public string[] skills;
}
