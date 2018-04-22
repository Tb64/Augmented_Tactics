using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortifiedStrike : Ability {

    private string animTrigger = "CastAttack2Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/yellow/yellow_33";

    public FortifiedStrike(GameObject obj)
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
        damage = actor.getStrength();
        manaCost = actor.getConstitution();
        abilityName = "Fortified Strike";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "A strike that increases the max health of the user. Each use increases in cost.";
    }

    public override void ActionSkill(GameObject target)
    {
        base.ActionSkill(target);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);

            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        Actor targetActor = target.GetComponent<Actor>();
        float damageCalc = (damage) - targetActor.getPhysicalDefense() + actor.getWeapon().RollPhysicalDamage();
        targetActor.TakeDamage(damageCalc, gameObject);
        actor.setMaxHealth((int)actor.GetHeathMax() + actor.getLevel());

        manaCost += 5f;

        DwellTime.Attack(dwell_time);
    }
}
