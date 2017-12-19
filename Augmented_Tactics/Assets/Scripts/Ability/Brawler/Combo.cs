using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : Ability {

    public Combo(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = parent.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Cyclone Kick";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");
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
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, parent.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MeleeAttack");
            //justin set attack string array choice hereS
            parent.GetComponent<Actor>().PlaySound("attack");
        }
        float damage = 10f + ((float)actor.getStrength() * 0.5f);
        target.GetComponent<Actor>().TakeDamage(damage);

        DwellTime.Attack(dwell_time);
    }
}
