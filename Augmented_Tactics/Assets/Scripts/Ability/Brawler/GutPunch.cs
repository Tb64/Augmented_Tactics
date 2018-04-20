using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutPunch : Ability
{
    private GameObject handVFX;

    public GutPunch(GameObject obj)
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
        abilityName = "Gut Punch";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect13_Hand_Optimized");

        actor.UseMana(actor.getManaCurrent());

        if (abilityImage == null)
            Debug.Log("Unable to load image");
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
            if (handVFX != null)
                GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform());
            else
                Debug.Log("handVFX null");
            anim.SetTrigger("Attack2Trigger");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
