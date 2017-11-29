using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnit : PlayerControlled
{
    public float setHealth;
    public float setMana;
    // Use this for initialization
    new void Start () {
        Init();
        PlayerInitialize();

        health_current = setHealth;
        health_max = setHealth;

        mana_current = setMana;
        mana_max = setMana;

    
    }
	
}
