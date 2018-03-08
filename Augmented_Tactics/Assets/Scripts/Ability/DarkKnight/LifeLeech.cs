using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLeech : Ability
{
   
    Actor user;
    GameObject leech = Resources.Load<GameObject>("animation/Effect8_Optimized");
    private MonoBehaviour mB;
    //private PositionAllignment reposition;

    public LifeLeech(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        //reposition = GameObject.FindObjectOfType<PositionAllignment>();
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 3.5f;
        abilityName = "lifeleech";
        range_max = 5;
        range_min = 0;
        damage = 10 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill5");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 20;
    }

    void leechAnim(GameObject target)
    {

        GameObject effect = GameObject.Instantiate(leech, target.GetComponent<Actor>().getCoords() 
            + new Vector3(0,.5f,0), Quaternion.identity );
        Transform coreDistance = effect.transform.Find("Core_Distance");
        Transform wavesDistance = coreDistance.transform.Find("Waves_Distance");
       
        //calculates distance between player and target, then multiplies x coord to scale particles correctly
        float distance = Vector3.Distance(target.GetComponent<Actor>().getCoords(), gameObject.transform.position);
        coreDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);
        wavesDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);
       
        //Turns the animation towards the player
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
        //will tick for 3 seconds, doing damage and healing each tick

        leechAnim(target);


        for(int index = 0; index < 3; index++)
        {
            yield return new WaitForSeconds(1);
            target.GetComponent<Actor>().TakeDamage(damage, gameObject);
            user.HealHealth(damage);
        }
       
    }

    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            //anim.SetTrigger("MeleeAttack");
            
            anim.Play("Leech");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        
        StartCoroutine(target);
        DwellTime.Attack(dwell_time);
    }

}