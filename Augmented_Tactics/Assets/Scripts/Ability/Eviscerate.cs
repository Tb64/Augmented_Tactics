using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Eviscerate : Ability {

    float damage = 10f;

    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect26");

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
            GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
            parent.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage);
        //Need to add status effect
        //Will apply bleed(damager per turn, 2 turns)
        //Will remove 1 move from enemies next 2 turns

        DwellTime.Attack(dwell_time);
    }

}
