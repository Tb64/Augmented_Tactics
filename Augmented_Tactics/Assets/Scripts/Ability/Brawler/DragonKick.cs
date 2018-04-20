using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonKick : Ability
{
    string animTrigger = "AttackKick1Trigger";
    GameObject footVFX;

    public DragonKick(GameObject obj)
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
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 3;
        damage = 10f + ((float)actor.getStrength() * 0.5f);
        footVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect6_Hand");
        abilityName = "Dragon Kick";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");
        actor.UseMana(actor.getManaCurrent());
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());

        abilityDescription = "Dragon Kick will knockback a target 1 block away from the Brawler. If the target can not be knocked back they will take 25% more damage. Cost is a percentage that depends on the level of the brawler.\nMana: " + manaPercent + "%";
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
        Actor targetActor = target.GetComponent<Actor>();
        Vector3 delta = targetActor.getCoords() - actor.getCoords();
        delta += targetActor.getCoords();
        delta.y = 0f;

        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (footVFX != null)
                GameObject.Instantiate<GameObject>(footVFX, actor.LeftFootTransform());
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        if (!targetActor.KnockBack(delta))
            targetActor.TakeDamage(damage * 1.25f, gameObject);
        else 
            targetActor.TakeDamage(damage, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
