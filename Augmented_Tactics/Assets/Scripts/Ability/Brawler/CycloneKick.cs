using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneKick : Ability {

    float BASE_DAMAGE = 10f;
    float DEX_SCALER = 0.5f;
    float STR_SCALER = 0.5f;

    float damage;

    Actor targetActor;

    string animTrigger = "";

    public CycloneKick(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Cyclone Kick";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");

        float damage =
            BASE_DAMAGE * actor.getLevel() +
            DEX_SCALER * actor.getDexterity() +
            STR_SCALER * actor.getStrength();

        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    public override bool UseSkill(GameObject target)
    {
        if(base.UseSkill(target))
        {
            Skill(target);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Skill(GameObject target)
    {

        targetActor = target.GetComponent<Actor>();

        Vector3 location = new Vector3();
        actor.map.getTileAtCoord(location);
    }

    private void SkillOnLocation(GameObject target)
    {

    }
}
