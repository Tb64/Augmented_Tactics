using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {

    private Actor player;

    private int skillPoints;
    private int experience;

    //Stat gains;
    private int strengthGain;
    private int dexterityGain;
    private int constitutionGain;
    private int intelligenceGain;
    private int wisdomGain;
    private int charismaGain;


	// Use this for initialization
	void Start () {
        initializeBaseStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void initializeBaseStats()
    {
        player.setStrength(10);
        player.setDexterity(10);
        player.setConstitution(10);
        player.setIntelligence(10);
        player.setWisdom(10);
        player.setCharisma(10);

        player.setMaxHealth(10 * player.getConstitution());
        player.setMaxMana(10 * player.getIntelligence());

        skillPoints = 1;
        experience = 0;

        //default values for stat gains;
        strengthGain = 1;
        dexterityGain = 1;
        constitutionGain = 1;
        intelligenceGain = 1;
        wisdomGain = 1;
        charismaGain = 1;
    }

    void levelUp() //stat gains will be set by each class
    {
        player.setStrength(player.getStrength() + strengthGain);
        player.setDexterity(player.getDexterity() + dexterityGain);
        player.setConstitution(player.getConstitution() + constitutionGain);
        player.setIntelligence(player.getIntelligence() + intelligenceGain);
        player.setWisdom(player.getWisdom() + wisdomGain);
        player.setCharisma(player.getCharisma() + charismaGain);

        skillPoints++;
    }

    #region set/gets

    public int getSkillPoints()
    {
        return skillPoints;
    }
    public void setExperience(int xp)
    {
        experience += xp;
    }
    public int getExperience()
    {
        return experience;
    }

    
    //===============stat gains================//

    //the amount each stat will increase with each level + 1
    public void setStrengthGain(int gain)
    {
        strengthGain = gain;
    }

    public void setDexterityGain(int gain)
    {
        dexterityGain = gain;
    }

    public void setConstitutionGain(int gain)
    {
        constitutionGain = gain;
    }

    public void setIntelligenceGain(int gain)
    {
        intelligenceGain = gain;
    }

    public void setWisdomGain(int gain)
    {
        wisdomGain = gain;
    }

    public void setCharismaGain(int gain)
    {
        charismaGain = gain;
    }

    #endregion

}
