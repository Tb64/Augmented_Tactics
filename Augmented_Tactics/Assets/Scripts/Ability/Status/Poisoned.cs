using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : StatusEffects {

    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect27");

    public Poisoned(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Poisoned";
        duration = Random.Range(1,4);
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
            Debug.Log(effectedPlayer + " taking poison damage from " + effectorPlayer + "'s statuseffect");
            effectedPlayer.TakeDamage(effect, effectorPlayer.gameObject);
            effectedPlayer.UseMana(effect);
        }
        else
            Debug.Log("effect1 null");

    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
    }
}
