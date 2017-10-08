using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public KeyValuePair<string, float>[] numberData = {new KeyValuePair<string, float>("Health", 0),
    new KeyValuePair<string, float>("Mana", 0), new KeyValuePair<string, float>("Experience", 0),
    new KeyValuePair<string, float>("Level",0 ),new KeyValuePair<string, float>("Strength",0),
    new KeyValuePair<string, float>("Dexterity",0), new KeyValuePair<string, float>("Constitution",0),
    new KeyValuePair<string, float>("Intelligence",0), new KeyValuePair<string, float>("Wisdom",0),
    new KeyValuePair<string, float>("Charisma",0), new KeyValuePair<string, float>("Acrobatics",0),
    new KeyValuePair<string, float>("Athletics",0), new KeyValuePair<string, float>("Stealth",0),
    new KeyValuePair<string, float>("Agility",0), new KeyValuePair<string, float>("Armor Class",0),
    new KeyValuePair<string, float>("Speed",0), new KeyValuePair<string, float>("Medicine",0)};

    public KeyValuePair<string, string>[] stringData = {new KeyValuePair<string, string>("Name", null),
    new KeyValuePair<string, string>("Class", null)};
}
