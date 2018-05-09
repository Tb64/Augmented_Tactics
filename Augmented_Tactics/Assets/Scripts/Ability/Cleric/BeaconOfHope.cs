using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconOfHope : AOE
{
    private string animTrigger = "CastAttack1Trigger";
    private GameObject handVFX;

    public BeaconOfHope(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        AOESizeMin = 0;
        AOESizeMax = 1;
        anim = gameObject.GetComponentInChildren<Animator>();
        canTargetTile = true;
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        heal = 5f + (float)actor.getWisdom() * 1.25f;
        manaCost = actor.getLevel() * 10 + 20;
        abilityName = "Beacon Of Hope";
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect13_Hand_Optimized");
        abilityImage = Resources.Load<Sprite>("UI/Ability/priest/priestSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "Area of effect heal based on wisdom. \nHeal = " + heal;
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
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MagicCast");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        for (int i = 0; i < listIterActor; i++)
        {
            if (listOfActorsAffected[i] != null)
                listOfActorsAffected[i].HealHealth(heal);
        }

        DwellTime.Attack(dwell_time);
    }
}
