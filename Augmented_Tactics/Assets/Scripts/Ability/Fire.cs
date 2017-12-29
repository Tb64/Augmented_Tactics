using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Ability
{

    float damage = 20f;
    
    public Fire(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getIntelligence();

        anim = parent.GetComponentInChildren<Animator>();
        range_max = 10;
        range_min = 1;
        manaCost = 5;
        dwell_time = 1.0f;
        abilityName = "Fire";
        abilityImage = Resources.Load<Sprite>("UI/Ability/Fire");
    }

    public override bool UseSkill(GameObject target)
    {
        if(!base.UseSkill(target))
        {
            return false;
        }

        if (target.tag == "Player" || target.tag == "Enemy")
        {
            if (anim != null)
            {
                rotateAtObj(target);
                anim.SetTrigger("MagicAttack");

                //animate fire attack

                actor.PlaySound("attack");
            }
            target.GetComponent<Actor>().TakeDamage(damage);
            return true;
        }

        return false;
    }
}
