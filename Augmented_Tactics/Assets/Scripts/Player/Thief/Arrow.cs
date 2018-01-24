using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ability
{
    float damage = 15f;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getIntelligence();

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 15;
        range_min = 1;
        manaCost = 3;
        dwell_time = 1.0f;
        abilityName = "Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/Arrow");
    }

    public override bool UseSkill(GameObject target)
    {
        if (!base.UseSkill(target))
        {
            return false;
        }

        if (target.tag == "Player" || target.tag == "Enemy")
        {
            if (anim != null)
            {
                rotateAtObj(target);
                anim.SetTrigger("MagicAttack");

                //animate arrow attack

                actor.PlaySound("attack");
            }
            target.GetComponent<Actor>().TakeDamage(damage, gameObject);
            return true;
        }

        return false;
    }
}