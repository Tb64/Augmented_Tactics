using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholyLightning : Ability {

    float damage = 10f;
    StateMachine SM = GameObject.Find("GameController").GetComponent<StateMachine>();
    GameObject unholyLightning = Resources.Load<GameObject>("animation/effect26");
    Actor user;

    public UnholyLightning(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "UnholyLightning";
        range_max = 1;
        range_min = 0;
        damage = actor.getStrength() * 2;

        manaCost = 0;
    }
}
