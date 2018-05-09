using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : Ability
{
    private string animTrigger = "Attack4Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/yellow/yellow_24";
    private GameObject handVFX;

    public Smite(GameObject obj)
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
        damage = (float)actor.getStrength() + (float)actor.getConstitution() + (float)actor.getWisdom();
        manaCost = 4f + actor.getLevel();
        abilityName = "Smite";
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect4_Hand_Optimized");
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "A magic based melee attack.";
    }

    public override void ActionSkill(GameObject target)
    {
        base.ActionSkill(target);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}. Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);

            if (handVFX != null)
            {
                GameObject.Destroy(GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform()), dwell_time);
            }

            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        Actor targetActor = target.GetComponent<Actor>();
        float damageCalc = damage - targetActor.getMagicalDefense() + actor.getWeapon().RollPhysicalDamage();
        targetActor.TakeDamage(damageCalc, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
