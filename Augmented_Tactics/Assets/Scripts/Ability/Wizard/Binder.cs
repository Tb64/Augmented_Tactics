using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binder : Ability {

    private bool item;
    private GameObject effect1; // need to add some sort of effect. red glare etc
    public Binder(GameObject obj, bool item)
    {
        Initialize(obj);
        this.item = item;
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = gameObject.GetComponent<Actor>(), actor = target.GetComponent<Actor>();
        Actor[] bond = {actor, targeta  };
        StatusEffectsController.bonded.Add(bond);
        actor.bonded = true;
        targeta.bonded = true;
        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 5;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Destiny Binder";
        if (item)
            manaCost = 20;
        else
            manaCost = 30;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
