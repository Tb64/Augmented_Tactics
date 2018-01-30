﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AOE
{
    public Thunder(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        AOESizeMin = 0;
        AOESizeMax = 2;

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Thunder";
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill1");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    public override bool UseSkill(GameObject target)
    {
        if (base.UseSkill(target))
        {
            AOEBase(target);
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
            anim.SetTrigger("MagicCast");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        for (int i = 0; i < 8; i++)
        {
            if (listOfAffected[i] != null)
                listOfAffected[i].TakeDamage(50, gameObject);
        }
    }
}