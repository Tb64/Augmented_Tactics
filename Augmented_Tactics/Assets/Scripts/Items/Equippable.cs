using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipable : Items {
    public string slot;
    public string model;

    public string class_req;
    public int level_req;

    public float crit_chance;

    public int str_bonus;
    public int dex_bonus;
    public int con_bonus;
    public int wis_bonus;
    public int int_bonus;
    public float eva_bonus;
    public float block_bonus;
    public float acc_bonus;
}
