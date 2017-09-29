using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int range;
    public Animator anim;
    public string abilityName;

    public virtual void Initialize()
    {

    }

    public virtual void UseSkill(GameObject target)
    {

    }

    public bool SkillInRange(Vector3 start, Vector3 end)
    {
        return (Vector3.Distance(start, end) < (float)range);
    }
}
