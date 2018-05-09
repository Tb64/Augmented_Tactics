using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFury : Ability {

    
    private MonoBehaviour mB;
    //StateMachine SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    //Damages Enemy and removes one action point from enemy
    //need to add status effect that removes one turn from enemy
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect26");
    private GameObject handVFX = Resources.Load<GameObject>("animation/Effect4_Hand");
    Actor user;
    float BASE_DAMAGE = 5f;
    float STR_SCALER = 0.5f;
    float mana_drain = 10f;

    public BladeFury(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.5f;
        abilityName = "bladefury";
        manaCost = 20;
        range_max = 1;
        range_min = 0;
        damage = BASE_DAMAGE * actor.getLevel() + STR_SCALER * actor.getStrength();
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/red/red_36");
        
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        abilityDescription = "The Dark Knight enters a blood rage, slashing the enemy multiple times";

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
        float totalDamage = damage + actor.getWeapon().RollPhysicalDamage() - target.GetComponent<Actor>().getPhysicalDefense();
        GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform());
        yield return new WaitForSeconds(.5f);
        GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
        target.GetComponent<Actor>().TakeDamage(totalDamage, gameObject);
        yield return new WaitForSeconds(.5f);
        effect = GameObject.Instantiate(bloodEffect, target.transform);
        target.GetComponent<Actor>().TakeDamage(totalDamage, gameObject);
        yield return new WaitForSeconds(.5f);
        effect = GameObject.Instantiate(bloodEffect, target.transform);
        target.GetComponent<Actor>().TakeDamage(totalDamage, gameObject);
    }


    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("BladeFury");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
       
        StartCoroutine(target);
        user.setManaCurrent(user.getManaCurrent() + mana_drain);
        target.GetComponent<Actor>().setManaCurrent(target.GetComponent<Actor>().getManaCurrent() - mana_drain);

        DwellTime.Attack(dwell_time);
    }

}
