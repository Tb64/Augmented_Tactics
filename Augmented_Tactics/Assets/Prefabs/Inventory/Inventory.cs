using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    GameObject inventory;
    Transform invTransform;
    private void Start()
    {

        inventory = GameObject.Find("Inventory");
        invTransform = inventory.transform;
    }

    void updateInventory()
    {
        //GameObject icon = Instantiate(Resources.Load("Prefabs/item"), invTransform);
        
    }

}
