using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAttack : Ability {

    public float damage = 10f;
    
    /// <summary>
    /// Constructor for Basic Attack, put in the gameobject of the object that basic attack will be added to.
    /// </summary>
    /// <param name="obj">The object that will be using the ability</param>
    public BasicAttack(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = parent.GetComponentInChildren<Animator>();
        range_max = 1;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Basic Attack";
        abilityImage = Resources.Load <Sprite>("UI/Ability/warriorSkill3") ;
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        //Debug.Log("Adding " + abilityName + " to " + parent.name);
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
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, parent.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MeleeAttack");
            //justin set attack string array choice hereS
            parent.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage);

        DwellTime.Attack(dwell_time);
    }
}
