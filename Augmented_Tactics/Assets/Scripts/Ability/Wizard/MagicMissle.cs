using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : Ability {

    GameObject missleEffect;

    public MagicMissle(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 3.5f;
        abilityName = "magicmissle";
        range_max = 5;
        range_min = 0;
        damage = 10 + actor.getMaxMana() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 10;
        missleEffect = Resources.Load<GameObject>("animation/Effect1_Optimized");
    }
    
    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);

            anim.SetTrigger("Missle");
            if (missleEffect != null)
                Projectile(missleEffect, target);
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        DwellTime.Attack(dwell_time);

    }
}
