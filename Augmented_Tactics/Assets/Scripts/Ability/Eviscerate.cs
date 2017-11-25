using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eviscerate : Ability {

    float damage = 10f;

    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy

    public Eviscerate(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = parent.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "Eviscerate";
        range_max = 1;
        range_min = 0;
        damage = actor.getStrength() * 2;

        manaCost = 0;
    }


    public override bool UseSkill(GameObject target)
    {
        if (base.UseSkill(target))
        {
            Skill(target);
            
            return true;
        }
        else
        {
            return false;
        }

    }

    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.SetTrigger("MeleeAttack");
            parent.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage);

        DwellTime.Attack(dwell_time);
    }

}
