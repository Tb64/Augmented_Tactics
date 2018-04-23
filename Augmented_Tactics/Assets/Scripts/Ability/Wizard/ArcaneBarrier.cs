using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBarrier : Ability {

    private MonoBehaviour mB;
    StateMachine SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect13");
    Actor user;

    public ArcaneBarrier(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        damage = 0f;
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 1;
        dwell_time = 1.0f;
        abilityName = "Arcane Barrier";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/blue/blue_20");
        actor.UseMana(actor.getManaCurrent());
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());

        abilityDescription = "Use a percentage of your available mana to shield all allies hit. Cost is a percentage that depends on the level of the user.\nMana: " + manaPercent + "%";

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
        //GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
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
        //StatusEffectsController.AddEffect(new Bleed(10, actor, target.GetComponent<Actor>(), target.tag == "Enemy"));
        //Will apply bleed(damager per turn, 2 turns)
        //Will remove 1 move from enemies next 2 turns


        DwellTime.Attack(dwell_time);
    }
}
