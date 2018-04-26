using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : Ability {

    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect2");
    TileMap map = GameObject.Find("Map").GetComponent<TileMap>();

    public Sneak(GameObject obj)
    {
        Initialize(obj);
        canTargetEnemy = false;
        canTargetFriendly = true; //in reality it can only target itself..
    }
    public override void ActionSkill(GameObject target)
    {
        Actor attacker = gameObject.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(attacker + "is Sneaking Around");
            attacker.PlaySound("move");
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, attacker.GetTileStandingOn().gameObject.transform);
            else
                Debug.Log("effect1 null");
            //gameObject.SetActive(false);
            Vector3 temp = gameObject.transform.localScale;
            DwellTime.Attack(dwell_time);
            //gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.transform.localScale = new Vector3(0, temp.y,0);
            StatusEffectsController.AddEffect(new Disappear(0,attacker,null,gameObject.tag == "Enemy",effect1,temp));
        }
    }
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        canTargetEnemy = false;
        canTargetFriendly = false;
        //can only do to self
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 0;
        range_max = 0;
        range_min = 0;
        damage = 0;
        dwell_time = 1f;
        abilityName = "Sneak";
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill6");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
