using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffects
{
    // Use this for initialization
	/*public override void Start ()
    {
        //base.Start();
        
	}*/

    public override void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        //base.OnDestroy();
    }
    // the only way I could find to solve the constructor issue. necessary for every effect
    public Burn(float effect,Actor effector, Actor effected, bool isEnemy) : base(effect,effector , effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Scorched";
        duration = 3; //placeholder until a method for determining this is decided
        this.effect = effect;
        //this.effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
    }

    public override void InduceEffect() //needs to be altered slightly for different effects. Gets called every
     //turn so any effect that reduces a stat etc needs to have a check here so it doesn't keep altering the stat further
    {
        Debug.Log(effectedPlayer+ " taking burn damage from "+ effectorPlayer+"'s statuseffect");
        effectedPlayer.TakeDamage(10, effectorPlayer.gameObject);
        //somewhere here animation can be added
    }

    public override void ReverseEffect()
    {
        //usually change stat back here but this one didn't require it
        return;
    }
}
