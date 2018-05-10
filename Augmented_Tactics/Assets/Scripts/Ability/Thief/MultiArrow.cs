using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiArrow : Ability {

    private string animTrigger = "Arrow";
    private GameObject effect1 = Resources.Load<GameObject>("Effects/ArrowShot");

    public MultiArrow(GameObject obj)
    {
        Initialize(obj);
    }

    public override void ActionSkill(GameObject[] targets)
    {
        Actor attacker = gameObject.GetComponent<Actor>();
        if (anim != null)
        {
            anim.SetTrigger(animTrigger);
            anim.SetInteger("Weapon", 7);
            gameObject.GetComponent<Actor>().PlaySound("attack");
            foreach (GameObject target in targets)
            {
                Debug.Log(attacker + "Bulls Eyed " + target);
                if (effect1 != null)
                {
                    Projectile(effect1, target);
                }
                Debug.Log("effect1 null");
                target.GetComponent<Actor>().TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
            }
        }
        DwellTime.Attack(dwell_time);
        
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1f;
        manaCost = 15;
        range_max = 5;
        range_min = 0;
        damage = 10f + obj.GetComponent<Actor>().getDexterity();
        abilityName = "Multi Arrow";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/green/green_05");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
