using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vengeance : Ability {
    private string animTrigger = "Attack4Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/yellow/yellow_41";

    public Vengeance(GameObject obj)
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
        damage = actor.getConstitution() + actor.getStrength();
        //heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Vengeance";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "An attack that receives bonus damage for the amount of health lost.";


    }

    public override void ActionSkill(GameObject target)
    {
        Actor targetActor = target.GetComponent<Actor>();
        base.ActionSkill(target);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);

            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        float damageCalc = (damage * (2f - actor.GetHealthPercent())) - targetActor.getPhysicalDefense() + actor.getWeapon().RollPhysicalDamage();
        targetActor.TakeDamage(damageCalc, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
