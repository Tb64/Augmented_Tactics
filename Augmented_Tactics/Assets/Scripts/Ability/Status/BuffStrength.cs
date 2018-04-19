using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStrength : StatusEffects {

    public bool buff;
    //private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9"); add glow

    public BuffStrength(float effect, Actor effector, Actor effected, bool isEnemy, bool buff) : base(effect, effector, effected, isEnemy)
    {
        if (buff)
        {
            this.buff = true;
            effectText = "Strength Buff";
        }
        else
        {
            this.buff = false;
            effectText = "Strength Debuff";
        }
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        duration = 2;
        this.effect = effect;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InitialEffect()
    {
        BuffDebuff.SwapEffect(buff, effectorPlayer, effectedPlayer, effect, "strength");
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        BuffDebuff.SwapEffect(!buff, effectorPlayer, effectedPlayer, effect, "strength");
    }
}
