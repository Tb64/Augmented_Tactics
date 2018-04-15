using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : StatusEffects {

    GameObject animation;
    //this is just for the thief Sneak Ability so the move won't wear off after the turn ends
    public Disappear(float effect, Actor effector, Actor effected, bool isEnemy,GameObject animation) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Hiding";
        duration = 1; //placeholder until a method for determining this is decided
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        this.animation = animation;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        if (animation != null)
            GameObject.Instantiate<GameObject>(animation, effectorPlayer.gameObject.transform);
        else
            Debug.Log("effect1 null");
        effectorPlayer.gameObject.GetComponent<Renderer>().enabled = true;
        Debug.Log(effectorPlayer + " is Now Visible");
    }
}
