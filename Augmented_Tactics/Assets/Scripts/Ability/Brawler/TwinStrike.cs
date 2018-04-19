using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStrike : Ability
{

    string[] animTrigger = { "Attack5Trigger", "Attack3Trigger", "Attack4Trigger" };
    GameObject handVFX;

    float manaGiven;

    public TwinStrike(GameObject obj)
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
        manaGiven = actor.getMaxMana() / actor.getLevel();
        damage = actor.getLevel() + actor.getStrength();
        abilityName = "Twin Strikes";
        abilityImage = Resources.Load<Sprite>("UI/Ability/archer/archerSkill1");

        actor.UseMana(actor.getManaCurrent());

        if (abilityImage == null)
            Debug.Log("Unable to load image");

        abilityDescription = "The basic left and right jab of the brawler used to generate mana to use other brawler skills.  Brawler's mana usage and generation is based on level. \nMana Generated: " + (int)manaGiven;
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
            
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        actor.GiveMana(manaGiven);
        float totalDamage = damage + actor.getWeapon().RollPhysicalDamage() - target.GetComponent<Actor>().getArmor().physical_def;
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


        for (int index = 0; index < 2; index++)
        {
            Action(target, this.animTrigger[index]);
            yield return new WaitForSeconds(1);
        }

    }
}
