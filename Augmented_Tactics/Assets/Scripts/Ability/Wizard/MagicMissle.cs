﻿using System.Collections;
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
        dwell_time = .5f;
        abilityName = "magicmissle";
        range_max = 4;
        range_min = 1;
        damage = 5 + actor.getMaxMana()/10;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 15;
        missleEffect = Resources.Load<GameObject>("animation/Effect1_Optimized");
    }
    
    public override void ActionSkill(GameObject target)
    {

        rotateAtObj(target);
        if (anim != null)
        {
            anim.SetTrigger("Missle");            
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        if (missleEffect != null)
            Projectile(missleEffect, target);

        DwellTime.Attack(dwell_time);

    }
}
