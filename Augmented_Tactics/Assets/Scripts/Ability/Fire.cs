using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Ability
{

    // float damage = 20f;
    GameObject effect;
    
    public Fire(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getIntelligence()/3;

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 1;
        manaCost = 10;
        dwell_time = 1.0f;
        abilityName = "Fire";
        abilityImage = Resources.Load<Sprite>("UI/Ability/Fire");
        effect = Resources.Load<GameObject>("Effects/Effect23");
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
                if (effect != null)
                    GameObject.Instantiate(effect, gameObject.transform);

                actor.PlaySound("attack");
            }
            target.GetComponent<Actor>().TakeDamage(damage,gameObject);
            DwellTime.Attack(dwell_time);
            return true;
        }

        return false;
    }
}
