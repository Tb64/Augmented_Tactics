using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour {

	public static UsableItem LoadItem(string itemID)
    {
        itemID = itemID.ToLower();
        switch (itemID)
        {
            case "ataymirror":
                return new AtayMirror();
            case "destinybinder":
                return new DestinyBnider();
            case "doubleedgedsword":
                return new DoublleEdgedSword();
            case "heavyshield":
                return new HeavyShield();
            case "largemanatonic":
                return new LargeManaTonic();
            case "medmanatonic":
                return new MedManaTonic();
            case "smallmanatonic":
                return new SmallManaTonic();
            case "lottomanatonic":
                return new LottoManaTonic();
            case "largepotion":
                return new LargePotion();
            case "medpotion":
                return new MedPotion();
            case "smallpotion":
                return new SmallPotion();
            case "lottopotion":
                return new LottoPotion();
            case "panacea":
                return new Panacea();
            case "unguent":
                return new Unguent();
            default:
                Debug.LogError(itemID + " not in item list above^^^");
                return null;
        }
    }

    public static string[] ItemKeys =
    {
        "ataymirror",
        "destinybinder",
        "doubleedgedsword",
        "heavyshield",
        "largemanatonic",
        "largepotion",
        "medpotion",
        "smallpotion",
        "lottopotion",
        "panacea",
        "unguent"
    };
}
