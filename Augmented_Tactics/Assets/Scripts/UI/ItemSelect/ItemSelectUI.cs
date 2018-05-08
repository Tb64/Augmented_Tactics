using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{
    public ItemInventory inventory;
    public ItemDetails details;
    //public Button Confirm;
    //public Button BackPage;
    //public Button NextPage;

    public Image[] currentEquiped;

    public Sprite nullImage;

    public PlayerData pdata;
    private List<UsableItem> items;
    private int currentlySelected = -1;
    private GameData gdata;


    // Use this for initialization
    void Start()
    {
        gdata = GameDataController.loadPlayerData();
        inventory.UpdateInventory(gdata.usableItems);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadInventory(PlayerData data)
    {
        InventoryReset();
        gdata = GameDataController.loadPlayerData();
        inventory.UpdateInventory(gdata.usableItems);

        foreach (Image img in currentEquiped)
        {

        }

    }

    private void InventoryReset()
    {

    }
}
