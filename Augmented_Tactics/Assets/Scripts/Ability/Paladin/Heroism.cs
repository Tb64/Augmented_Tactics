using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroism : Ability
{
    private string animTrigger = "CastAttack2Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/yellow/yellow_10";

    public Heroism(GameObject obj)
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
        heal = 0;
        manaCost = 5f * actor.getLevel();
        abilityName = "Heroism";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "Grants a person the ability to counter attack.";
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
        //float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        //target.GetComponent<Actor>().HealHealth(heal);
        target.GetComponent<Actor>().setCounterAttack(2);

        DwellTime.Attack(dwell_time);
    }
}
