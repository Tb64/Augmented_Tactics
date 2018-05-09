using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ability
{
    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/ArrowShot");

    public Arrow(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = actor.getDexterity();

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 7;
        range_min = 1;
        manaCost = 0;
        dwell_time = 3.0f;
        abilityName = "Arrow";
        abilityImage = Resources.Load<Sprite>("UI/WeaponAndArmorIconPack/black/arrows/ar_b_03");
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (effect1 != null)
            {
                Projectile(effect1, target);
            }
            else
                Debug.LogError("effect1 null");
            anim.SetTrigger(animTrigger);
            anim.SetInteger("Weapon", 7);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        DwellTime.Attack(dwell_time);
        targeta.TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
    }
}