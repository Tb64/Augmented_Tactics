using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : StatusEffects {

    GameObject animation;
    private Vector3 initCoords;
    //this is just for the thief Sneak Ability so the move won't wear off after the turn ends
    public Disappear(float effect, Actor effector, Actor effected, bool isEnemy,GameObject animation, Vector3 initCoords) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Hiding";
        duration = 1; 
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        this.animation = animation;
        this.initCoords = initCoords;
        anim = effector.gameObject.GetComponentInChildren<Animator>();
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        if (animation != null)
            GameObject.Instantiate<GameObject>(animation, effectorPlayer.gameObject.transform);
        else
            Debug.Log("effect1 null");
        //initCoords = effectorPlayer.gameObject.transform.localScale;
        effectorPlayer.gameObject.transform.localScale = initCoords;
        Debug.Log(effectorPlayer + " is Now Visible");
    }
}
