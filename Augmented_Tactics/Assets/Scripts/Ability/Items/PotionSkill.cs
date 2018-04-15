using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSkill : Ability {

    //public float heal;
    public bool lotto;

    public PotionSkill(GameObject obj, int level, bool lotto)
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

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Potion";
        manaCost = 0;
        //heal = 10f;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill5");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        //Debug.Log("Adding " + abilityName + " to " + parent.name);
    }

    public override bool UseSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (!base.UseSkill(target))
        {
            return false;
        }
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            targeta.HealHealth(heal);
            if (lotto)
            {
                Debug.Log(targeta + "Recovered Random Amount " + heal + " of Health");
                StatusEffectsController.AddEffect(new RandomHeal(0, gameObject.GetComponent<Actor>(), targeta, target.tag == "Enemy"));
            }
            else
            {
                Debug.Log(targeta + "Recovered " + heal + " Health");
            }
            DwellTime.Attack(dwell_time);
            return true;
        }
        return false;
    }


}
