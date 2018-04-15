using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureStatus: Ability
{
    private bool cureAll;
    public CureStatus(GameObject obj, int level)
	{
        Initialize(obj);
        if (level == 1)
            cureAll = true;
        else
            cureAll = false;
	}

    public override void ActionSkill(GameObject target)
    {
        //options here: Panacea will always remove every statuseffect
        //Unguent will remove 1. We can give the player a chance to select or do it at random
        Actor actor = target.GetComponent<Actor>();
        List<StatusEffects> status = StatusEffectsController.GetEffects(actor);
        if (status == null)
        {
            Debug.Log("No Status To Cure");
            return;
        }
        if (cureAll)
        {
            foreach (StatusEffects stat in StatusEffectsController.allEffects)
                if (actor == stat.effectedPlayer && stat.effectorPlayer.tag != stat.effectedPlayer.tag)
                {
                    Debug.Log("Removing status " + stat + " from " + actor);
                    StatusEffectsController.RemoveEffect(stat);
                }
            Debug.Log("Removed all status effects from " + actor);
        }
        else
        {
            //for now randomly selecting which status to clear
            if (status.Count == 1)
                status.Remove(status[0]);
            else
            {
                int remove = Random.Range(0, status.Count - 1);
                Debug.Log("Removing status " + status[remove] + " from " + actor);
                status.Remove(status[remove]);
            }
        }
        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 2;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Cure Status";
        manaCost = 0;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
