using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClasses : MonoBehaviour {

    public const int PaladinKey = 0;
    public const int DarkKnightKey = 1;
    public const int BrawlerKey = 2;
    public const int ThiefKey = 3;
    public const int MageKey = 4;
    public const int ClericKey = 5;

    public static string[] classNames =
    {
        "Paladin",
        "Dark Knight",
        "Brawler",
        "Thief",
        "Mage",
        "Cleric"
    };

    public static string[] PrefabPath =
    {
        "", //Paladin
        "", //Darkknight
        "PlayerClasses/Brawler", //brawler
        "PlayerClasses/Thief",  //thief
        "", //Mage
        "PlayerClasses/Cleric" //cleric
    };

    public static string[] IconPath =
    {
        "", //Paladin
        "", //Darkknight
        "", //brawler
        "",  //thief
        "", //Mage
        ""  //cleric
    };

    public static string KeyToString(int key)
    {
        switch (key)
        {
            case PaladinKey:
                return "Paladin";

            case DarkKnightKey:
                return "Dark Knight";

            case ThiefKey:
                return "Thief";

            case BrawlerKey:
                return "Brawler";
                
            case MageKey:
                return "Mage";

            case ClericKey:
                return "Cleric";

            default:
                break;
        }
        return "";
    }

    public static int StringToKey(string className)
    {
        for (int index = 0; index < classNames.Length; index++)
        {
            if (className.ToLower() == classNames[index].ToLower())
                return index;
        }
        return -1;
    }
    
}
