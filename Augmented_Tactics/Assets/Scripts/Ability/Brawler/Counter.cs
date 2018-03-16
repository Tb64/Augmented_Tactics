using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Ability {

    string animTrigger = "CastAttack1Trigger";

    public Counter(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = 0f;

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 0;
        range_min = 0;
        manaCost = 5;
        dwell_time = 1.0f;
        abilityName = "Counter";
        abilityImage = Resources.Load<Sprite>("UI/Ability/warrior/warriorSkill5");
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
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        //target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        actor.setCounterAttack(2);
        DwellTime.Attack(dwell_time);
    }
}
