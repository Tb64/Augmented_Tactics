using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : StatusEffects {

    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9");

    public Frozen(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Frozen";
        duration = Random.Range(1, 2);
        //this.effect = effect;
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
            Debug.Log(effectedPlayer + " is frozen solid from " + effectorPlayer + "'s statuseffect");
            DwellTime.Attack(2f);
            effectedPlayer.remainingMovement = 0;
        }
        else
            Debug.LogError("effect1 null");

    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
    }
}
