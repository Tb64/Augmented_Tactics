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
    private GameObject effect1 = Resources.Load<GameObject>("Effects/Effect3_Optimized");

    // the only way I could find to solve the constructor issue. necessary for every effect
    public Burn(float effect,Actor effector, Actor effected, bool isEnemy) : base(effect,effector , effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Burning";
        duration = 3; //placeholder until a method for determining this is decided
        this.effect = effect;
        //this.effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InduceEffect() //needs to be altered slightly for different effects. Gets called every
     //turn so any effect that reduces a stat etc needs to have a check here so it doesn't keep altering the stat further
    {
        if (effect1 != null)
        {
            GameObject.Instantiate<GameObject>(effect1, effectedPlayer.gameObject.transform);
            Debug.Log(effectedPlayer + " taking burn damage from " + effectorPlayer + "'s statuseffect");
            effectedPlayer.TakeDamage(effect, effectorPlayer.gameObject);
            //somewhere here animation can be added
        }
        else
            Debug.Log("effect1 null");
        
    }

    public override void ReverseEffect()
    {
        //usually change stat back here but this one didn't require it
        base.ReverseEffect();
        return;
    }
}
