using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability
{
    public int range_max;
    public int range_min;
    public float dwell_time;
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
        //return false;

    }

    /// <summary>
    /// Use this method if you need to check Skill is sucessful (in range or valid target).
    /// </summary>
    /// <param name="target"></param>
    /// <param name="isSuccessful"></param>
    public virtual void UseSkill(GameObject target, out bool isSuccessful)
    {
        isSuccessful =  false;
    }

    public virtual void UseSkillAsync(GameObject target, out bool isSuccessful)
    {
        isSuccessful = false;
    }

    public bool SkillInRange(Vector3 start, Vector3 end)
    {
        float distance = Vector3.Distance(start, end);
        return (distance <= (float)range_max && distance >= (float)range_min );
    }

    public bool SkillInRange(GameObject startObj, GameObject endObj)
    {
        Vector3 start = startObj.GetComponent<Actor>().getCoords();
        Vector3 end = endObj.GetComponent<Actor>().getCoords();

        return SkillInRange(start, end);
    }

    protected void rotateAtObj(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(parent.transform.forward, target.transform.position, 1f, 0f);
        newDir = new Vector3(newDir.x, parent.transform.position.y, newDir.z);

        newDir = new Vector3(target.transform.position.x, parent.transform.position.y, target.transform.position.z);
        parent.transform.LookAt(newDir);
    }

    protected void DwellTimeThread()
    {
        int sleepMS = (int)this.dwell_time * 1000;
        Thread.Sleep(sleepMS);
        //yield return new WaitForSeconds(this.dwell_time);

        Debug.Log("Attack dwell finished");
        TurnBehaviour.ActorHasAttacked();
    }

    protected void DwellTime()
    {
        Thread dwellThread = new Thread(this.DwellTimeThread);
        dwellThread.Start();
        //StartCoroutine(DwellTimeThread());
    }
}