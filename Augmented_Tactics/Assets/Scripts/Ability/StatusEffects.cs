using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class StatusEffects : MonoBehaviour
{
    //private FieldInfo fld;
    private void Start()
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateMachine>();
    }
    public void OnDestroy()
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

    private int duration;
    private string effectedStat;
    private float effect;
    string effectName;
    Actor effectedPlayer;
    bool isEnemy;
    StateMachine SM;
    internal string ID;

    /*public StatusEffect(how many turns, name of variable,text to display when triggered, + - * / in a string, the effected actor,
enemy or player, state machine)*/
 /*   public StatusEffects(int dur, string stat, string name, float effect, string operation, Actor effected, bool isEnemy)
    {
        this.duration = dur;
        //effected.GetType().GetField(stat);
        this.effectedStat = stat; //operation that takes in the stat name and returns the variable using the actor
        this.effect = effect;
        this.effectName = name;
        this.effectedPlayer = effected;
        this.isEnemy = isEnemy;
       // decideOperation(operation);
    }*/
    public void decreaseTimeCounter()
    {
        if (duration <= 0)
        {
            reverseEffect();
            Destroy(this);
        }
        if (!isEnemy && SM.checkTurn() || isEnemy && !SM.checkTurn())
        {
            induceEffect();
            duration--;
        }      
    }

    public void induceEffect()
    {
        //another template: use this to process turn based damage loss etc if necessary
    }
    public void reverseEffect()
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
