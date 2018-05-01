using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class Disintegrate : Ability {

    static public Disintegrate instance;

    //float damage = 10f;
    //StateMachine SM = GameObject.Find("GameController").GetComponent<StateMachine>();
    GameObject bloodEffect = Resources.Load<GameObject>("animation/effect26");
    TileMap map;
    Actor user;
    private MonoBehaviour mB;
    private Enemy position;

    public Disintegrate(GameObject obj)
    {
        Initialize(obj);
        user = obj.GetComponent<Actor>();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        map = actor.map;
        anim = gameObject.GetComponentInChildren<Animator>();
        dwell_time = 1.0f;
        abilityName = "disintegrate";
        range_max = 4;
        range_min = 0;
        damage = 10 + actor.getStrength() * 2;
        abilityImage = Resources.Load<Sprite>("UI/Ability/warriorSkill3");
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        position = GameObject.FindObjectOfType<Enemy>();

        if (abilityImage == null)
            Debug.Log("Unable to load image");

        manaCost = 10;
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
            mB.StartCoroutine(disintegrateAnim(target));
        }
        else
        {
            Debug.Log("mB doesnt exist");
        }
    }

    IEnumerator disintegrateAnim(GameObject target)
    {
        yield return new WaitForSeconds(1f);
        anim.gameObject.SetActive(true);
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        Debug.Log("Starting disintegrate");
        
        GameObject.Instantiate(bloodEffect, gameObject.transform);
        if (anim != null)
        {
            rotateAtObj(target);
            anim.Play("Disintegrate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        target.GetComponent<Actor>().TakeDamage(damage, gameObject);
    }

    private void Skill(GameObject target)
    {
        Vector3 currentLoc = gameObject.GetComponent<Transform>().position;
        GameObject effect = GameObject.Instantiate(bloodEffect, actor.getCoords(), Quaternion.identity);

        gameObject.GetComponent<Transform>().position = Enemy.PosCloseTo(position,target.GetComponent<Actor>().getCoords(),map);
        map.SetOcc(gameObject, currentLoc, Enemy.PosCloseTo(position,target.GetComponent<Actor>().getCoords(), map));
        
        StartCoroutine(target);
        anim.gameObject.SetActive(false);
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;

        gameObject.GetComponent<Actor>().setCoords(Enemy.PosCloseTo(position,target.GetComponent<Actor>().getCoords(),map));
        
        DwellTime.Attack(dwell_time);
    }
}
