using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWord : Ability {

    private string animTrigger = "CastAttack1Trigger";
    private GameObject handVFX;

    public HealingWord(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "HealingWord";
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect13_Hand_Optimized");
        abilityImage = Resources.Load<Sprite>("UI/Ability/priest/priestSkill1");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
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
        float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        target.GetComponent<Actor>().HealHealth(heal);

        DwellTime.Attack(dwell_time);
    }
}
