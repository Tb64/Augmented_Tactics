using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class StatusEffects
{
    //private FieldInfo fld;
   /* public virtual void Start()
    {
        
    }*/
    public virtual void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
    }

    protected int duration;
   // private string effectedStat; useless now due to new method
    protected float effect; //degree of effect
    protected string effectText;
    protected Actor effectedPlayer, effectorPlayer;
    protected GameObject effectedObject, effectorObject; // for areas or spots on the map
    protected bool isEnemy;
    StateMachine SM;
    internal string ID;

    /*public StatusEffect(degree of effect (damage or multiplier etc.) the actor doing damage, the effected actor, enemy or player?)*/
    public StatusEffects(float effect, Actor effector, Actor effected, bool isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
        //duration = dur;
        this.effect = effect;
        //effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
    }
    public StatusEffects(float effect,GameObject effector, GameObject effected, bool isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
        //duration = dur;
        this.effect = effect;
        //effectName = name;
        effectedObject = effected;
        effectorObject = effector;
        this.isEnemy = isEnemy;
    }
    public void decreaseTimeCounter()
    {
        Debug.Log("Time Counter Triggered: " + duration);
        if (duration <= 0)
        {
            ReverseEffect();
            OnDestroy();
            //TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
            
        }
        if (!isEnemy && SM.checkTurn() || isEnemy && !SM.checkTurn())
        {
            InduceEffect();
            duration--;
        }      
    }

    public virtual void InduceEffect()
    {
        //another template: use this to process turn based damage loss etc if necessary
    }
    public virtual void ReverseEffect()
    {
       //just a template. overload this with an undo of your effect if necessary 
    }

   
}
