using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect {

        int duration;
        string effectedStat;
        float effect;
        string effectType;
        public StatusEffect(int dur, string stat, string type, float effect, string operation)
        {
            this.duration = dur;
            this.effectedStat = stat;
            this.effect = effect;
            this.effectType = type;
        }
        public void decreaseTimeCounter()
        {
            duration--;
        }


}
