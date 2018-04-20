using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortifiedStrike : Ability {

    private string animTrigger = "CastAttack2Trigger";
    const string SpiriteDir = "UI/Ability/priest/priestSkill1";

    public FortifiedStrike(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Fortified Strike";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");
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
        float damageCalc = (damage * (1f - actor.GetHealthPercent())) - targetActor.getPhysicalDefense() + actor.getWeapon().RollPhysicalDamage();
        targetActor.TakeDamage(damageCalc, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
