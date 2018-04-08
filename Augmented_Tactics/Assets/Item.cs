using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	private bool slotOccupied;
    public Sprite inventoryIcon;
    GameObject item;

    void start()
    {
        slotOccupied = false;
    }

    public GameObject getItem()
    {
        return item;
    }
    public bool isOccupied()
    {
        return slotOccupied;
    }

}
