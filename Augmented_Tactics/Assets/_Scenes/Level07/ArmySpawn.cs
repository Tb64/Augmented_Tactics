using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmySpawn : StatusEffects {

    private GameObject effect1; // need to add rift opening and spawning character animation
    //the rift should open near the boss or somewhere random and bring two more enemies

    public ArmySpawn(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Rift Opened";
        duration = 10000;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InduceEffect()
    {
        if (effect1 != null)
        {
            GameObject.Instantiate<GameObject>(effect1, effectedPlayer.gameObject.transform);
            Debug.Log(effectedPlayer + " Spawning New Allies From The Open Rift!");
            //add in however to make the players come out
        }
        else
            Debug.LogError("effect1 null");

    }

    public override void ReverseEffect()
    {
        //this effect should never run out
    }
}
