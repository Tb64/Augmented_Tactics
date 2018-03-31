using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSkill : Ability {

    public float heal;

    public PotionSkill(GameObject obj)
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
        abilityName = "Potion";
        manaCost = 0;
        heal = 10f;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill5");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        //Debug.Log("Adding " + abilityName + " to " + parent.name);
    }

    public override bool UseSkill(GameObject target)
    {
        if (!base.UseSkill(target))
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
