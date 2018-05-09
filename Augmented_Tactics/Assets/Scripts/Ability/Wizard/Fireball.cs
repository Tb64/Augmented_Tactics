using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ability
{
    GameObject fireBall = Resources.Load<GameObject>("animation/effect15");
    Actor user;

    public FireBall(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "fireball";
        range_max = 7;
        range_min = 0;
        damage = actor.getIntelligence() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassinSkill10");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 0;
    }


    void calculateDistance(GameObject target)
    {
        float distance = Vector3.Distance(target.GetComponent<Actor>().getCoords(), gameObject.transform.position);

    }


    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Eviscerate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        GameObject effect = GameObject.Instantiate(fireBall, user.transform);
        
        //effect.transform.LookAt(target.transform.position);
        float distance = Vector3.Distance(target.GetComponent<Actor>().getCoords(), gameObject.transform.position);
        distance = (int)distance;
        //gameObject.GetComponentInChildren<RFX4_TransformMotion>().Distance = distance;
        DwellTime.Attack(dwell_time);
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

}