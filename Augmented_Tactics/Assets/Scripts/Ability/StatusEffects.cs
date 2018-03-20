using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class StatusEffects : MonoBehaviour
{
    //private FieldInfo fld;
    public virtual void Start()
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    }
    public virtual void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        
       /* if (added)
        {
            fld = effectedPlayer.GetType().GetField(effectedStat);
            fld.SetValue((float)fld.GetValue(effectedStat) - effect);
        }    
        if (subtracted)
            effectedStat += effect;
        if (multiplied)
            effectedStat /= effect;
        if (divided)
            effectedStat *= effect;
       */
    }

    protected int duration;
   // private string effectedStat; useless now due to new method
    protected float effect; //degree of effect
    protected string effectName;
    protected Actor effectedPlayer, effectorPlayer;
    protected GameObject effectedObject, effectorObject; // for areas or spots on the map
    protected bool isEnemy;
    StateMachine SM;
    internal string ID;

    /*public StatusEffect(how many turns, name of variable,text to display when triggered, + - * / in a string, the effected actor,
enemy or player, state machine)*/
    public StatusEffects(int dur, string stat, string name, float effect, string operation,Actor effector, Actor effected, bool isEnemy)
    {
        duration = dur;
        this.effect = effect;
        effectName = name;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
    }
    public StatusEffects(int dur, string stat, string name, float effect, string operation,GameObject effector, GameObject effected, bool isEnemy)
    {
        duration = dur;
        this.effect = effect;
        effectName = name;
        effectedObject = effected;
        effectorObject = effector;
        this.isEnemy = isEnemy;
    }
    public void decreaseTimeCounter()
    {
        if (duration <= 0)
        {
            ReverseEffect();
            Destroy(this);
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

   /* public void addToStat()
    {
        effectedStat += effect;
        added = true;
    }

    public void multiplyStat()
    {
        effectedStat *= effect;
        multiplied = true;
    }

    public void subtractFromStat()
    {
        effectedStat -= effect;
        subtracted = true;
    }

    public void divideStat()
    {
        effectedStat /= effect;
        divided = true;
    }

    private void decideOperation(string op)
    {
        if (op == "+")
            addToStat();
        else if (op == "-")
            subtractFromStat();
        else if (op == "*")
            multiplyStat();
        else if (op == "/")
            divideStat();
        Debug.LogError("Incorrect Operation Given");
    }*/
}
