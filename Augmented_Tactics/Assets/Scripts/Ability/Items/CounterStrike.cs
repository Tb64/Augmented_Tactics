using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterStrike : Ability {

    private GameObject effect1; //add red glow or something

    public CounterStrike(GameObject obj)
    {
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        targeta.counter = true;
        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 10000;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Counter Strike";
        manaCost = 10;
        abilityImage = Resources.Load<Sprite>("UI/Ability/warrior/warriorSkill2");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
