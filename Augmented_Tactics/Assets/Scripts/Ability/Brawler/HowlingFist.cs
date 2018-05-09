using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlingFist : Ability
{

    public HowlingFist(GameObject obj)
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
        abilityName = "Howling Fist";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/blue/blue_31");

        actor.UseMana(actor.getManaCurrent());

        if (abilityImage == null)
            Debug.Log("Unable to load image");

        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 1;
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());

        abilityDescription = "An attack that full charges a brawler with energy allowing them to use all of their skills. Cost is a percentage that depends on the level of the brawler.\nMana: " + manaPercent + "%";

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

    }
}
