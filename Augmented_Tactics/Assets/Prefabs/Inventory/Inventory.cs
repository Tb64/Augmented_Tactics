using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    GameObject inventory;
    public GameObject inventoryHead;
    GameObject item;
    Transform invTransform;
    GameObject[,] inventoryArray = new GameObject[5, 5];
    GameObject backgroundImage;
    GameObject inventoryBackground;
    private float inventorySize;
    //test
    ArmorGen armorgen;
    Armor armor;

    private void Start()
    {
        inventorySize = 25;
        item = Resources.Load<GameObject>("Prefabs/Item");
        inventoryBackground = GameObject.Find("InventoryBackground");
        inventory = GameObject.Find("Inventory");
        inventoryHead = GameObject.Find("Inventory");
        invTransform = inventory.GetComponent<Transform>();
        updateInventory();
        //armorgen = new ArmorGen();
        armor = ArmorGen.ArmorGenerate(1, "Brawler", 1);
        addEquipable(armor);
    }

    public void addEquipable(Equipable item)
    {

        for (int index = 0; index < 5; index++)
        {
            Debug.Log("index:" + index);
            for (int jindex = 0; jindex < 5; jindex++)
            {
                Debug.Log("jindex:" + jindex);
                if (inventoryArray[index, jindex].GetComponent<Item>().isOccupied() == false)
                {
                    inventoryArray[index, jindex].GetComponent<Item>().setEquipable(item);
                    return;
                }
            }
        }
        updateInventory();
    }

    public void addUsable(UsableItem item)
    {
        for (int index = 0; index < 5; index++)
        {
            Debug.Log("index:" + index);
            for (int jindex = 0; jindex < 5; jindex++)
            {
                Debug.Log("jindex:" + jindex);
                if (inventoryArray[index, jindex].GetComponent<Item>().isOccupied() == false)
                {
                    inventoryArray[index, jindex].GetComponent<Item>().setUsable(item);
                    return;
                }
            }
        }
    }

    public void updateInventory()
    {
        if (item == null)
        {
            Debug.Log("ITEM NOT FOUND");
            return;
        }
        GameDataController.loadPlayerData();

        inventoryArray[0, 0] = Instantiate(item);
        inventoryArray[0, 0].transform.SetParent(inventoryBackground.transform, false);
        float initialX = inventoryArray[0, 0].transform.localPosition.x;
        float initialY = inventoryArray[0, 0].transform.localPosition.y;
        inventoryArray[0, 0].transform.localPosition = new Vector3(initialX + 25f, initialY - 30f, 0);
        Vector3 iconPlacement = inventoryArray[0, 0].transform.localPosition;
        //iconPlacement += new Vector3(50f, 0f, 0f);

        float inventoryRows = (inventorySize / 5f);
        double numRows = Math.Ceiling(inventoryRows);
        int numItems = 5;
        float inventoryCounter = inventorySize;
        int numOfWeapons = GameDataController.gameData.weapons.Count;
        int numOfArmors = GameDataController.gameData.armors.Count;
        int numOfUsables = GameDataController.gameData.usableItems.Count;


        for (int index = 0; index < numRows; index++)
        {
            if (inventoryCounter >= 5)
                numItems = 5;
            else
                numItems = (int)inventoryCounter;

            for (int jindex = 0; jindex < numItems; jindex++)
            {
                inventoryArray[index, jindex] = Instantiate(item);
                inventoryArray[index, jindex].transform.SetParent(inventoryBackground.transform, false);
                inventoryArray[index, jindex].transform.localPosition = iconPlacement;
                iconPlacement += new Vector3(70f, 0f, 0f);
                inventoryCounter--;
            }
            iconPlacement.x = inventoryArray[0, 0].transform.localPosition.x;
            iconPlacement += new Vector3(0, -80f, 0);
        }

        int gameDataIndex = 0;


        //foreach(WeaponGen in)

     

        //loads inventory from gamedata
        for (int index = 0; index < 5; index++)
        {
            for (int jindex = 0; jindex < 5; jindex++)
            {

                inventoryArray[index, jindex].GetComponent<Item>().setEquipable(GameDataController.gameData.weapons[gameDataIndex]);
            }
        }

    }

    public void findNextEmpty()
    {

    }

    public void toggleInventory()
    {
        if (inventoryHead.transform.GetChild(0).gameObject.activeSelf == true)
            inventoryHead.transform.GetChild(0).gameObject.SetActive(false);
        else
            inventoryHead.transform.GetChild(0).gameObject.SetActive(true);

    }

    void loadGameData()
    {
        //will load data and place into inventory
        GameDataController.loadPlayerData();

    }
}