using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureWounds : Ability
{
    private string animTrigger = "CastAttack2Trigger";
    private GameObject handVFX;
    private GameObject effect;

    public CureWounds(GameObject obj)
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
        canHeal = true;
        heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Cure Wounds";
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect7_Hand_Optimized");
        effect = Resources.Load<GameObject>("Effects/Effect21_Optimized");
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/green/green_16");
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
            if (handVFX != null)
                GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform());
            else
                Debug.Log("handVFX null");

            if (effect != null)
                GameObject.Instantiate<GameObject>(effect, target.transform);
            else
                Debug.Log("effect null");
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        //float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        target.GetComponent<Actor>().HealHealth(heal);

        DwellTime.Attack(dwell_time);
    }
}
