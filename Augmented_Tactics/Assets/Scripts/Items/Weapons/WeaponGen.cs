using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGen : MonoBehaviour {

    private string FileLocation = "GameData/Weapon/weapons";

    public Weapons weapon;

    private string[] wData;
    private List<string[]> wList;

    private void Start()
    {
        WeaponGenerate(1, "Brawler", 1);
    }

    public void LoadData(int level, string characterClass)
    {
        //set weapon data
        wList = new List<string[]>();
        string[,] rawData;
        TextAsset dataFile = Resources.Load<TextAsset>(FileLocation);
        rawData = CSVReader.SplitCsvGrid(dataFile.text);

        for (int index = 0; index < rawData.GetLength(1) - 2; index++)
        {
            if(rawData[ItemKey.ClassType,index].ToLower() == characterClass.ToLower() && rawData[ItemKey.Level, index] == level.ToString())
            {
                string[] newData = CSVReader.GetRowFrom2DArray(index, rawData);
                wList.Add(newData);
            }
        }
    }

    public Weapons WeaponGenerate(int level, string characterClass, int rarity)
    {
        // search for weapon of this level and class
        weapon = new Weapons();
        LoadData(level, characterClass);

        //random selection of weapon
        int randomNum = Random.Range(0, wList.Count);

        wData = wList[randomNum];

        //weapon.name = GetName();
        randomGenDamage();
        SetWeaponData();
        //for (int index = 0; index < rarity; index++)
        //{
        //    randomStatBoost();
        //}

        //DebugPrint(weapon);
        weapon.rarity = rarity;
        for (int index = 0; index < rarity; index++)
        {
            randomStatBoost();
        }

        return weapon;
    }

    public void randomGenDamage()
    {
        int pDmgMin = int.Parse(wData[ItemKey.PhysicalDamageMin]);
        int pDmgMax = int.Parse(wData[ItemKey.PhysicalDamageMax]);

        int dmg1 = (int)Random.Range(pDmgMin, pDmgMax + 1f);
        int dmg2 = (int)Random.Range(pDmgMin, pDmgMax + 1f);
        while(dmg1 == dmg2)
        {
            dmg2 = (int)Random.Range(pDmgMin, pDmgMax + 1f);
        }

        if(dmg1 < dmg2)
        {
            weapon.physical_dmg_max = dmg2;
            weapon.physical_dmg_min = dmg1;
        }
        else
        {
            weapon.physical_dmg_max = dmg1;
            weapon.physical_dmg_min = dmg2;
        }

        int mDmgMin = int.Parse(wData[ItemKey.MagicDamageMin]);
        int mDmgMax = int.Parse(wData[ItemKey.MagicDamageMax]);

        dmg1 = (int)Random.Range(mDmgMin, mDmgMax + 1f);
        dmg2 = (int)Random.Range(mDmgMin, mDmgMax + 1f);
        while (dmg1 == dmg2)
        {
            dmg2 = (int)Random.Range(mDmgMin, mDmgMax + 1f);
        }

        if (dmg1 < dmg2)
        {
            weapon.magic_dmg_max = dmg2;
            weapon.magic_dmg_min = dmg1;
        }
        else
        {
            weapon.magic_dmg_max = dmg1;
            weapon.magic_dmg_min = dmg2;
        }
    }

    public void randomStatBoost()
    {
        // get value of bonus
        int selected = Random.Range(0, 7 + 1);
        int bonus = 0;

        switch (selected)
        {
            case 0:     //str
                bonus = int.Parse(wData[ItemKey.StrBonus]) + 1;
                weapon.str_bonus = (int)Random.Range(1, bonus);
                break;

            case 1:     //dex
                bonus = int.Parse(wData[ItemKey.DexBonus]) + 1;
                weapon.dex_bonus = (int)Random.Range(1, bonus);
                break;

            case 2:     //con
                bonus = int.Parse(wData[ItemKey.ConBonus]) + 1; ;
                weapon.con_bonus = (int)Random.Range(1, bonus);
                break;

            case 3:     //wis
                bonus = int.Parse(wData[ItemKey.WisBonus]) + 1;
                weapon.wis_bonus = (int)Random.Range(1, bonus);
                break;

            case 4:     //int
                bonus = int.Parse(wData[ItemKey.IntBonus]) + 1;
                weapon.int_bonus = (int)Random.Range(1, bonus);
                break;

            case 5:     //eva
                bonus = int.Parse(wData[ItemKey.EvaBonus]) + 1;
                weapon.eva_bonus = (int)Random.Range(1, bonus);
                break;

            case 6:     //acc
                bonus = int.Parse(wData[ItemKey.AccBonus]) + 1;
                weapon.str_bonus = (int)Random.Range(1, bonus);
                break;

            case 7:     //crit
                bonus = int.Parse(wData[ItemKey.CritBonus]) + 1;
                weapon.str_bonus = (int)Random.Range(1, bonus);
                break;

            default:
                break;
        }

    }

    private void SetWeaponData()
    {
        weapon.name = wData[ItemKey.Name];
        weapon.class_req = wData[ItemKey.ClassType];
        weapon.type = wData[ItemKey.Type];
        weapon.image = wData[ItemKey.IconPath];
        weapon.model = wData[ItemKey.ModelPath];
        weapon.slot = "Weapon";

        weapon.cost = int.Parse(wData[ItemKey.Cost]);
        weapon.level_req = int.Parse(wData[ItemKey.Level]);
    }

    private void DebugPrint(Weapons weap)
    {
        string weaponData = "";

        foreach (string str in wData)
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
