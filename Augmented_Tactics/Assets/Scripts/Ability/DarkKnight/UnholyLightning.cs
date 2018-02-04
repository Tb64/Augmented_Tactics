using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholyLightning : Ability {

    float damage = 10f;
    StateMachine SM = GameObject.Find("GameController").GetComponent<StateMachine>();
    GameObject unholyLightning = Resources.Load<GameObject>("animation/effect25");
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
        range_max = 4;
        range_min = 0;
        damage = actor.getStrength() * 2;

        manaCost = 0;
    }

    private void unholyAnim()
    {
        GameObject effect = GameObject.Instantiate(unholyLightning, actor.getCoords() + new Vector3(0,2,0), Quaternion.identity);

        
        unholyLightning.transform.Find("Trail1").GetComponent<RFX4_ParticleTrail>().Target = EnemyController.enemyList[0].gameObject;
        unholyLightning.transform.Find("Trail2").GetComponent<RFX4_ParticleTrail>().Target = EnemyController.enemyList[1].gameObject;
    }

    public override bool UseSkill(GameObject target)
    {
        if (base.UseSkill(target))
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
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Eviscerate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        unholyAnim();
        
    }

}
