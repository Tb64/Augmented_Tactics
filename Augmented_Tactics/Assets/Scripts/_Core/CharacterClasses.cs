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

    
}
