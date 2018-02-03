using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice : Ability {

    float damage = 20f;

    public Ice(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getIntelligence();

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 5;
        range_min = 0;
        manaCost = 5;
        dwell_time = 1.0f;
        abilityName = "Ice";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");
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
        // when the attack lands spawn a circle, anything touching that circle is hit

        if (target.tag == "Player" || target.tag == "Enemy")
        {
            if (anim != null)
            {
                rotateAtObj(target);
                anim.SetTrigger("MagicAttack");

                //animate fire attack

                actor.PlaySound("attack");
            }
            target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        }
    }

}
