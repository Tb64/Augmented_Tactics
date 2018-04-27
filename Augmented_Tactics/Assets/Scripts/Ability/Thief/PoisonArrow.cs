using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : Ability
{

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect12_Optimized"), effect2 = Resources.Load<GameObject>("Effects/ArrowShot");

    public PoisonArrow(GameObject obj)
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
            {
                effect2.GetComponent<ArrowShot>().SetTarget(target.name);
                GameObject.Destroy(GameObject.Instantiate<GameObject>(effect2, Actor.PosInFrontOf(actor, targeta), gameObject.transform.rotation), 3);
            }      
            else
                Debug.Log("effect2 null");
            anim.SetTrigger(animTrigger);
            anim.SetInteger("Weapon", 7);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        DwellTime.Attack(dwell_time);
        targeta.TakeDamage(damage, target);
        if (Ability.DiceRoll(actor.getDexterity(), targeta.getDexterity()))
        {
            if(StatusEffectsController.AddEffect(new Poisoned(actor.getDexterity() / 4, actor, targeta, target.tag == "Enemy")))
                Debug.Log(targeta+ " poisioned. Suck Out The Venom?");
            else
                Debug.Log(this + " Effect Already Exists On " + targeta);
        }



        
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 0;
        range_max = 7;
        range_min = 1;
        damage = 10f + actor.getDexterity() * 1.5f;
        dwell_time = 3f;
        abilityName = "Poison Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}

