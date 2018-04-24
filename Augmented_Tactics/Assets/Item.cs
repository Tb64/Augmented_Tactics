using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour {

	private bool slotOccupied;
    public Sprite inventoryIcon;
    public Equipable equipItem;
    public Armor armor;
    public Weapons weapon;
    public UsableItem useItem;
    public Image storeImage;
    public GameObject playerInventory;
    public GameObject store;
    
    string itemType;

    void start()
    {
        store = GameObject.Find("StoreUI");
        if(store == null)
        {
            Debug.Log("store is null");
        }
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




    public void setInventory(GameObject obj)
    {
        playerInventory = obj;
    }

    public void setEquipable(Armor item)
    {
        armor = item;
        setEquipable((Equipable)item);
    }

    public void setEquipable(Weapons item)
    {
        weapon = item;
        setEquipable((Equipable)item);
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

    public Armor getArmor()
    {
        return armor;
    }

    public Weapons getWeapon()
    {
        return weapon;
    }

    public Equipable getEquipable()
    {
        return equipItem;
    }

    public bool isOccupied()
    {
        return slotOccupied;
    }

    public void checkParent()
    {
        string parentName = transform.parent.name;
        store.GetComponent<Store>().setSelectedItem(this);

        switch (parentName)
        {
            case "InventoryBackground":
                break;
            case "StoreBackground":
                displayStore();
                break;
        }

    }

    public void displayStore()
    {
        if(store != null)
        {
            Debug.Log("TEST");
            store.GetComponent<Store>().populateStore(this);
        }

        
    }

    public string getItemType()
    {
        return itemType;
    }

    public void setStore(GameObject obj)
    {
        store = obj;
    }


}
