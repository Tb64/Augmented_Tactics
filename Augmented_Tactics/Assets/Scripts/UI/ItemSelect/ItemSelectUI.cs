using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{
    public ItemInventory inventory;
    public ItemDetails details;
    public Button Move;
    //public Button BackPage;
    //public Button NextPage;

    public Image[] currentEquiped;

    public Sprite nullImage;
    public Sprite redButtonImage;
    public Sprite greenButtonImage;

    public PlayerData pdata;
    private List<UsableItem> items;
    private int currentlySelected = -1;
    private GameData gdata;
    private int onHandItemCount;

    // Use this for initialization
    void Start()
    {
        onHandItemCount = 0;
        gdata = GameDataController.loadPlayerData();
        pdata = gdata.armyList[0];
        LoadInventory(pdata);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadInventory(PlayerData data)
    {
        InventoryReset();
        gdata = GameDataController.loadPlayerData();
        items = gdata.usableItems;
        inventory.UpdateInventory(items);
        
        if (data.Item1 != "") currentEquiped[onHandItemCount++].sprite = Resources.Load<Sprite>(data.Item1);
        if (data.Item2 != "") currentEquiped[onHandItemCount++].sprite = Resources.Load<Sprite>(data.Item2);
        if (data.Item3 != "") currentEquiped[onHandItemCount++].sprite = Resources.Load<Sprite>(data.Item3);
        if (data.Item4 != "") currentEquiped[onHandItemCount++].sprite = Resources.Load<Sprite>(data.Item4);
        if (onHandItemCount > 3)
            Move.interactable = false;
    }

    public void UpdateDetails(int input)
    {
        //0-24 will belong to item in the inventory
        //25-28 will be the items in the players on hand inventory
        //if the current selected is <25 then show the green equip button, but disable if there are no empty item spots on the character 
        //if the current selected is >=25 then show the red unequip button, unless the string is empty
        currentlySelected = input;
        Move.interactable = true;

        if (currentlySelected < 25)
        {
            Move.GetComponentInChildren<Text>().text = "Equip";
            if (items.Count > currentlySelected && items[currentlySelected] != null)
            {
                details.LoadItem(items[currentlySelected]);                
                if (onHandItemCount > 3)
                    Move.interactable = false;
            }
            else
                Move.interactable = false;
        }
        else
        {
            Move.GetComponentInChildren<Text>().text = "Unequip";
            Move.GetComponentInChildren<Image>().sprite = redButtonImage;

            if (currentlySelected == 25)
            {
                if (pdata.Item1 != "") details.LoadItem(ItemLoader.LoadItem(pdata.Item1));
                else Move.interactable = false;
            }
            else if (currentlySelected == 26)
            {
                if (pdata.Item2 != "") details.LoadItem(ItemLoader.LoadItem(pdata.Item2));
                else Move.interactable = false;
            }
            else if (currentlySelected == 27)
            {
                if (pdata.Item3 != "") details.LoadItem(ItemLoader.LoadItem(pdata.Item3));
                else Move.interactable = false;
            }
            else if (currentlySelected == 28)
            {
                if (pdata.Item4 != "") details.LoadItem(ItemLoader.LoadItem(pdata.Item4));
                else Move.interactable = false;
            }
        }
    }



    public void MoveSelected()
    {
        //now i have to make a method that when its less than 25 it moves the item to an open slot
        // keep in mind it is possible for slot 3 to be open and none else, will have to search for an open slot

        //<25 are items in inventory. Moves to a free slot for the player. Remove from items list
        if (currentlySelected < 25)
        {
            if (onHandItemCount < 4) {
                if (pdata.Item1 == "") pdata.Item1 = items[currentlySelected].itemKey;
                else if (pdata.Item2 == "") pdata.Item2 = items[currentlySelected].itemKey;
                else if (pdata.Item3 == "") pdata.Item3 = items[currentlySelected].itemKey;
                else if (pdata.Item4 == "") pdata.Item4 = items[currentlySelected].itemKey;
                Debug.Log("Added " + items[currentlySelected].itemKey);
                items.Remove(items[currentlySelected]);
            }
            //TODO RESET UI
        }
        else //Else the button removes something from the players on hand
        {
            if (currentlySelected == 25)
            {
                items.Add(ItemLoader.LoadItem(pdata.Item1));
                pdata.Item1 = "";
            }
            else if (currentlySelected == 26)
            {
                items.Add(ItemLoader.LoadItem(pdata.Item2));
                pdata.Item2 = "";
            }
            else if (currentlySelected == 27)
            {
                items.Add(ItemLoader.LoadItem(pdata.Item3));
                pdata.Item3 = "";
            }
            else if (currentlySelected == 28)
            {
                items.Add(ItemLoader.LoadItem(pdata.Item4));
                pdata.Item4 = "";
            }
        }
        GameDataController.savePlayerData(gdata);
        /*if(gdata.)
        data.Item1 = items[currentlySelected].itemKey;*/
    }

    private void InventoryReset()
    {
        inventory.ResetUI();
    }


}
