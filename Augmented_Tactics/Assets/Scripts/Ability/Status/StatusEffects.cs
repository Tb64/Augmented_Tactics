using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class StatusEffects
{
    private static List<StatusEffects> currentEffects;
    public int duration;

    public virtual void OnDestroy()
    {
        ReverseEffect();
        if (currentEffects != null)
            currentEffects.Remove(this);
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
    }

    protected float effect; //degree of effect
    protected string effectText;
    public string getName() { return effectText; }
    public Actor effectedPlayer, effectorPlayer;
    public GameObject effectedObject, effectorObject; // for areas or spots on the map
    protected bool isEnemy;
    protected Animator anim;
    StateMachine SM;
    internal string ID;

    /*public StatusEffect(degree of effect (damage or multiplier etc.) the actor doing damage, the effected actor, enemy or player?)*/
    public StatusEffects(float effect, Actor effector, Actor effected, bool isEnemy) // person on person
    {
        Init(effect, effector, effected, isEnemy);
    }
    public StatusEffects(float effect, Actor effector, GameObject effected, bool isEnemy) //person on object or map
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
        this.effect = effect;
        effectedObject = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        InitialEffect();
    }
    public StatusEffects(float effect,GameObject effector, GameObject effected, bool isEnemy) //object / map on map
    {
        Init(effect, effector.GetComponent<Actor>(), effected.GetComponent<Actor>(), isEnemy);
    }

    public void Init(float effect, Actor effector, Actor effected, bool isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
        //duration = dur;
        this.effect = effect;
        //effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        effectedObject = effected.gameObject;
        effectorObject = effector.gameObject;
        this.isEnemy = isEnemy;
        if (currentEffects != null)
            currentEffects.Add(this);
        else
        {
            currentEffects = new List<StatusEffects>();
            currentEffects.Add(this);
        }
        InitialEffect();
    }
    public void decreaseTimeCounter()
    {
        Debug.Log("Time Counter Triggered: " + duration);
        if (duration <= 0)
        {
            OnDestroy();
        }
        if (!isEnemy && SM.checkTurn() || isEnemy && !SM.checkTurn())
        {
            InduceEffect();
            duration--;
        }      
    }

    public virtual void InitialEffect()
    {

    }

    public virtual void InduceEffect()
    {
        //another template: use this to process turn based damage loss etc if necessary
    }
    public virtual void ReverseEffect()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        //just a template. overload this with an undo of your effect if necessary 
    }

    public static List<StatusEffects> GetAllEffects()
    {
        return currentEffects;
    }
}
