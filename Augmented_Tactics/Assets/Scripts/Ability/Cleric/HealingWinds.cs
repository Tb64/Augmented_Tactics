using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWinds : AOE {
    private string animTrigger = "Attack4Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/yellow/yellow_41";
    private HealOverTime statuseffect;

    public HealingWinds(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        canHeal = true;
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        AOESizeMin = 0;
        AOESizeMax = 1;
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        damage = actor.getConstitution() + actor.getStrength();
        //heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Vengeance";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "Area of Effect health regeneration.";


    }

    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MagicCast");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        for (int i = 0; i < listIterActor; i++)
        {
            if (listOfActorsAffected[i] != null)
                statuseffect = new HealOverTime(heal, actor, listOfActorsAffected[i].GetComponent<Actor>(), false);
        }

        DwellTime.Attack(dwell_time);
    }
}
