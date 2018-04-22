using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorGen : MonoBehaviour {


    private static string FileLocation = "GameData/Armor/armor";

    //public Armor armor;
    public Armor publicArmor;
    //private static string[] aData;
    //private static List<string[]> aList;

    private void Start()
    {
        publicArmor = ArmorGenerate(1, "Brawler", 1);
    }

    public static List<string[]> LoadData(int level, string characterClass)
    {
        //set weapon data
        List<string[]> aList = new List<string[]>();
        string[,] rawData;
        TextAsset dataFile = Resources.Load<TextAsset>(FileLocation);
        rawData = CSVReader.SplitCsvGrid(dataFile.text);

        for (int index = 0; index < rawData.GetLength(1) - 2; index++)
        {
            if (rawData[ItemKey.Armor.ClassType, index].ToLower() == characterClass.ToLower() && rawData[ItemKey.Armor.Level, index] == level.ToString())
            {
                string[] newData = CSVReader.GetRowFrom2DArray(index, rawData);
                aList.Add(newData);
            }
        }

        return aList;
    }

    /// <summary>
    /// Generates a random Armor based off CSV file data
    /// </summary>
    /// <param name="level">The level you want to make the Armor</param>
    /// <param name="characterClass">The Armor's class</param>
    /// <param name="rarity">How many random stat boosts</param>
    /// <returns></returns>
    public static Armor ArmorGenerate(int level, string characterClass, int rarity)
    {
        // search for weapon of this level and class
        Armor armor = new Armor();
        List<string[]> aList = LoadData(level, characterClass);

        //random selection of weapon
        int randomNum = Random.Range(0, aList.Count);
        if (aList.Count == 0)
            Debug.Log("No data found for Armor");
        string[] aData = aList[randomNum];

        //weapon.name = GetName();
        randomDef(armor, aData);
        SetWeaponData(armor, aData);

        //DebugPrint(weapon);
        armor.rarity = rarity;
        for (int index = 0; index < rarity; index++)
        {
            randomStatBoost(armor, aData);
        }

        armor.cost += (int)(armor.cost * rarity * 0.25);

        return armor;
    }

    public static Armor ArmorGenerate(int level, int characterClass, int rarity)
    {
        return ArmorGenerate(level, CharacterClasses.KeyToString(characterClass), rarity);
    }


    public static void randomDef(Armor armor, string[] aData)
    {
        int pDmgMin = int.Parse(aData[ItemKey.Armor.PhysicalDefenseMin]);
        int pDmgMax = int.Parse(aData[ItemKey.Armor.PhysicalDefenseMax]);

        int pdef = (int)Random.Range(pDmgMin, pDmgMax + 1f);

        armor.physical_def = pdef;

        int mDmgMin = int.Parse(aData[ItemKey.Armor.MagicDefenseMin]);
        int mDmgMax = int.Parse(aData[ItemKey.Armor.MagicDefenseMax]);

        int mdef = (int)Random.Range(mDmgMin, mDmgMax + 1f);

        armor.magic_def = mdef;

        armor.armor_class = pdef + mdef;

    }

    public static void randomStatBoost(Armor armor, string[] aData)
    {
        // get value of bonus
        int selected = Random.Range(0, 4 + 1);
        int bonus = 0;

        switch (selected)
        {
            case 0:     //str
                bonus = int.Parse(aData[ItemKey.Armor.StrBonus]) + 1;
                armor.str_bonus += (int)Random.Range(1, bonus);
                break;

            case 1:     //dex
                bonus = int.Parse(aData[ItemKey.Armor.DexBonus]) + 1;
                armor.dex_bonus += (int)Random.Range(1, bonus);
                break;

            case 2:     //con
                bonus = int.Parse(aData[ItemKey.Armor.ConBonus]) + 1;
                armor.con_bonus += (int)Random.Range(1, bonus);
                break;

            case 3:     //wis
                bonus = int.Parse(aData[ItemKey.Armor.WisBonus]) + 1;
                armor.wis_bonus += (int)Random.Range(1, bonus);
                break;

            case 4:     //int
                bonus = int.Parse(aData[ItemKey.Armor.IntBonus]) + 1;
                armor.int_bonus += (int)Random.Range(1, bonus);
                break;

            case 5:     //eva
                bonus = int.Parse(aData[ItemKey.Armor.EvaBonus]) + 1;
                armor.eva_bonus = (int)Random.Range(1, bonus);
                break;

            case 6:     //acc
                bonus = int.Parse(aData[ItemKey.Armor.AccBonus]) + 1;
                armor.acc_bonus = (int)Random.Range(1, bonus);
                break;

            case 7:     //crit
                bonus = int.Parse(aData[ItemKey.Armor.CritBonus]) + 1;
                armor.crit_chance = (int)Random.Range(1, bonus);
                break;

            default:
                break;
        }

    }

    private static void SetWeaponData(Armor armor, string[] aData)
    {
        armor.name = aData[ItemKey.Armor.Name];
        armor.class_req = aData[ItemKey.Armor.ClassType];
        armor.type = aData[ItemKey.Armor.Type];
        armor.image = aData[ItemKey.Armor.IconPath];
        armor.model = aData[ItemKey.Armor.ModelPath];
        armor.slot = "Armor";

        armor.cost = int.Parse(aData[ItemKey.Armor.Cost]);
        armor.level_req = int.Parse(aData[ItemKey.Armor.Level]);
    }

    private void DebugPrint(Weapons weap, string[] aData)
    {
        string weaponData = "";

        foreach (string str in aData)
        {
            weaponData += str + " ";
        }

        Debug.Log(weaponData);

        string output;
        output = "Name: " + weap.name;
        Debug.Log(output);

        output = "Class: " + weap.class_req;
        Debug.Log(output);

        output = "Type: " + weap.type;
        Debug.Log(output);

        output = "Level: " + weap.level_req;
        Debug.Log(output);

        output = "pDmg: " + weap.physical_dmg_min + "-" + weap.physical_dmg_max;
        Debug.Log(output);

        output = "mDmg: " + weap.magic_dmg_min + "-" + weap.magic_dmg_max;
        Debug.Log(output);
    }
}
