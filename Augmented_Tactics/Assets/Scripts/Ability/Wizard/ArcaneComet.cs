﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneComet : Ability {

    GameObject cometEffect;
    GameObject worldEffect;

    public ArcaneComet(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        //isAOE = true;
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.5f;
        abilityName = "arcanecomet";
        range_max = 5;
        range_min = 1;
        damage = 20 + actor.getIntelligence();
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 50;
        cometEffect = Resources.Load<GameObject>("animation/MageComet");
    }

    public override void ActionSkill(GameObject target)
    {

        rotateAtObj(target);
        if (anim != null)
        {
            anim.SetTrigger("Missle");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        Debug.Log("Coords I have: " + target.GetComponent<Actor>().getCoords());

        if (cometEffect != null)
            worldEffect = GameObject.Instantiate(cometEffect, target.GetComponent<Actor>().getCoords()
            + new Vector3(0, .5f, 0), Quaternion.identity);

        DwellTime.Attack(dwell_time);

    }
}
