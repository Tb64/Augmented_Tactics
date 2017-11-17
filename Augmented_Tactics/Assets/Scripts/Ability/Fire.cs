using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Ability
{

    float damage = 20f;
    float manaCost = 10f;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getIntelligence();

        anim = parent.GetComponentInChildren<Animator>();
        range_max = 10;
        range_min = 1;
        dwell_time = 1.0f;
        abilityName = "Fire";
        abilityImage = Resources.Load<Sprite>("UI/Ability/Fire");
    }

    public override bool UseSkill(GameObject target)
    {
        base.UseSkill(target);

        if (target == null)
            return false;

        if (target.tag == "Player" || target.tag == "Enemy")
        {
            if (SkillInRange(parent, target) == false)
            {
                Debug.Log("Out of range.");
                return false;
            }
            if (!actor.UseMana(manaCost))
            {
                Debug.Log("Not enough mana");
                return false;
            }
            if (!actor.useAction())
            {
                Debug.Log("Not enough actions");
                return false;
            }
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
