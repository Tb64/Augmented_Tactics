using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrow : Ability
{

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9"), effect2 = Resources.Load<GameObject>("Effects/ArrowShot");

    public IceArrow(GameObject obj)
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
                GameObject.Destroy(GameObject.Instantiate<GameObject>(effect1, gameObject.transform),5);
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
            if(StatusEffectsController.AddEffect(new Frozen(0, actor, targeta, target.tag == "Enemy")))
                Debug.Log(targeta+ " is frozen solid!"); //issue here with animation
            else
                Debug.Log(this + " Effect Already Exists On " + targeta);
        }



        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 10;
        range_max = 6;
        range_min = 1;
        damage = 10f + actor.getDexterity() * 2;
        dwell_time = 5f;
        abilityName = "Ice Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}

