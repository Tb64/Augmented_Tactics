using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability {

    public float heal;

    public Heal(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Heal";
        manaCost = 10;
        heal = actor.getWisdom() + actor.getLevel();
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill5");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        //Debug.Log("Adding " + abilityName + " to " + parent.name);
    }

    public override bool UseSkill(GameObject target)
    {
        if(!base.UseSkill(target))
        {
            return false;
        }
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            target.GetComponent<Actor>().HealHealth(heal);
            DwellTime.Attack(dwell_time);
            return true;
        }
        return false;
    }
}
