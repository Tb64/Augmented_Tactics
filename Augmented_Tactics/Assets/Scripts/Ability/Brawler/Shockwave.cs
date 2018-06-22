using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Ability {
    private string animTrigger = "CastAttack2Trigger";
    private string effectDir = "Effects/Effect18";
    private GameObject effect;

    public Shockwave(GameObject obj)
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
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 1;
        manaCost = 0;
        effect = Resources.Load<GameObject>(effectDir);

        abilityName = "Shockwave";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/blue/blue_24");
        actor.UseMana(actor.getManaCurrent());
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        damage = ((float)actor.getWisdom()) + ((float)actor.getStrength());
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());
        abilityDescription = "A ranged attack that uses strength and wisdom. Cost is a percentage that depends on the level of the brawler. \nMana: " + manaPercent + "%";

    }

    public override void ActionSkill(GameObject target)
    {
        base.ActionSkill(target);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (effect != null)
                Projectile(effect, target);
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        //float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        //target.GetComponent<Actor>().HealHealth(heal);
        float totalDamage = damage + actor.getWeapon().RollPhysicalDamage() - target.GetComponent<Actor>().getPhysicalDefense();
        target.GetComponent<Actor>().TakeDamage(totalDamage, gameObject);
        Debug.Log("Shockwave damage = " + totalDamage + " " + actor.getStrength());

        DwellTime.Attack(dwell_time);
    }
}
