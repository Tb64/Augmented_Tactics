using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDefense : StatusEffects {

    public bool buff, physical;
    //private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9"); add glow

    public BuffDefense(float effect, Actor effector, Actor effected, bool isEnemy, bool buff,bool physical) : base(effect, effector, effected, isEnemy)
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
        duration = Random.Range(1,3);
        this.physical = physical;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InitialEffect()
    {
        if(physical)
            BuffDebuff.SwapEffect(buff, effectorPlayer, effectedPlayer, effect, "physicaldefense");
        else
            BuffDebuff.SwapEffect(buff, effectorPlayer, effectedPlayer, effect, "magicdefense");
        effectorPlayer.aggroScore += (int)effect;
    }

    public override void InduceEffect()
    {
        effectorPlayer.aggroScore += (int)effect;
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        BuffDebuff.SwapEffect(!buff, effectorPlayer, effectedPlayer, effect, "defense");
    }
}
