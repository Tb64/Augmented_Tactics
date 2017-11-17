﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability {

    public float heal;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = parent.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Heal";
        abilityImage = Resources.Load<Sprite>("UI/Ability/warriorSkill3");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        //Debug.Log("Adding " + abilityName + " to " + parent.name);
    }

    public override bool UseSkill(GameObject target)
    {
        if(base.UseSkill(target))
        {
            return false;
        }
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            target.GetComponent<Actor>().HealHealth(heal);

            return true;
        }
        return false;
    }
}
