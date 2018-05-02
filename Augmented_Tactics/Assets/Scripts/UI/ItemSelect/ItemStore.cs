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
        items = new List<UsableItem>();
        for (int index = 0; index < 5; index++)
        {
            UsableItem newitem = UsableItemGen.RandomItem();
            Debug.Log("Adding " + newitem.name);
            items.Add(newitem);
        }

        inventory.UpdateInventory(items);
	}

    public void UpdateInventory()
    {
        
    }

    public void UpdateDetails(int index)
    {
        currentlySelected = index;
        details.LoadItem(items[index]);
    }
}
