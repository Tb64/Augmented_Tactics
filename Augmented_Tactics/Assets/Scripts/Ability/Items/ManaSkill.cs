using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSkill : Ability {

    private bool lotto;

    public ManaSkill(GameObject obj, int level, bool lotto) //for small/medium/large just use level and adjust accordingly for quick items
    {
        Initialize(obj);
        this.lotto = lotto;
        if (lotto)
            heal = Random.Range(1, 10);
        else if (level == 3)
            heal = 50f;
        else if (level == 2)
            heal = 25f;
        else
            heal = 10f;
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (!base.UseSkill(target))
        {
            return;
        }
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            targeta.setManaCurrent(targeta.getManaCurrent() + heal);
            if (lotto)
            {
                Debug.Log(targeta + "Recovered Random Amount " + heal + " of Mana");
                StatusEffectsController.AddEffect(new RandomHeal(0, gameObject.GetComponent<Actor>(), targeta, target.tag == "Enemy"));
            }
            else
            {
                Debug.Log(targeta + "Recovered " + heal + " Mana");
            }
            DwellTime.Attack(dwell_time);
        }
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 2;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "ManaTonic";
        manaCost = 0;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
