using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLeech : Ability
{
   
    Actor user;
    GameObject leech = Resources.Load<GameObject>("animation/Effect8_Optimized");
    private MonoBehaviour mB;

    public LifeLeech(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 2.0f;
        abilityName = "lifeleech";
        range_max = 4;
        range_min = 0;
        damage = 10 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassinSkill10");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 0;
    }

    void leechAnim(GameObject target)
    {
        GameObject effect = GameObject.Instantiate(leech, target.GetComponent<Actor>().getCoords() + new Vector3(0,.5f,0), Quaternion.identity );
        Transform coreDistance = effect.transform.Find("Core_Distance");
        Transform wavesDistance = coreDistance.transform.Find("Waves_Distance");
        ParticleSystem pS = wavesDistance.GetComponent<ParticleSystem>();
       
        float distance = Vector3.Distance(target.GetComponent<Actor>().getCoords(), gameObject.transform.position);
        
        coreDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);
        wavesDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);




        effect.transform.LookAt(gameObject.transform.position + new Vector3(0, .5f, 0));
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

    public void StartCoroutine(GameObject target)
    {
        if (mB != null)
        {
            mB.StartCoroutine(healTakeDamage(target));
        }
    }

    IEnumerator healTakeDamage(GameObject target)
    {
        leechAnim(target);
        yield return new WaitForSeconds(1);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        user.HealHealth(damage);
        yield return new WaitForSeconds(1);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        user.HealHealth(damage);
        yield return new WaitForSeconds(1);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        user.HealHealth(damage);
    }

    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            //anim.SetTrigger("MeleeAttack");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        
        StartCoroutine(target);
               
    }

}