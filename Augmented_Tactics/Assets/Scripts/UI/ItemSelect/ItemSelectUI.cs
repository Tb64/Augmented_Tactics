using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{

    public Image newImg;
    public Text newText1;
    public Text newText2;
    public Image[] inventoryImg;

    public Button Confirm;
    public Button BackPage;
    public Button NextPage;

    public Sprite nullImage;

    public PlayerData pdata;
    private List<UsableItem> items;
    private int currentlySelected = -1;
    private GameData gdata;


    // Use this for initialization
    void Start()
    {
        gdata = GameDataController.loadPlayerData();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadInventory()
    {
        InventoryReset();
        items = new List<UsableItem>();
        int index = 0;
        foreach (UsableItem item in gdata.usableItems)
        {

            index++;
        }

    }

    private void InventoryReset()
    {

    }
}
