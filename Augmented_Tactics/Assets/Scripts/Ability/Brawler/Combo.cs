using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : Ability {

    string[] animTrigger = { "Attack5Trigger", "Attack3Trigger", "Attack4Trigger" };
    GameObject handVFX;

    public Combo(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 3.0f;
        abilityName = "Combo";
        manaCost = actor.getMaxMana() / actor.getLevel();
        manaCost = manaCost * 2;
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/blue/blue_01");
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect4_Hand_Optimized");
        actor.UseMana(actor.getManaCurrent());
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        damage = 5f + ((float)actor.getStrength());
        int manaPercent = (int)((manaCost * 100f) / actor.getMaxMana());
        abilityDescription = "A three hit combo. Cost is a percentage that depends on the level of the brawler. \nMana: " + manaPercent + "%";
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
        StartCoroutine(target);
        DwellTime.Attack(dwell_time);
    }

    private void Action(GameObject target, string animName)
    {
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger(animName);
            if (handVFX != null)
            {
                GameObject.Instantiate<GameObject>(handVFX, actor.RightHandTransform());
                GameObject.Destroy( GameObject.Instantiate<GameObject>(handVFX, actor.LeftHandTransform()), dwell_time);
            }

            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        float totalDamage = damage + actor.getWeapon().RollPhysicalDamage() - target.GetComponent<Actor>().getPhysicalDefense();
        Debug.Log("combo damage = " + totalDamage + " " + actor.getStrength());
        target.GetComponent<Actor>().TakeDamage(totalDamage, gameObject);
    }

    private void StartCoroutine(GameObject target)
    {
        if (monoBehaviour != null)
        {
            monoBehaviour.StartCoroutine(Thread(target));
        }
    }

    IEnumerator Thread(GameObject target)
    {
        //will tick for 3 seconds, doing damage and healing each tick


        for (int index = 0; index < 3; index++)
        {
            Action(target, this.animTrigger[index]);
            yield return new WaitForSeconds(1);
        }

    }
}
