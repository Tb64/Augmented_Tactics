using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneKick : AOE {

    float BASE_DAMAGE = 10f;
    float DEX_SCALER = 0.5f;
    float STR_SCALER = 0.5f;

    float damage;

    Actor targetActor;

    string animTrigger = "";

    public CycloneKick(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 0;
        range_min = 0;
        AOESizeMin = 0;
        AOESizeMax = 2;
        dwell_time = 1.0f;
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 4;
        abilityName = "Cyclone Kick";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");
        actor.UseMana(actor.getManaCurrent());

        if (abilityImage == null)
            Debug.Log("Unable to load image");
        float damage =
            BASE_DAMAGE * actor.getLevel() +
            DEX_SCALER * actor.getDexterity() +
            STR_SCALER * actor.getStrength();
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());

        abilityDescription = "An area attack that attacks all adjacent enemies. Cost is a percentage that depends on the level of the brawler.\nMana: " + manaPercent + "%";

    }

    public override bool UseSkill(GameObject target)
    {
        if(base.UseSkill(target))
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
            if (listOfActorsAffected[i] != null && listOfActorsAffected[i].tag != actor.tag)
                listOfActorsAffected[i].TakeDamage(damage, gameObject);
        }

        DwellTime.Attack(dwell_time);
    }
}
