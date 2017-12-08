using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectRPG : MonoBehaviour
{

    private void Start()
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
    }
    public void OnDestroy()
    {
        TurnBehaviour.OnTurnStart -= this.decreaseTimeCounter;
    }
    int duration;
    string effectedStat;
    float effect;
    string effectType;
    Actor effectedPlayer;
    bool isEnemy;
    StateMachine SM;
    public StatusEffectRPG(int dur, string stat, string type, float effect, string operation, Actor effected, bool isEnemy, StateMachine SM)
    {
        this.duration = dur;
        this.effectedStat = stat;
        this.effect = effect;
        this.effectType = type;
        this.effectedPlayer = effected;
        this.isEnemy = isEnemy;
        this.SM = SM;
    }
    public void decreaseTimeCounter()
    {
        if(!isEnemy && SM.checkTurn() || isEnemy && !SM.checkTurn() )
        duration--;
    }


}
