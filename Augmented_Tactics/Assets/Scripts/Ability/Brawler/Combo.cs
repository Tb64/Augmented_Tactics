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
        abilityImage = Resources.Load<Sprite>("UI/Ability/warrior/warriorSkill2");
        handVFX = Resources.Load<GameObject>("Effects/HandEffects/Effect4_Hand_Optimized");
        if (abilityImage == null)
            Debug.Log("Unable to load image");

        damage = 10f + ((float)actor.getStrength() * 0.5f);
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
                GameObject.Instantiate<GameObject>(handVFX, actor.LeftHandTransform());
            }

            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        Debug.Log("combo damage = " + damage + " " + actor.getStrength());
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
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
