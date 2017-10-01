using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int range;
    public Animator anim;
    public string abilityName;

    protected GameObject parent;

    public virtual void Initialize(GameObject obj)
    {
        parent = obj;
    }

    public virtual void UseSkill(GameObject target)
    {

    }

    public bool SkillInRange(Vector3 start, Vector3 end)
    {
        return (Vector3.Distance(start, end) < (float)range);
    }
}
