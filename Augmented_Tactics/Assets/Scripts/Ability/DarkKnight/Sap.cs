using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : Ability
{

    private MonoBehaviour mB;
    //StateMachine SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy
    GameObject sapEffect = Resources.Load<GameObject>("animation/Effect1_Collision");
    Actor user;
    float BASE_DAMAGE = 10f;
    float STR_SCALER = 0.5f;
    float mana_drain = 10f;

    public Sap(GameObject obj)
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
        abilityName = "sap";
        manaCost = 0;
        range_max = 1;
        range_min = 0;
        damage = BASE_DAMAGE * actor.getLevel() + STR_SCALER * actor.getStrength();
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/violet/violet_28");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        abilityDescription = "Slash the enemy, draining their mana";
        
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
        yield return new WaitForSeconds(.2f);
        GameObject effect = GameObject.Instantiate(sapEffect, target.transform);
        target.GetComponent<Actor>().TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
    }


    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Sap");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        StartCoroutine(target);
        user.setManaCurrent(user.getManaCurrent() + mana_drain);
        target.GetComponent<Actor>().setManaCurrent(target.GetComponent<Actor>().getManaCurrent() - mana_drain);

        DwellTime.Attack(dwell_time);
    }
}