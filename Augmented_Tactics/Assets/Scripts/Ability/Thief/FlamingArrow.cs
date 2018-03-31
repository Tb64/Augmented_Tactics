using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingArrow : Ability {

    private string animTrigger = "CastAttack2Trigger";
    private GameObject handVFX; //need a shoot arrow effect

    public FlamingArrow(GameObject obj)
    {
        Initialize(obj);
    }
    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (handVFX != null)
                GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform());
            else
                Debug.Log("handVFX null");
            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().HealHealth(heal);

        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 10;
        range_max = 15;
        range_min = 0;
        damage = 10f + actor.getDexterity() * 2;
        dwell_time = 1.0f;
        abilityName = "Flaming Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill8");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
