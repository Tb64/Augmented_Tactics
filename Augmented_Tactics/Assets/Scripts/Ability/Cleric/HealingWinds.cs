using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWinds : AOE {
    private string animTrigger = "Attack4Trigger";
    const string SpiriteDir = "UI/Skill_Icon_Pack/green/green_11";
    private GameObject effect;
    private HealOverTime statuseffect;

    public HealingWinds(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        canHeal = true;
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        AOESizeMin = 0;
        AOESizeMax = 1;
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        manaCost = actor.getWisdom() * 3;
        damage = actor.getConstitution() + actor.getStrength();
        //heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Healing Winds";
        effect = Resources.Load<GameObject>("Effects/Effect21_Optimized");
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "Area of Effect health regeneration.";


    }

    public override void ActionSkill(GameObject target)
    {
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MagicCast");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        for (int i = 0; i < listIterActor; i++)
        {
            if (listOfActorsAffected[i] != null)
            {
                statuseffect = new HealOverTime(heal, actor, listOfActorsAffected[i].GetComponent<Actor>(), false);
                if (effect != null)
                    GameObject.Instantiate<GameObject>(effect, listOfActorsAffected[i].transform);
                else
                    Debug.Log("effect null");
            }
        }

        DwellTime.Attack(dwell_time);
    }
}
