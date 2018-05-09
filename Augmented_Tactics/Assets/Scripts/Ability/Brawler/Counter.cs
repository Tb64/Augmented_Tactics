using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Ability {

    string animTrigger = "CastAttack1Trigger";

    public Counter(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        damage = 0f;

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 0;
        range_min = 0;
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 1;
        dwell_time = 1.0f;
        abilityName = "Counter";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/blue/blue_12");
        actor.UseMana(actor.getManaCurrent());
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());

        abilityDescription = "Brawler Buff allowing 1 melee attack during the following enemy turn to be countered with brawler's pure strength. Cost is a percentage that depends on the level of the brawler.\nMana: " + manaPercent + "%";

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
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        //target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        actor.setCounterAttack(2);
        DwellTime.Attack(dwell_time);
    }
}
