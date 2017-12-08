using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{

    private void Start()
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
    }
    public void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
        if (added)
            effectedStat -= effect;
        if (subtracted)
            effectedStat += effect;
        if (multiplied)
            effectedStat /= effect;
        if (divided)
            effectedStat *= effect;
    }
    int duration;
    float effectedStat;
    float effect;
    string effectName;
    Actor effectedPlayer;
    bool isEnemy, added, multiplied, subtracted, divided;
    StateMachine SM;
    /*public StatusEffect(how many turns, name of variable,text to display when triggered, + - * / in a string, the effected actor,
     enemy or player, state machine)*/
    public StatusEffect(int dur, string stat, string name, float effect, string operation, Actor effected, bool isEnemy, StateMachine SM)
    {
        this.duration = dur;
        this.effectedStat = (float)stat; //operation that takes in the stat name and returns the variable using the actor
        this.effect = effect;
        this.effectName = name;
        this.effectedPlayer = effected;
        this.isEnemy = isEnemy;
        this.SM = SM;
        decideOperation(operation);
    }
    public void decreaseTimeCounter()
    {
        if(!isEnemy && SM.checkTurn() || isEnemy && !SM.checkTurn() )
        duration--;
        if (duration <= 0)
            Destroy(this);
    }

    public void addToStat()
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
    }


}
