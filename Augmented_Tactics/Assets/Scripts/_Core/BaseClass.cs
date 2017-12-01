using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {

    protected Actor player;
    private GameData savedPlayerData = new GameData();    

    private int skillPoints;
    private int experience;
    private int playerLevel;
    private string className;

    public BaseClass[] jobs;

    //Stat gains;
    private int strengthGain;
    private int dexterityGain;
    private int constitutionGain;
    private int intelligenceGain;
    private int wisdomGain;
    private int charismaGain;

    public virtual void Start() {


        initializeBaseStats();

        //loadChar("Doogy");
        /*if (loadChar("Doogy"))
        {
            Debug.Log("Successful Load!!");
            Debug.Log(player.getCharisma());
            //levelUp();
            Debug.Log(player.getStrength());
            //levelUp(); 
        }
        /*levelUp();
        levelUp();
        saveChar("Doogy");
        Debug.Log("Successful Save");*/
    }

    void initializeBaseStats()
    {
        player = GetComponentInParent<Actor>();

        player.abilitySet = new Ability[4];
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
        player.setHealthCurrent(player.GetHeathMax());
        player.setManaCurrent(player.getMaxMana());


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

    bool loadChar(string charName)
    {
        PlayerData character = savedPlayerData.findPlayer(charName);
        if (character == null)
        {
            Debug.LogError("Character " + " does not exist!");
            return false;
        }
        else
        {
            setExperience((int)character.getStatByKey("Experience"));
            skillPoints = (int)character.getStatByKey("Skill Points");
            playerLevel = (int)character.getStatByKey("Level");
            player.setDexterity((int)character.getStatByKey("Dexterity"));
            player.setIntelligence((int)character.getStatByKey("Intelligence"));
            player.setCharisma((int)character.getStatByKey("Charisma"));
            player.setConstitution((int)character.getStatByKey("Constitution"));
            player.setSpeed((int)character.getStatByKey("Speed"));
            player.setStrength((int)character.getStatByKey("Strength"));
            player.setWisdom((int)character.getStatByKey("Wisdom"));
            player.setArmorClass(character.getStatByKey("Armor Class"));
            player.setHealthCurrent((int)character.getStatByKey("Health"));
            player.setManaCurrent((character.getStatByKey("Mana")));
            return true;
        }
    }

    private bool saveChar(string charName)
    {
        PlayerData newData = new PlayerData(charName);
        newData.setStatbyKey("Experience", getExperience());
        newData.setStatbyKey("Skill Points", getSkillPoints());
        newData.setStatbyKey("Player Level", playerLevel);
        newData.setStatbyKey("Dexterity", player.getDexterity());
        newData.setStatbyKey("Intelligence", player.getIntelligence());
        newData.setStatbyKey("Charisma", player.getCharisma());
        newData.setStatbyKey("Constitution", player.getConstitution());
        newData.setStatbyKey("Speed", player.getSpeed());
        newData.setStatbyKey("Strength", player.getStrength());
        newData.setStatbyKey("Wisdom", player.getWisdom());
        newData.setStatbyKey("Armor Class", player.getArmorClass());
        newData.setStatbyKey("Health", player.GetHealthCurrent());
        newData.setStatbyKey("Mana", player.getManaCurrent());
        return savedPlayerData.savePlayer(newData);
    }

    private void triggerStatusEffect()
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

    public void setJobName(string name)
    {
        className = name;
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
