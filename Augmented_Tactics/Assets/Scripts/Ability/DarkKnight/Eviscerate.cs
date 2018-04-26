using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Eviscerate : Ability
{

    private MonoBehaviour mB;
    StateMachine SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect26");
    Actor user;
    float BASE_DAMAGE = 10f;
    float STR_SCALER = 0.5f;


    public Eviscerate(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "eviscerate";
        manaCost = 10;
        range_max = 1;
        range_min = 0;
        damage = BASE_DAMAGE * actor.getLevel() + STR_SCALER * actor.getStrength();
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        abilityDescription = "A devestating stab that will leave an enemy bleeding for two turns";
        
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
    public void StartCoroutine(GameObject target)
    {
        if (mB != null)
        {
            mB.StartCoroutine(animDelay(target));
        }
    }

    IEnumerator animDelay(GameObject target)
    {
        yield return new WaitForSeconds(.8f);
        GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
    }


    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Eviscerate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        StartCoroutine(target);

        //decide if status effect is successful
        //StatusEffect status = new StatusEffect(2, (float)typeof(Actor).GetField("health_current").GetValue(user), "Bleeding", 5, "-", target.GetComponent<Actor>(),true, SM);
        //Need to add status effect
        StatusEffectsController.AddEffect(new Bleed((damage/2), actor, target.GetComponent<Actor>(), target.tag == "Enemy"));
        //Will apply bleed(damager per turn, 2 turns)
        //Will remove 1 move from enemies next 2 turns

        DwellTime.Attack(dwell_time);
    }

}