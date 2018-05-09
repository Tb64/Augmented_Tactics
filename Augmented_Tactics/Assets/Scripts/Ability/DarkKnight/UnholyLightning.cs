using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnholyLightning : Ability {

    float damage = 10f;
    StateMachine SM = GameObject.Find("GameController").GetComponent<StateMachine>();
    GameObject unholyLightning = Resources.Load<GameObject>("animation/effect25");
    Actor user;

    RFX4_ParticleTrail beam1;
    RFX4_ParticleTrail beam2;
    RFX4_ParticleTrail beam3;

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
        manaCost = 40;
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/gray/gray_15");
        beam1 = unholyLightning.transform.Find("Trail1").GetComponent<RFX4_ParticleTrail>();
        beam2 = unholyLightning.transform.Find("Trail2").GetComponent<RFX4_ParticleTrail>();
        beam3 = unholyLightning.transform.Find("Trail3").GetComponent<RFX4_ParticleTrail>();
    }

    private void unholyAnim()
    {
        GameObject effect = GameObject.Instantiate(unholyLightning, actor.getCoords() + new Vector3(0,4,0), Quaternion.identity);

        int totalEnemies = EnemyController.enemyNum;

        //if less than 3 enemies only 2 beams will be active
        if (totalEnemies < 3)
            beam3.transform.parent.gameObject.SetActive(false);
        else
            beam3.transform.parent.gameObject.SetActive(true);
        //if less than 2 enemies, one 1 beam will be active
        if (totalEnemies < 2)
            beam2.transform.parent.gameObject.SetActive(false);
        else
            beam2.transform.parent.gameObject.SetActive(true);

        int chooseEnemy = Random.Range(0, totalEnemies);

        float distance = Vector3.Distance(EnemyController.enemyList[chooseEnemy].getCoords(), gameObject.transform.position);
        if (distance <= range_max)
        {
            beam1.Target = EnemyController.enemyList[chooseEnemy].transform.parent.gameObject;
        }

        beam2.Target = EnemyController.enemyList[1].transform.parent.gameObject.gameObject;
        //GameObject collision1 = GameObject.Find("Trail1").transform.Find("effect25_collision(clone)");
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
        DwellTime.Attack(dwell_time);

    }

}
