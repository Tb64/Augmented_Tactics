using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour {

	private bool slotOccupied;
    public Sprite inventoryIcon;
    Equipable equipItem;
    UsableItem useItem;
    string itemType;

    void start()
    {
        slotOccupied = false;
    }

    void setItemType(string type)
    {
        itemType = type;
    }

    public void setEquipable(Equipable item)
    {
        equipItem = item;
        setItemType("Equipable");
        inventoryIcon = Resources.Load<Sprite>(item.image);

        if (inventoryIcon != null)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = inventoryIcon;
        }
        slotOccupied = true;
    }

    public void setUsable(UsableItem item)
    {
        useItem = item;
        setItemType("Usable");
        inventoryIcon = Resources.Load<Sprite>(item.image);

        if (inventoryIcon != null)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = inventoryIcon;
        }
        slotOccupied = true;
    }

    public UsableItem getUsable()
    {
        return useItem;
    }


    public Equipable getEquipable()
    {
        return equipItem;
    }

    public bool isOccupied()
    {
        return slotOccupied;
    }

}
