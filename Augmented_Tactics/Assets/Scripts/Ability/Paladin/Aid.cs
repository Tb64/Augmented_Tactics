using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aid : Ability
{
    private string animTrigger = "CastAttack2Trigger";
    const string SpiriteDir = "UI/Ability/priest/priestSkill1";
    GameObject teleportEffect = Resources.Load<GameObject>("animation/Effect6_Collision");
    GameObject healEffect = Resources.Load<GameObject>("animation/Effect8Paladin");
    TileMap map;
    private Enemy position;
    private MonoBehaviour mB;

    public Aid(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        mB = GameObject.FindObjectOfType<MonoBehaviour>();
        map = actor.map;
        anim = gameObject.GetComponentInChildren<Animator>();
        position = GameObject.FindObjectOfType<Enemy>();
        range_max = 4;
        range_min = 0;
        dwell_time = 3.0f;
        heal = 5f + (float)actor.getWisdom() * 1.25f;
        abilityName = "Aid";
        abilityImage = Resources.Load<Sprite>(SpiriteDir);
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }



    public void StartCoroutine(GameObject target)
    {
        if (mB != null)
        {
            mB.StartCoroutine(aidAnim(target));
        }
        else
        {
            Debug.Log("mB doesnt exist");
        }
    }

    IEnumerator aidAnim(GameObject target)
    {
        yield return new WaitForSeconds(1f);
        anim.gameObject.SetActive(true);
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        //Debug.Log("Starting disintegrate");
        anim.Play("heal");
        GameObject.Instantiate(teleportEffect, gameObject.transform);
        if (anim != null)
        {
            rotateAtObj(target);
            //anim.Play("Disintegrate");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

        GameObject.Instantiate(healEffect, target.transform);
        yield return new WaitForSeconds(2f);
        target.GetComponent<Actor>().HealHealth(heal);

    }

    public override void ActionSkill(GameObject target)
    {
        base.ActionSkill(target);
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);

            anim.SetTrigger(animTrigger);
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }
        float damage = 10f + ((float)actor.getStrength() * 0.5f);
        //Debug.Log("combo damage = " + damage + " " + actor.getStrength());


        Vector3 currentLoc = gameObject.GetComponent<Transform>().position;
        GameObject effect = GameObject.Instantiate(teleportEffect, actor.getCoords(), Quaternion.identity);

        gameObject.GetComponent<Transform>().position = Enemy.PosCloseTo(position, target.GetComponent<Actor>().getCoords(), map);
        map.SetOcc(gameObject, currentLoc, Enemy.PosCloseTo(position, target.GetComponent<Actor>().getCoords(), map));

        StartCoroutine(target);
        anim.gameObject.SetActive(false);
        
        gameObject.GetComponent<Actor>().setCoords(Enemy.PosCloseTo(position, target.GetComponent<Actor>().getCoords(), map));
        

        DwellTime.Attack(dwell_time);
    }
}
