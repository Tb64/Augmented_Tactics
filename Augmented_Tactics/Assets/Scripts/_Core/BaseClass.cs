using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {

    private Actor player;

    private int skillPoints;
    private int experience;
    private int playerLevel;

    //Stat gains;
    private int strengthGain;
    private int dexterityGain;
    private int constitutionGain;
    private int intelligenceGain;
    private int wisdomGain;
    private int charismaGain;

	void Start () {
        initializeBaseStats();
	}
	

    void initializeBaseStats()
    {
        player = GetComponentInParent<Actor>();

        player.abilitySet = new BasicAttack[4];  
        for (int i = 0; i < 4; i++)
        {
            player.abilitySet[i] = new BasicAttack(gameObject);
        }

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
        playerLevel = 1;

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

    void loadChar(string charName)
    {

    }



    void checkLevel()
    {

        if (experience >= 355000 && playerLevel < 20)
        {
            levelUp();
        }
        else if (experience >= 305000 && playerLevel < 19)
        {
            levelUp();
        }
        else if (experience >= 265000 && playerLevel < 18)
        {
            levelUp();
        }
        else if (experience >= 225000 && playerLevel < 17)
        {
            levelUp();
        }
        else if (experience >= 195000 && playerLevel < 16)
        {
            levelUp();
        }
        else if (experience >= 165000 && playerLevel < 15)
        {
            levelUp();
        }
        else if (experience >= 140000 && playerLevel < 14)
        {
            levelUp();
        }
        else if (experience >= 120000 && playerLevel < 13)
        {
            levelUp();
        }
        else if (experience >= 100000 && playerLevel < 12)
        {
            levelUp();
        }
        else if (experience >= 85000 && playerLevel < 11)
        {
            levelUp();
        }
        else if (experience >= 64000 && playerLevel < 10)
        {
            levelUp();
        }
        else if (experience >= 48000 && playerLevel < 9)
        {
            levelUp();
        }
        else if (experience >= 34000 && playerLevel < 8)
        {
            levelUp();
        }
        else if (experience >= 23000 && playerLevel < 7)
        {
            levelUp();
        }
        else if (experience >= 14000 && playerLevel < 6)
        {
            levelUp();
        }
        else if (experience >= 6500 && playerLevel < 5)
        {
            levelUp();
        }
        else if (experience >= 2700 && playerLevel < 4)
        {
            levelUp();
        }
        else if (experience >= 900 && playerLevel < 3)
        {
            levelUp();
        }
        else if (experience >= 300 && playerLevel < 2)
        {
            levelUp();
        }
       

    }

    #region set/gets

    public int getSkillPoints()
    {
        return skillPoints;
    }
    public void setExperience(int xp)
    {
        experience += xp;
        checkLevel();
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
