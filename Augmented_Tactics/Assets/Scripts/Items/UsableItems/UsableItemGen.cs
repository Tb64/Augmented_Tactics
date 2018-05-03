using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemGen : MonoBehaviour {

    public UsableItem item;

    public void Start()
    {
        item = RandomItem();
    }

    public static UsableItem RandomItem()
    {
        string key = RandomKey();
        Debug.Log("Generating item " + key);
        UsableItem newItem = ItemLoader.LoadItem(key);
        newItem.InitInitialize();
        return newItem;
    }

    public static string RandomKey()
    {
        return ItemLoader.ItemKeys[RandomItemIndex()];
    }

    private static int RandomItemIndex()
    {
        int max = ItemLoader.ItemKeys.Length;
        return (int)Random.Range(0, max);
    }
}
