using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : Ability
{

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/CollisionEffects/Effect12_Explosion"), effect2 = Resources.Load<GameObject>("Effects/ArrowShot");
     
    public PoisonArrow(GameObject obj)
    {
        Initialize(obj);
        effect2.GetComponent<ArrowShot>().impactVFX = effect1;
    }
    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            //if (effect1 != null)
            //    Projectile(effect1, target);
            //else
            //    Debug.Log("effect1 null");

            if (effect2 != null)
            {
                //effect2.GetComponent<ArrowShot>().SetTarget(target.name);
                Projectile(effect2, target);
                //GameObject.Destroy(GameObject.Instantiate<GameObject>(effect2, Actor.PosInFrontOf(actor, targeta), gameObject.transform.rotation), 3);
            }      
            else
                Debug.Log("effect2 null");
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        DwellTime.Attack(dwell_time);
        targeta.TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
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
        manaCost = 15;
        range_max = 7;
        range_min = 1;
        damage = 10f + actor.getDexterity() * 1.5f;
        dwell_time = 3f;
        abilityName = "Poison Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/green/green_01");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}

