using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseClass {

	
	
    void KnightStatGains()
    {
        setStrengthGain(5);
        setDexterityGain(2);
        setConstitutionGain(8);
        setIntelligenceGain(2);
        setWisdomGain(2);
        setCharismaGain(5);
    }

    public void taunt()
    {
        //all within a radius enemies will attack the Knight
    }

    //Dark Knight

    //Paladin

    public void smite()
    {
        //deals extra damage to undead enemies
    }
}
