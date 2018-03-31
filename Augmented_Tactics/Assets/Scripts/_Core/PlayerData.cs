using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public PlayerData(string name)
    {
        playerName = name;
        unlocked = false;
    }
    private bool unlocked;
    public void unlockPlayer() { unlocked = true; }
    public string playerName;
    public void setPlayerName(string name) { playerName = name; }
    public string getPlayerName() { return playerName; }
    public int cost;

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

    public float getStatByKey(string key)
    {
        float stat = -1;
        foreach(KeyValuePair<string, float> stats in numberData)
        {
            if(stats.Key == key)
            {
                stat = stats.Value;
                return stat;
            }
        }
        Debug.LogError("Failed to load " + key + " from GameData");
        return stat;
    }

    public string getStringByKey(string key)
    {
        string stat = "";
        foreach (KeyValuePair<string, string> stats in stringData)
        {
            if (stats.Key == key)
            {
                stat = stats.Value;
                return stat;
            }
        }
        Debug.LogError("Failed to load " + key + " from GameData");
        return stat;
    }

    public void setStatbyKey(string key, float stat)
    {
        int statIndex = 0;
        foreach (KeyValuePair<string, float> stats in numberData)
        {
            if (stats.Key == key)
            {
                numberData[statIndex] = new KeyValuePair<string, float>(key, stat);
                return;
            }
            statIndex++;
        }
        Debug.LogError("Unable to save " + key + " stat");
    }

    public void setStatbyKey(string key, string stat)
    {
        int statIndex = 0;
        foreach (KeyValuePair<string, string> stats in stringData)
        {
            if (stats.Key == key)
            {
                stringData[statIndex] = new KeyValuePair<string, string>(key, stat);
                return;
            }
            statIndex++;
        }
        Debug.LogError("Unable to save " + key + " stat");
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
