using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseClass {

    

    void Start()
    {
        base.Start();
        initKnightStats();
    }

    void initKnightStats()
    {
        //initialize stat gains per level
        //jobs[0] is index for Knight class
        jobs[0].setJobName("Knight");
        jobs[0].setStrengthGain(5);
        jobs[0].setDexterityGain(2);
        jobs[0].setConstitutionGain(8);
        jobs[0].setIntelligenceGain(2);
        jobs[0].setWisdomGain(2);
        jobs[0].setCharismaGain(5);

        //initialize base stats
       

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
