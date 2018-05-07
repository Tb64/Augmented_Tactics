using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucidity : Ability
{

    GameObject lucidityEffect;
    GameObject worldEffect;
    Actor user;

    public Lucidity(GameObject obj)
    {
        user = obj.GetComponent<Actor>();
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        manaRestore = true;
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1f;
        abilityName = "lucidity";
        range_max = 0;
        range_min = 0;
        damage = 0;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 0;
        lucidityEffect = Resources.Load<GameObject>("animation/MageLucidity2");
    }

    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            anim.SetTrigger("Lucidity");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        user.GiveMana(20, user);

        //need to delete it afterwards
        if(lucidityEffect!=null)
            worldEffect = GameObject.Instantiate(lucidityEffect, target.GetComponent<Actor>().getCoords()
            + new Vector3(0, .5f, 0), Quaternion.identity);

        DwellTime.Attack(dwell_time);
    }
}
