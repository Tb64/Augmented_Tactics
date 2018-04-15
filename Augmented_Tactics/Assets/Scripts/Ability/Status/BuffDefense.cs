using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDefense : StatusEffects {

    public bool buff;
    //private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9"); add glow

    public BuffDefense(float effect, Actor effector, Actor effected, bool isEnemy, bool buff) : base(effect, effector, effected, isEnemy)
    {
        if (buff)
        {
            buff = true;
            effectText = "Defensive Buff";
        }
        else
        {
            buff = false;
            effectText = "Defensive Debuff";
        }
        this.effect = effect;
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        duration = 2;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InitialEffect()
    {
        BuffDebuff.SwapEffect(buff, effectorPlayer, effectedPlayer, effect, "defense");
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        BuffDebuff.SwapEffect(!buff, effectorPlayer, effectedPlayer, effect, "defense");
    }
}
