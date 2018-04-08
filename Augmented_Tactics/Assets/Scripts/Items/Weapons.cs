using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapons : Equipable
{
    //name is inherited

    public string type;

    public int physical_dmg_min;
    public int physical_dmg_max;

    public int magic_dmg_min;
    public int magic_dmg_max;

    public float crit_scaler;

    public int RollPhysicalDamage()
    {
        int damage = Random.Range(physical_dmg_min, physical_dmg_max);
        if(damage == physical_dmg_max)
        {
            //crit
            damage = damage * (int)(1f + crit_scaler);
        }
        return damage;
    }

    public int RollMagicDamage()
    {
        int damage = Random.Range(magic_dmg_min, magic_dmg_max);
        if (damage == magic_dmg_max)
        {
            //crit
            damage = damage * (int)(1f + crit_scaler);
        }
        return damage;
    }
}
