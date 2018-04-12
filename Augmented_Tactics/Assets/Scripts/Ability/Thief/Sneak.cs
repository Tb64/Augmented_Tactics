using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : Ability {

    private GameObject effect1 = Resources.Load<GameObject>("Assets/KriptoFX/Realistic Effects Pack v4/Prefabs/Effects/Effect2");
    TileMap map = GameObject.Find("Map").GetComponent<TileMap>();

    public Sneak(GameObject obj)
    {
        Initialize(obj);
    }
    public override void ActionSkill(GameObject target)
    {
        Actor attacker = gameObject.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(attacker + "is Sneaking Around");
            attacker.PlaySound("move");
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.GetComponent<Renderer>().enabled = false;
            DwellTime.Attack(.3f);
            new Disappear(0,attacker,null,gameObject.tag == "Enemy",effect1);
        }
    }
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        canTargetEnemy = false;
        canTargetFriendly = false;
        //can only do to self
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 15;
        range_max = 1;
        range_min = 0;
        damage = 0;
        //dwell_time = 3.0f;
        abilityName = "Steal";
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill6");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
