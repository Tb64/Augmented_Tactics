using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexArrow : Ability
{

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect6_optimized"), effect2 = Resources.Load<GameObject>("Effects/ArrowShot");

    public VortexArrow(GameObject obj)
    {
        Initialize(obj);
    }
    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");

            if (effect2 != null)
                GameObject.Instantiate<GameObject>(effect2, gameObject.transform);
            else
                Debug.Log("effect2 null");
            anim.SetTrigger(animTrigger);
            anim.SetInteger("Weapon", 7);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        targeta.TakeDamage(damage, target);
        if (Ability.DiceRoll(actor.getDexterity(), targeta.getDexterity()))
        {
            new Beaconed(actor.getDexterity() / 3, actor, targeta, target.tag == "Enemy");
            Debug.Log("{0} now caught in a vortex!",targeta);
        }
        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 10;
        range_max = 5;
        range_min = 1;
        damage = 10f + actor.getDexterity() * 2;
        dwell_time = 1.5f;
        abilityName = "Vortex Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
