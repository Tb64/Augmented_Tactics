using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseClass {

    

    void Start()
    {
        base.Start();
        initKnightStats();
    }

    public void initKnightStats()
    {
        //initialize stat gains per level
        //jobs[0] is index for Knight class
        setJobName("Knight");
        setStrengthGain(5);
        setDexterityGain(2);
        setConstitutionGain(8);
        setIntelligenceGain(2);
        setWisdomGain(2);
        setCharismaGain(5);

        //initialize base stats
        player.abilitySet[0] = new Eviscerate(gameObject);

    }

    public void taunt()
    {
        //all enemies within a radius will attack the Knight
    }

    //Dark Knight

    public void bloodHarvest()
    {
        //passive skill which heals Dark knight with each attack
    }


    //Paladin

    public void smite()
    {
        //deals extra damage to undead enemies
    }

    public void heal()
    {
        //heals self or other friendly unit, or does damage to undead units
    }
}
