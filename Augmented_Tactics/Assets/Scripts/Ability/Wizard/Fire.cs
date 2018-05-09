using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Ability
{

    // float damage = 20f;
    GameObject effect;
    
    public Fire(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        damage = 10 + (actor.getIntelligence()==0? 0 : actor.getIntelligence()/3);
        dwell_time = 1.0f;        
        range_max = 3;
        range_min = 1;
        manaCost = 20;
        
        abilityName = "Fire";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/red/red_20");
        effect = Resources.Load<GameObject>("Effects/Effect23");
    }

    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);

            anim.SetTrigger("Missle");
            if (effect != null)
                Projectile(effect, target);
            gameObject.GetComponent<Actor>().PlaySound("attack");
            /*
            Actor targeta = target.GetComponent<Actor>();
            if (Ability.DiceRoll(actor.getDexterity(), targeta.getDexterity()))
            {
                StatusEffectsController.AddEffect(new Burn(actor.getDexterity() / 3, actor, targeta, target.tag == "Enemy"));
                Debug.Log("Burn Initiated. Stop Drop and Roll Bitch");
            }*/

        }
        target.GetComponent<Actor>().TakeDamage(CalcMagicDamage(damage,target),gameObject);
        DwellTime.Attack(dwell_time);

    }

    /*
    public override bool UseSkill(GameObject target)
    {
        if(!base.UseSkill(target))
        {
            return false;
        }
       
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            Actor targeta = target.GetComponent<Actor>();
            if (anim != null)
            {
                rotateAtObj(target);
                anim.SetTrigger("MagicAttack");

                //animate fire attack
                if (effect != null)
                    Projectile(effect, target);

                actor.PlaySound("attack");
            }
            targeta.TakeDamage(damage,gameObject);
            if (Ability.DiceRoll(actor.getDexterity(), targeta.getDexterity()))
            {
                StatusEffectsController.AddEffect(new Burn(actor.getDexterity() / 3, actor, targeta, target.tag == "Enemy"));
                Debug.Log("Burn Initiated. Stop Drop and Roll Bitch");
            }

            DwellTime.Attack(dwell_time);
            return true;
        }

        return false;
    }*/
}
