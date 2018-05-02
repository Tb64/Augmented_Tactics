using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : Ability {

    private MonoBehaviour mB;
    StateMachine SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    GameObject missleEffect = Resources.Load<GameObject>("animation/effect1");
    Actor user;

    public MagicMissle(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 3.5f;
        abilityName = "magicmissle";
        range_max = 5;
        range_min = 0;
        damage = 10 + actor.getMaxMana() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill2.png");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
        manaCost = 10;
    }



    void magicmissleAnim(GameObject target)
    {

        GameObject effect = GameObject.Instantiate(missleEffect, target.GetComponent<Actor>().getCoords()
            + new Vector3(0, .5f, 0), Quaternion.identity);
        /*
        Transform coreDistance = effect.transform.Find("Core_Distance");
        Transform wavesDistance = coreDistance.transform.Find("Waves_Distance");

        //calculates distance between player and target, then multiplies x coord to scale particles correctly
       
        coreDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);
        wavesDistance.transform.localScale = new Vector3(.1f * distance, 1, 1);*/

        float distance = Vector3.Distance(target.GetComponent<Actor>().getCoords(), gameObject.transform.position);
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
            mB.StartCoroutine(animDelay(target));
        }
    }

        IEnumerator animDelay(GameObject target)
    {
        yield return new WaitForSeconds(.8f);
        //GameObject effect = GameObject.Instantiate(bloodEffect, target.transform);
        //target.GetComponent<Actor>().TakeDamage(damage, gameObject);
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
    }

    private void Skill(GameObject target)
    {
        if (anim != null)
        {
            rotateAtObj(target);
            //anim.SetTrigger("MeleeAttack");

            //anim.SetTrigger("Missle");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        StartCoroutine(target);
        DwellTime.Attack(dwell_time);
    }
}
