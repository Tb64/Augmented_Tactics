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
}
