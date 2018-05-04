using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour {

    public ItemDetails details;

    public ItemInventory inventory;

    public Button Confirm;
    public Button BackPage;
    public Button NextPage;

    public PlayerData pdata;
    public GameData gdata;

    private int currentlySelected = -1;
    public Sprite nullImage;

    public List<UsableItem> items;


    // Use this for initialization
    void Start () {
        gdata = GameDataController.loadPlayerData();
        items = new List<UsableItem>();
        UsableItem potion = ItemLoader.LoadItem("smallpotion");
        potion.InitInitialize();
        items.Add(potion);
        for (int index = 0; index < 5; index++)
        {
            UsableItem newitem = UsableItemGen.RandomItem();
            Debug.Log("Adding " + newitem.name);
            items.Add(newitem);
        }

        inventory.UpdateInventory(items);
        UpdateDetails(0);
	}

    public void UpdateInventory()
    {
        
    }

    public void UpdateDetails(int index)
    {
        currentlySelected = index;
        if (index >= 0 || index <= items.Count)
            details.LoadItem(items[index]);
        else
            Debug.Log("Index is out of range");
    }

    public void BuyCurrentItem()
    {
        if (currentlySelected < 0 || currentlySelected >= items.Count)
        {
            Debug.Log("Index is out of range");
            return;
        }

        GameDataController.loadPlayerData();

        GameDataController.gameData.usableItems.Add(items[currentlySelected]);

        GameDataController.savePlayerData();
    }
}
