using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : StatusEffects {

    float heal;

    public override void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        //base.OnDestroy();
    }
    // the only way I could find to solve the constructor issue. necessary for every effect
    public HealOverTime(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Heal";
        duration = 3; //placeholder until a method for determining this is decided
        this.effect = effect;
        //this.effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        heal = effector.getLevel() + effector.getWisdom();
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
        //usually change stat back here but this one didn't require it
        return;
    }

    private void Effect()
    {
        heal = HealCalc();
        Debug.Log(effectedPlayer + " healed " + effectorPlayer + "'s from Heal Over Time statuseffect");
        effectedPlayer.HealHealth(HealCalc());
    }

    private float HealCalc()
    {
        float num1 = effectorPlayer.getLevel();
        float num2 = effectorPlayer.getWisdom();

        if (num1 < num2)
            return Random.Range(num1, num2);
        else
            return Random.Range(num2, num1);
    }
}
