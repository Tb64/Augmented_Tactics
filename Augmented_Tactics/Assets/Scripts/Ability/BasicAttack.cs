using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {

    public float damage;
    public string abilityName;

    public override void UseSkill(GameObject target)
    {
        if(target.tag == "Player" || target.tag == "Enemy")
        {
            target.GetComponent<Actor>().TakeDamage(damage);
        }
    }
}
