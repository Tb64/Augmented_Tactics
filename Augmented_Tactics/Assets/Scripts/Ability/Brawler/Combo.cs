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
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Combo";
        abilityImage = Resources.Load<Sprite>("UI/Ability/warrior/warriorSkill2");
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
            anim.SetTrigger("Attack1Trigger");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        float damage = 10f + ((float)actor.getStrength() * 0.5f);
        Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);

        DwellTime.Attack(dwell_time);
    }
}
