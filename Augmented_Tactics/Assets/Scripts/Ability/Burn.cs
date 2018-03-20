using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffects
{
	// Use this for initialization
	public override void Start ()
    {
        base.Start();

	}

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    // the only way I could find to solve the constructor issue. necessary for every effect
    public Burn(int dur, string stat, string name, float effect, string operation,Actor effector, Actor effected, bool isEnemy) : base(dur, stat, name, effect,  operation,effector , effected, isEnemy)
    {
        this.duration = dur;
        this.effect = effect;
        this.effectName = name;
        this.effectedPlayer = effected;
        this.isEnemy = isEnemy;
    }

    public override void InduceEffect() //needs to be altered slightly for different effects. Gets called every
     //turn so any effect that reduces a stat etc needs to have a check here so it doesn't keep altering the stat further
    {
        effectedPlayer.TakeDamage(10, )
    }

    public override void ReverseEffect()
    {
        
    }
}
