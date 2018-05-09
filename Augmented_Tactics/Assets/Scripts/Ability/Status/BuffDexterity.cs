using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDexterity : StatusEffects {

    public bool buff;
    //private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect9"); add glow

    public BuffDexterity(float effect, Actor effector, Actor effected, bool isEnemy, bool buff) : base(effect, effector, effected, isEnemy)
    {
        if (buff)
        {
            this.buff = true;
            effectText = "Dexterity Buff";
        }
        else
        {
            this.buff = false;
            effectText = "Dexterity Debuff";
        }
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        this.effect = effect;
        duration = 2;
        effect = 10;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InitialEffect()
    {
        Debug.Log(buff);
        BuffDebuff.SwapEffect(buff, effectorPlayer, effectedPlayer, effect, "dexterity");
        effectorPlayer.aggroScore += (int)(effect * duration);
    }

    public override void ReverseEffect()
    {
        base.ReverseEffect();
        BuffDebuff.SwapEffect(!buff, effectorPlayer, effectedPlayer, effect, "dexterity");
    }
}
