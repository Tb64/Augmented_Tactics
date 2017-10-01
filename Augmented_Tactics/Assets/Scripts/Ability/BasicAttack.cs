using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {

    public float damage = 10f;

    

    public BasicAttack(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        Debug.Log(parent.name);
        anim = parent.GetComponentInChildren<Animator>();
        range = 1;
        abilityName = "Basic Attack";
    }

    public override void UseSkill(GameObject target)
    {
		if(target == null)
		{
			Debug.Log("Target is null.");
			return;
		}
        if (SkillInRange(transform.position, target.transform.position) == false)
        {
            Debug.Log("Ability out of range. Range is " + range);
            return;
        }

        if(target.tag == "Player" || target.tag == "Enemy")
        {
            if (anim != null)
                anim.SetTrigger("MeleeAttack");

            target.GetComponent<Actor>().TakeDamage(damage);
        }
        else
        {
            Debug.Log("Target is not an Actor");
        }
    }
}
