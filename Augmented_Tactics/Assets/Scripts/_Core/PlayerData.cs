using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public PlayerData(string name)
    {
        playerName = name;
    }
    private string playerName;
    public void setPlayerName(string name) { playerName = name; }
    public string getPlayerName() { return playerName; }

    private KeyValuePair<string, float>[] numberData = {new KeyValuePair<string, float>("Health", 0),
    new KeyValuePair<string, float>("Mana", 0), new KeyValuePair<string, float>("Experience", 0),
    new KeyValuePair<string, float>("Level",0 ),new KeyValuePair<string, float>("Strength",0),
    new KeyValuePair<string, float>("Dexterity",0), new KeyValuePair<string, float>("Constitution",0),
    new KeyValuePair<string, float>("Intelligence",0), new KeyValuePair<string, float>("Wisdom",0),
    new KeyValuePair<string, float>("Charisma",0), new KeyValuePair<string, float>("Stealth",0),
    new KeyValuePair<string, float>("Armor Class",0), new KeyValuePair<string, float>("Speed",0),
    new KeyValuePair<string, float>("Skill Points",0), new KeyValuePair<string, float>("Class", 0),
    new KeyValuePair<string, float>("Player Level",0)};
    
    //private KeyValuePair<string, string>[] stringData;

    public KeyValuePair<string, float>[] getNumberData(){return numberData;}
    //public KeyValuePair<string, string>[] getStringData() {return stringData; }
   
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
