using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {

    public float damage = 10f;

    

    public BasicAttack(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        Debug.Log(parent.name);
        anim = parent.GetComponentInChildren<Animator>();
        range = 1;
        abilityName = "Basic Attack";
    }

    public override void UseSkill(GameObject target)
    {
		if(target == null)
		{
			Debug.Log("Target is null.");
			return;
		}

        if(target.tag == "Player" || target.tag == "Enemy")
        {
            if (SkillInRange(parent, target) == false)
            {
                Debug.Log("Ability out of range. Range is " + range);
                return;
            }
            if (anim != null)
            {
                rotateAtObj(target);
                anim.SetTrigger("MeleeAttack");
            }
            target.GetComponent<Actor>().TakeDamage(damage);
        }
        else
        {
            Debug.Log("Target is not an Actor");
        }
    }

    private void rotateAtObj(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(parent.transform.forward, target.transform.position, 1f, 0f);
        newDir = new Vector3(newDir.x, parent.transform.position.y, newDir.z);


        newDir = new Vector3(target.transform.position.x, parent.transform.position.y, target.transform.position.z);
        parent.transform.LookAt(newDir);
    }
}
