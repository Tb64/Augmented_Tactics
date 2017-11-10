using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability {

    public float heal;

    public override bool UseSkill(GameObject target)
    {
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            target.GetComponent<Actor>().HealHealth(heal);

            return true;
        }
        return false;
    }
}
