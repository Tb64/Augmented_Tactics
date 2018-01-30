using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disintegrate : Ability {

    float damage = 10f;
    StateMachine SM = GameObject.Find("GameController").GetComponent<StateMachine>();
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect26");
    Actor user;

    public Disintegrate(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "disintegrate";
        range_max = 3;
        range_min = 0;
        damage = 10 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/warriorSkill3");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 0;
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
            
            gameObject.GetComponent<Transform>().position = (target.GetComponent<Actor>().getCoords() - new Vector3(0,0,1));
            gameObject.GetComponent<Actor>().setCoords(target.GetComponent<Actor>().getCoords() - new Vector3(0, 0, 1));
            rotateAtObj(target);
            anim.SetTrigger("MeleeAttack");
            GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        //decide if status effect is successful
        //StatusEffect status = new StatusEffect(2, (float)typeof(Actor).GetField("health_current").GetValue(user), "Bleeding", 5, "-", target.GetComponent<Actor>(),true, SM);
        //Need to add status effect
        //Will apply bleed(damager per turn, 2 turns)
        //Will remove 1 move from enemies next 2 turns

        DwellTime.Attack(dwell_time);
    }
}
