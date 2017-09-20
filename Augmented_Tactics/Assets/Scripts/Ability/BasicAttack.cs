using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {

    public int range = 1;
    public float damage;
    public string abilityName;

    public override void Initialize()
    {
        base.Initialize();
        anim = GetComponentInChildren<Animator>();
    }

    public override void UseSkill(GameObject target)
    {
        if (SkillInRange(transform.position, target.transform.position) == false)
            return;
        if(target.tag == "Player" || target.tag == "Enemy")
        {
            if (anim != null)
                anim.SetTrigger("MeleeAttack");

            target.GetComponent<Actor>().TakeDamage(damage);
        }
    }
}
