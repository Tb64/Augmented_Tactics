using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaconed : StatusEffects {

    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect5_Optimized");

    public Beaconed(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Beaconed";
        duration = Random.Range(1, 4);
        this.effect = effect;
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
            Debug.Log(effectedPlayer + " taking vortex damage from " + effectorPlayer + "'s statuseffect");
            effectedPlayer.TakeDamage(effect, effectorPlayer.gameObject);
            effectedPlayer.UseMana((int)effect/2);
        }
        else
            Debug.Log("effect1 null");

    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
    }
}
