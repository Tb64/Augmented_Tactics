using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public PlayerData(string name)
    {
        playerName = name;
        unlocked = false;
    }
    private bool unlocked;
    //public bool[] unlockedSkills;
    public void unlockPlayer() { unlocked = true; }
    public string playerName;
    public void setPlayerName(string name) { playerName = name; }
    public string getPlayerName() { return playerName; }
    public int cost;
    public int Health;
    public int Mana;
    public int Experience;
    public int Level;
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public int Wisdom;
    //public int Charismast;
    public int Stealth;
    public int ArmorClass;
    public int Speed;
    public int SkillPoints;
    public int Class;
    public int PlayerLevel;

    public string DisplayName;
    public string ClassName;
    public string Icon;
    public string Prefab;
    public string Skill1;
    public string Skill2;
    public string Skill3;
    public string Skill4;
    public string Item1;
    public string Item2;
    public string Item3;
    public string Item4;

    public bool UnlockSkill1 = false;
    public bool UnlockSkill2 = false;
    public bool UnlockSkill3 = false;
    public bool UnlockSkill4 = false;
    public bool UnlockSkill5 = false;
    public bool UnlockSkill6 = false;
    public bool UnlockSkill7 = false;
    public bool UnlockSkill8 = false;

    public Armor armor;
    public Weapons weapon;

    public KeyValuePair<string, float>[] numberData = 
    {
        new KeyValuePair<string, float>("Health", 20),
        new KeyValuePair<string, float>("Mana", 15),
        new KeyValuePair<string, float>("Experience", 0),
        new KeyValuePair<string, float>("Level",1 ),
        new KeyValuePair<string, float>("Strength",10),
        new KeyValuePair<string, float>("Dexterity",13),
        new KeyValuePair<string, float>("Constitution",14),
        new KeyValuePair<string, float>("Intelligence",15),
        new KeyValuePair<string, float>("Wisdom",20),
        new KeyValuePair<string, float>("Charisma",5),
        new KeyValuePair<string, float>("Stealth",0),
        new KeyValuePair<string, float>("Armor Class",0),
        new KeyValuePair<string, float>("Speed",15),
        new KeyValuePair<string, float>("Skill Points",0),
        new KeyValuePair<string, float>("Class", 0),
        new KeyValuePair<string, float>("Player Level",1)
    };
    
    private KeyValuePair<string, string>[] stringData =
    {
        new KeyValuePair<string, string>("DisplayName", ""),
        new KeyValuePair<string, string>("ClassName", ""),
        new KeyValuePair<string, string>("Icon", ""),
        new KeyValuePair<string, string>("Prefab", ""),
        new KeyValuePair<string, string>("Skill1", ""),
        new KeyValuePair<string, string>("Skill2", ""),
        new KeyValuePair<string, string>("Skill3", ""),
        new KeyValuePair<string, string>("Skill4", "")
    };

    public KeyValuePair<string, float>[] getNumberData(){return numberData;}
    public KeyValuePair<string, string>[] getStringData() {return stringData; }

    //All of this code seems unncecessary now so I left it
    /*public float getValueByKey(string key)
    {
        float value = this.numberContains(key);
        if(value == -1)
        {
            value = this.stringContains(key);
            if(value == -1)
            {
                Debug.LogError(key + " does not exist in this save");
            }
        }
        return value;
    }*/

    /// <summary>
    /// Creates random Level 1 player
    /// </summary>
    /// <returns></returns>
    public static PlayerData GenerateNewPlayer()
    {
        int randomIndex = (int)Random.Range(0, CharacterClasses.classNames.Length);
        return GenerateNewPlayer(randomIndex);
    }

    /// <summary>
    /// Creates random level 1 player of a selected class
    /// </summary>
    /// <param name="classID"></param>
    /// <returns></returns>
    public static PlayerData GenerateNewPlayer(int classID)
    {
        PlayerData player = null;

        string name = RandomName();
        player = new PlayerData(name);
        player.DisplayName = name;
        int randomNum = Random.Range(0, 100);
        player.playerName = name + randomNum; //this can be a problem because playerName must be unique
        player.Class = classID;
        player.ClassName = CharacterClasses.classNames[classID];
        PlayerBaseLine(player);
        RandomStatBoost(player);

        player.Health = player.Constitution * 10;

        player.Mana = (player.Intelligence + player.Wisdom) * 5;

        if (player.DisplayName.Length == 0)
            Debug.Log("GenerateNewPlayer Empty Name ");

        if (player.ClassName.Length == 0)
            Debug.Log("GenerateNewPlayer Empty Class " + player.Class);

        return player;
    }

    private static void PlayerBaseLine(PlayerData input)
    {
        input.Strength = 5;
        input.Dexterity = 5;
        input.Constitution = 5;
        input.Intelligence = 5;
        input.Wisdom = 5;
        input.Speed = 3;
        input.SkillPoints = 1;
        input.Level = 1;
        input.PlayerLevel = 1;

        input.UnlockSkill1 = true;
        input.Skill1 = SkillLoader.ClassSkills(input.Class)[0];
        input.Skill2 = SkillLoader.ClassSkills(input.Class)[0];
        input.Skill3 = SkillLoader.ClassSkills(input.Class)[0];
        input.Skill4 = SkillLoader.ClassSkills(input.Class)[0];
        input.weapon = WeaponGen.WeaponGenerate(1, input.Class, 0);
        input.armor = ArmorGen.ArmorGenerate(1, input.Class, 0);
        input.Prefab = CharacterClasses.PrefabPath[input.Class];
        input.Icon = CharacterClasses.IconPath[input.Class];
    }

    public static void LevelUp(PlayerData input, bool cheater)
    {
        if(cheater || input.Experience >= PlayerKey.LevelCaps[input.Level])
        {
            if(input.Level >= 8)
            {
                Debug.Log("Max level " + input.playerName);
                return;
            }
            input.Level++;
            RandomStatBoost(input);
            Debug.Log("Leveling up " + input.playerName + " cheater=" + cheater);
        }
        else
        {
            Debug.Log("Not enough EXP for level: " + input.playerName);
            return;
        }
    }

    public static void LevelUp(PlayerData input)
    {
        LevelUp(input, false);
    }


    private static void RandomStatBoost(PlayerData input)
    {
        int randomStat;
        for (int index = 0; index < 3; index++)
        {
            randomStat = (int)Random.Range(0, 5);
            switch (randomStat)
            {
                case 0:
                    input.Strength++;
                    break;

                case 1:
                    input.Dexterity++;
                    break;

                case 2:
                    input.Constitution++;
                    break;

                case 3:
                    input.Intelligence++;
                    break;

                case 4:
                    input.Wisdom++;
                    break;

                default:
                    break;
            }
        }

        switch (input.Class)
        {
            case CharacterClasses.PaladinKey:
                input.Constitution += 2;
                break;

            case CharacterClasses.DarkKnightKey:
                input.Strength += 2;
                break;

            case CharacterClasses.ThiefKey:
                input.Dexterity += 2;
                break;

            case CharacterClasses.BrawlerKey:
                input.Constitution += 1;
                input.Strength += 1;
                break;

            case CharacterClasses.ClericKey:
                input.Wisdom += 2;
                break;

            case CharacterClasses.MageKey:
                input.Intelligence += 2;
                break;

            default:
                break;
        }
    }

    private static void RandomClass(PlayerData input)
    {
        int randomIndex = (int)Random.Range(0, CharacterClasses.classNames.Length);
        input.Class = randomIndex;
        input.ClassName = CharacterClasses.classNames[randomIndex];
    }

    private static string RandomName()
    {
        string filePath = Application.streamingAssetsPath + "/armyNames.txt";
        //Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string[] nameData = File.ReadAllLines(filePath);

            int randomIndex = (int)Random.Range(0,nameData.Length);

            return nameData[randomIndex];

        }
        else
        {
            Debug.Log("Can't Find Name Data at " + filePath);
            return "";
        }
    }

    public float getStatByKey(string key)
    {
        float stat = -1;
        switch(key)
        {
            case PlayerKey.Health:
                return this.Health;

            case PlayerKey.Mana:
                return this.Mana;

            case PlayerKey.Experience:
                return this.Experience;

            case PlayerKey.Level:
                return this.Level;

            case PlayerKey.Strength:
                return this.Strength;

            case PlayerKey.Dexterity:
                return this.Dexterity;

            case PlayerKey.Constitution:
                return this.Constitution;

            case PlayerKey.Intelligence:
                return this.Intelligence;

            case PlayerKey.Wisdom:
                return this.Wisdom;

            case PlayerKey.Stealth:
                return this.Stealth;

            case PlayerKey.ArmorClass:
                return this.ArmorClass;

            case PlayerKey.Speed:
                return this.Speed;

            case PlayerKey.SkillPoints:
                return this.SkillPoints;

            case PlayerKey.Class:
                return this.Class;

            case PlayerKey.PlayerLevel:
                return this.PlayerLevel;

            default:
                break;
        }
        return stat;
    }

    public string getStringByKey(string key)
    {
        string stat = "";
        switch (key)
        {
            case PlayerKey.DisplayName:
                return this.DisplayName;

            case PlayerKey.ClassName:
                return this.ClassName;

            case PlayerKey.Icon:
                return this.Icon;

            case PlayerKey.Prefab:
                return this.Prefab;

            case PlayerKey.Skill1:
                return this.Skill1;

            case PlayerKey.Skill2:
                return this.Skill2;

            case PlayerKey.Skill3:
                return this.Skill3;

            case PlayerKey.Skill4:
                return this.Skill4;

            case PlayerKey.Item1:
                return this.Item1;

            case PlayerKey.Item2:
                return this.Item2;

            case PlayerKey.Item3:
                return this.Item3;

            case PlayerKey.Item4:
                return this.Item4;

            default:
                break;
        }
        Debug.LogError("Failed to load " + key + " from GameData");
        return stat;
    }

    public void setStatbyKey(string key, float stat)
    {
        switch (key)
        {
            case PlayerKey.Health:
                this.Health = (int)stat;
                return;

            case PlayerKey.Mana:
                this.Mana = (int)stat;
                return;

            case PlayerKey.Experience:
                this.Experience = (int)stat;
                return;

            case PlayerKey.Level:
                this.Level = (int)stat;
                return;

            case PlayerKey.Strength:
                this.Strength = (int)stat;
                return;

            case PlayerKey.Dexterity:
                this.Dexterity = (int)stat;
                return;

            case PlayerKey.Constitution:
                this.Constitution = (int)stat;
                return;

            case PlayerKey.Intelligence:
                this.Intelligence = (int)stat;
                return;

            case PlayerKey.Wisdom:
                this.Wisdom = (int)stat;
                return;

            case PlayerKey.Stealth:
                this.Stealth = (int)stat;
                return;

            case PlayerKey.ArmorClass:
                this.ArmorClass = (int)stat;
                return;

            case PlayerKey.Speed:
                this.Speed = (int)stat;
                return;

            case PlayerKey.SkillPoints:
                this.SkillPoints = (int)stat;
                return;

            case PlayerKey.Class:
                this.Class = (int)stat;
                return;

            case PlayerKey.PlayerLevel:
                this.PlayerLevel = (int)stat;
                return;

            default:
                break;
        }
        Debug.LogError("Unable to save " + key + " stat");
    }

    public void setStatbyKey(string key, string stat)
    {
        switch (key)
        {
            case PlayerKey.DisplayName:
                this.DisplayName = stat;
                return;

            case PlayerKey.ClassName:
                this.ClassName = stat;
                return;

            case PlayerKey.Icon:
                this.Icon = stat;
                return;

            case PlayerKey.Prefab:
                this.Prefab = stat;
                return;

            case PlayerKey.Skill1:
                this.Skill1 = stat;
                return;

            case PlayerKey.Skill2:
                this.Skill2 = stat;
                return;

            case PlayerKey.Skill3:
                this.Skill3 = stat;
                return;

            case PlayerKey.Skill4:
                this.Skill4 = stat;
                return;

            default:
                break;
        }
        Debug.LogError("Unable to save " + key + " stat");
    }


    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalStr()
    {
        return Strength + armor.str_bonus + weapon.str_bonus;
    }
    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalDex()
    {
        return Dexterity + armor.dex_bonus + weapon.dex_bonus;
    }
    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalCon()
    {
        return Constitution + armor.con_bonus + weapon.con_bonus;
    }
    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalWis()
    {
        return Wisdom + armor.wis_bonus + weapon.wis_bonus;
    }
    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalInt()
    {
        return Intelligence + armor.int_bonus + weapon.int_bonus;
    }
    /// <summary>
    /// Character stats + item bonus
    /// 
    /// </summary>
    /// <returns></returns>
    public int getTotalMaxHealth()
    {
        return getTotalCon() * 10;
    }
    /// <summary>
    /// Character stats + item bonus
    /// </summary>
    /// <returns></returns>
    public int getTotalMaxMana()
    {
        return (getTotalWis() + getTotalInt()) * 5;
    }
    /*public string getTraitByKey(string key)
    {
        string trait = null;
        foreach (KeyValuePair<string, string> traits in stringData)
        {
            if (traits.Key == key)
            {
               trait = traits.Value;
                return trait;
            }
        }
        Debug.LogError("Failed to load " + key + " from GameData");
        return trait;
    }*/

}
