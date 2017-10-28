using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead_Knight : Enemy
{
    public float setHealth;
    public float setMana;
    // Use this for initialization
    void Start () {
        Init();
        EnemyInitialize();

        health_current = setHealth;
        health_max = setHealth;

        mana_current = setMana;
        mana_max = setMana;
    }
	
}
