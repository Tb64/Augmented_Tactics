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
        Vector3 iconPlacement = new Vector3(40f, -30f, 0f);
        for (int index = 0; index < inventorySize; index++)
        {
            inventoryArray[0, index] = Instantiate(item);
            inventoryArray[0, index].transform.parent = invTransform;
            inventoryArray[0, index].transform.position = new Vector3(0f, -30f, 0f);
            inventoryArray[0, index].transform.position = iconPlacement + new Vector3(50f, 0f, 0f);
            
            iconPlacement += new Vector3(50f, 0f, 0f);
        }
       
        
    }

}
