using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : Ability {

    private GameObject effect1 = Resources.Load<GameObject>("Assets/KriptoFX/Realistic Effects Pack v4/Prefabs/Effects/Effect2");
    TileMap map =  GameObject.Find("Map").GetComponent<TileMap>();
    public Steal(GameObject obj)
    {
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            //gameObject.GetComponent<Actor>().PlaySound("attack");
            gameObject.SetActive(false);
            DwellTime.Attack(.5f);
            Actor attacker = gameObject.GetComponent<Actor>();
            Vector3 initCoords = attacker.getCoords();
            attacker.setCoords(Enemy.PosCloseTo(attacker, targeta.getCoords(), map));
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.SetActive(true);
            targeta.TakeDamage(damage, target);
            TakeSomething(targeta);
            DwellTime.Attack(.5f);
            rotateAtObj(initCoords);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.SetActive(false);
            attacker.setCoords(initCoords);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.SetActive(true);
        }
        
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 10;
        range_max = 7;
        range_min = 0;
        damage = 0;
        //dwell_time = 3.0f;
        abilityName = "Steal";
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill6");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    private void TakeSomething(Actor victim)
    {
        //add here once items are working. take item from person 0d
    }
}
