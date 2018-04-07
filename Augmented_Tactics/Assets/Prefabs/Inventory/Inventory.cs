using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    GameObject inventory;
    GameObject item;
    Transform invTransform;
    GameObject[,] inventoryArray = new GameObject[5, 5];
    private int inventorySize;

    private void Start()
    {
        inventorySize = 5;
        item = Resources.Load<GameObject>("Prefabs/Item");
       
        inventory = GameObject.Find("Inventory");
        invTransform = inventory.GetComponent<Transform>();

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
        iconPlacement += new Vector3(50f, 0f, 0f);

        for (int index = 0; index < 5; index++)
        {
            for (int jindex = 0; jindex < inventorySize; jindex++)
            {
                inventoryArray[0, jindex] = Instantiate(item);
                inventoryArray[0, jindex].transform.SetParent(inventory.transform, false);
                inventoryArray[0, jindex].transform.localPosition = iconPlacement;
                iconPlacement += new Vector3(50f, 0f, 0f);
            }
            iconPlacement.x = 0f;
            iconPlacement += new Vector3(0, -80f, 0);
        }
       
        
    }

}
