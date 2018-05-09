using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : Ability {

    private GameObject effect1 = Resources.Load<GameObject>("Assets/KriptoFX/Realistic Effects Pack v4/Prefabs/Effects/Effect2");
    TileMap map;// =  GameObject.Find("Map").GetComponent<TileMap>();
    public Steal(GameObject obj)
    {
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>(),attacker = gameObject.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            attacker.PlaySound("move");
            rotateAtObj(target);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.GetComponent<Renderer>().enabled = false;
            //DwellTime.Attack(.3f);
            Vector3 initCoords = attacker.getCoords();
            attacker.setCoords(Enemy.PosCloseTo(attacker, targeta.getCoords(), map));
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.GetComponent<Renderer>().enabled = true;
            targeta.TakeDamage(damage, target);
            attacker.PlaySound("attack");
            int choice = Random.Range(0, targeta.usableItems.Count - 1);
            attacker.usableItems.Add(targeta.usableItems[choice]);
            Debug.Log(string.Format("{0} Has Pilfered {1} from {2}", attacker, targeta.usableItems[choice],targeta));
            targeta.usableItems.Remove(targeta.usableItems[choice]);
            rotateAtObj(initCoords);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.GetComponent<Renderer>().enabled = false;
            //DwellTime.Attack(.3f);
            attacker.setCoords(initCoords);
            if (effect1 != null)
                GameObject.Instantiate<GameObject>(effect1, gameObject.transform);
            else
                Debug.Log("effect1 null");
            gameObject.GetComponent<Renderer>().enabled = true;
            DwellTime.Attack(.3f);
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
        map = actor.map;
        //dwell_time = 3.0f;
        abilityName = "Steal";
        abilityImage = Resources.Load<Sprite>("UI/Ability/assassin/assassinSkill6");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
