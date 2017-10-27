using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability 
{
    public int range;
    public int range_min;
    public Animator anim;
    public string abilityName;

    protected GameObject parent;
    public Sprite abilityImage;

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

    public bool SkillInRange(GameObject startObj, GameObject endObj)
    {
        Vector3 start = startObj.GetComponent<Actor>().getCoords();
        Vector3 end = endObj.GetComponent<Actor>().getCoords();

        //Debug.Log("Skill:Find Range " + start + end);
        return (Vector3.Distance(start, end) <= (float)range);
    }
}