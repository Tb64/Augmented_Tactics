using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    GameObject inventory;
    GameObject item;
    Transform invTransform;
    GameObject[,] inventoryArray = new GameObject[7, 7];
    private float inventorySize;

    private void Start()
    {
        inventorySize = 39;
        item = Resources.Load<GameObject>("Prefabs/Item");
       
        inventory = GameObject.Find("Inventory");
        invTransform = inventory.GetComponent<Transform>();

    }
    

    public void addItem()
    {
        //will iterate through array until it finds an open slot

    }

    public void updateInventory()
    {

        if (item == null)
        {
            Debug.Log("ITEM NOT FOUND");
            return;
        }
        
        inventoryArray[0, 0] = Instantiate(item);
        inventoryArray[0, 0].transform.SetParent(inventory.transform, false);
        Vector3 iconPlacement = inventoryArray[0, 0].transform.localPosition;
        //iconPlacement += new Vector3(50f, 0f, 0f);

        float inventoryRows = (inventorySize / 7f);
        double numRows = Math.Ceiling(inventoryRows);
        int numItems = 7;
        float inventoryCounter = inventorySize;

        for (int index = 0; index < numRows; index++)
        {
            if (inventoryCounter >=7)
                numItems = 7;
            else
                numItems = (int)inventoryCounter;
            for (int jindex = 0; jindex < numItems; jindex++)
            {
                inventoryArray[0, jindex] = Instantiate(item);
                inventoryArray[0, jindex].transform.SetParent(inventory.transform, false);
                inventoryArray[0, jindex].transform.localPosition = iconPlacement;
                iconPlacement += new Vector3(56.5f, 0f, 0f);
                inventoryCounter--;
            }
            iconPlacement.x = inventoryArray[0,0].transform.localPosition.x;
            iconPlacement += new Vector3(0, -60f, 0);
        }
       
        
    }

}
