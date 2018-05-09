using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneComet : AOE {

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
        AOESizeMin = 0;
        AOESizeMax = 2;
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 2f;
        abilityName = "arcanecomet";
        range_max = 5;
        range_min = 0;
        damage = 30 + actor.getIntelligence() * 2 + actor.getWeapon().RollMagicDamage();
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/violet/violet_07");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 50;
        cometEffect = Resources.Load<GameObject>("Effects/Effect7_Optimized");
    }

    public override void ActionSkill(GameObject target)
    {

        rotateAtObj(target);
        if (anim != null)
        {
            anim.SetTrigger("Missle");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        if (cometEffect != null)
            worldEffect = GameObject.Instantiate(cometEffect, target.GetComponent<ClickableTile>().getCoords()
            + new Vector3(0, .5f, 0), Quaternion.identity);

        DwellTime.Attack(dwell_time);
        for (int i = 0; i < listIterActor; i++)
        {
            if (listOfActorsAffected[i] != null)
                listOfActorsAffected[i].TakeDamage(damage, gameObject);
        }

    }
}
