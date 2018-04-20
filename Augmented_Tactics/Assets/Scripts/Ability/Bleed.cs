using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffects{

    public Bleed(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Poisoned";
        duration = Random.Range(1, 4);
        this.effect = effect;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }
}
