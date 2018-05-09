using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStab : Ability {

    TileMap map;// = GameObject.FindWithTag("Map").GetComponent<TileMap>();
    public QuickStab(GameObject obj)
    {
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>(), attacker = gameObject.GetComponent<Actor>();
        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            int temp = attacker.getMoves();
            float temp2 = attacker.getRemainingMovement();
            attacker.setNumOfActions(10);
            attacker.remainingMovement = 100;
            Vector3 initCoords = attacker.getCoords();
            map.moveActorAsync(attacker, Enemy.PosCloseTo(attacker, targeta.getCoords(), map));
            rotateAtObj(target);
            //DwellTime.Attack(dwell_time);
            anim.SetTrigger("MeleeAttack");
            gameObject.GetComponent<Actor>().PlaySound("attack");
            map.moveActorAsync(attacker, initCoords);
            attacker.remainingMovement = temp2;
            attacker.setNumOfActions(0);

        }
        target.GetComponent<Actor>().TakeDamage(CalcPhysicalDamage(damage, target), gameObject);
        DwellTime.Attack(dwell_time);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        manaCost = 0;
        range_max = 5;
        range_min = 0;
        damage = 10f + actor.getStrength() * 1.5f;
        map = actor.map;
        dwell_time = 2f;
        abilityName = "Quick Stab";
        abilityImage = Resources.Load<Sprite>("UI/Skill_Icon_Pack/gray/gray_06");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }
}
