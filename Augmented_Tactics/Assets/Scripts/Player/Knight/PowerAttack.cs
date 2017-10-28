using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttack : Ability
{

    
    private int damage;


    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        damage = 20;
    }



}
