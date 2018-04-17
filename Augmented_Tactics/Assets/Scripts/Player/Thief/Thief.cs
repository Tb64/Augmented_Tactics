using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : BaseClass
{

    // Use this for initialization
    void Start()
    {
        base.Start();
        initThiefStats();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void initThiefStats()
    {
        //initialize stat gains per level
        //jobs[1] is index for Thief class
        setJobName("Thief");
        setStrengthGain(2);
        setDexterityGain(8);
        setConstitutionGain(5);
        setIntelligenceGain(2);
        setWisdomGain(2);
        setCharismaGain(5);
    }

    public void stealItem()
    {

    }

    public void Hide()
    {

    }

    public void setTrap()
    {

    }
}
