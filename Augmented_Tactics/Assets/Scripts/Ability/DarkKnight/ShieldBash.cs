using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : Ability {

    Actor user;

    public ShieldBash(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "shieldbash";
        range_max = 1;
        range_min = 0;
        damage = 20 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassinSkill10");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
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
            anim.Play("ShieldBash");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        //Knockback the enemy?
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        DwellTime.Attack(dwell_time);
    }
}
