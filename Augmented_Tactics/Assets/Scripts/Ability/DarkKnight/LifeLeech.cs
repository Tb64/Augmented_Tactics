using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLeech : Ability
{
    float damage = 10f;
    Actor user;
    GameObject leech = Resources.Load<GameObject>("animation/effect16");

    public LifeLeech(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "lifeleech";
        range_max = 2;
        range_min = 0;
        damage = 10 + actor.getStrength() * 2;
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
            anim.SetTrigger("MeleeAttack");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        GameObject.Instantiate(leech, gameObject.transform);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);

        DwellTime.Attack(dwell_time);
    }

}