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
        abilityName = "eviscerate";
        range_max = 1;
        range_min = 0;
        damage = 50 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassinSkill10");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 0;
    }



    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Eviscerate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        GameObject effect = GameObject.Instantiate(fireBall, gameObject.transform);


        DwellTime.Attack(dwell_time);
    }

}