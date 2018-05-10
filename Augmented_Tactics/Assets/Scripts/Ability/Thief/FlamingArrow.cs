using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingArrow : Ability {

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/CollisionEffects/Effect23_Explosion"), effect2 = Resources.Load<GameObject>("Effects/ArrowShot");

    public FlamingArrow(GameObject obj)
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
        targeta.TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
        if (Ability.DiceRoll(actor.getDexterity(), targeta.getDexterity()))
        {
            if(StatusEffectsController.AddEffect(new Burn(actor.getDexterity() / 2, actor, targeta, target.tag == "Enemy")))
                Debug.Log("Burn Initiated. Stop Drop and Roll " + targeta);
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
        effect2.GetComponent<ArrowShot>().impactVFX = effect1;
        abilityName = "Flaming Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/red/red_13");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
