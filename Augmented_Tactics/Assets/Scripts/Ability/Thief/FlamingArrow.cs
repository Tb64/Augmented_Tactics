using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingArrow : Ability {

    private string animTrigger = "Arrow";
    private GameObject effect1, effect2; 

    public FlamingArrow(GameObject obj)
    {
        Initialize(obj);
        effect1 = null;
        effect2 = null;
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
            new Burn(actor.getDexterity() / 3, actor, targeta, target.tag == "Enemy");
            Debug.Log("Burn Initiated. Stop Drop and Roll Bitch");
        }
           


        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 10;
        range_max = 7;
        range_min = 1;
        damage = 10f + actor.getDexterity() * 2;
        dwell_time = 1.0f;
        abilityName = "Flaming Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill8");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        effect1 = Resources.Load<GameObject>("Effects/Effect15_Optimized");
        //effect2 = Resources.Load<GameObject>();
    }
}
