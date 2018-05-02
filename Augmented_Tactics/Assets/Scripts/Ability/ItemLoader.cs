using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour {

	public static UsableItem LoadItem(string itemID)
    {
        itemID = itemID.ToLower();
        UsableItem item = new UsableItem();
        switch (itemID)
        {
            case "ataymirror":
                item = new AtayMirror();
                break;
            case "destinybinder":
                item = new DestinyBnider();
                break;
            case "doubleedgedsword":
                item = new DoublleEdgedSword();
                break;
            case "heavyshield":
                item = new HeavyShield();
                break;
            case "largemanatonic":
                item = new LargeManaTonic();
                break;
            case "medmanatonic":
                item = new MedManaTonic();
                break;
            case "smallmanatonic":
                item = new SmallManaTonic();
                break;
            case "lottomanatonic":
                item = new LottoManaTonic();
                break;
            case "largepotion":
                item = new LargePotion();
                break;
            case "medpotion":
                item = new MedPotion();
                break;
            case "smallpotion":
                item = new SmallPotion();
                break;
            case "lottopotion":
                item = new LottoPotion();
                break;
            case "panacea":
                item = new Panacea();
                break;
            case "unguent":
                item = new Unguent();
                break;
            default:
                Debug.LogError(itemID + " not in item list above^^^");
                return null;
        }
        item.itemKey = itemID;
        return item;
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
