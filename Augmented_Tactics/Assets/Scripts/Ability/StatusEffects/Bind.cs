using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind : StatusEffects {

    private int savedMoveDist;
    private GameObject visualEffect;

    public override void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        //base.OnDestroy();
    }
    // the only way I could find to solve the constructor issue. necessary for every effect
    public Bind(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Bind";
        duration = 1; //placeholder until a method for determining this is decided
        this.effect = effect;
        //this.effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        visualEffect = Resources.Load<GameObject>("Effects/HandEffects/Effect13_Hand_Optimized");
        savedMoveDist = effected.moveDistance;
    }

    public override void InitialEffect()
    {
        base.InitialEffect();
        Effect();
    }

    public override void InduceEffect() //needs to be altered slightly for different effects. Gets called every
                                        //turn so any effect that reduces a stat etc needs to have a check here so it doesn't keep altering the stat further
    {
        Effect();
    }

    public override void ReverseEffect()
    {
        effectedPlayer.moveDistance = savedMoveDist;
        return;
    }

    private void Effect()
    {
        if(visualEffect != null)
            GameObject.Instantiate<GameObject>(visualEffect, effectedPlayer.transform);
        effectedPlayer.TakeDamage(effectorPlayer.getLevel(),effectorObject);
        effectedPlayer.moveDistance = 0;
    }
}
