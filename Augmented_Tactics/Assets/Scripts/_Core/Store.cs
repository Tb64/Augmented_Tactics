using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Store : MonoBehaviour
{

    public int storeLevel;
    GameObject inventory;
    public GameObject inventoryHead;
    GameObject item;
    Transform invTransform;
    GameObject[,] inventoryArray = new GameObject[5, 5];
    GameObject backgroundImage;
    GameObject StoreBackground;
    //1 for armorer, 2 for weaponsmith, 3 for generalStore
    public int storeType;
    public GameObject inventoryActual; // variables need renaming - quick fix
    public Image storeImage;
    public Item selectedItem;
    public Text storeText;
    private float inventorySize;
    //test
    ArmorGen armorgen;
    Armor weapon;

    private void Start()
    {
        storeLevel = 3;
        inventorySize = 10;
        item = Resources.Load<GameObject>("Prefabs/Item");
        StoreBackground = GameObject.Find("StoreBackground");
        inventory = GameObject.Find("StoreUI");
        inventoryHead = GameObject.Find("Store");
        invTransform = inventory.GetComponent<Transform>();
        updateStore();
        armorgen = new ArmorGen();
        weapon = ArmorGen.ArmorGenerate(1, "Brawler", 1);
        addEquipable(weapon);
        storeType = 1;
       
    }

    public void addEquipable(Equipable item)
    {
        for (int index = 0; index < 2; index++)
        {
            for (int jindex = 0; jindex < 5; jindex++)
            {
                if (inventoryArray[index, jindex].GetComponent<Item>().isOccupied() == false)
                {
                    inventoryArray[index, jindex].GetComponent<Item>().setEquipable(item);
                    return;
                }
            }
        }
    }

    public void addUsable(UsableItem item)
    {
        int index = 0;
        int jindex = 0;

        while (inventoryArray[index, jindex].GetComponent<Item>().isOccupied() != false)
        {


        }
    }

    public void updateStore()
    {

        if (item == null)
        {
            Debug.Log("ITEM NOT FOUND");
            return;
        }

        inventoryArray[0, 0] = Instantiate(item);
        inventoryArray[0, 0].transform.SetParent(StoreBackground.transform, false);
        float initialX = inventoryArray[0, 0].transform.localPosition.x;
        float initialY = inventoryArray[0, 0].transform.localPosition.y;
        inventoryArray[0, 0].transform.localPosition = new Vector3(initialX + 25f, initialY - 250f, 0);
        Vector3 iconPlacement = inventoryArray[0, 0].transform.localPosition;
        //iconPlacement += new Vector3(50f, 0f, 0f);

        float inventoryRows = (inventorySize / 5f);
        double numRows = Math.Ceiling(inventoryRows);
        int numItems = 5;
        float inventoryCounter = inventorySize;

        for (int index = 0; index < numRows; index++)
        {
            if (inventoryCounter >= 5)
                numItems = 5;
            else
                numItems = (int)inventoryCounter;

            for (int jindex = 0; jindex < numItems; jindex++)
            {
                inventoryArray[index, jindex] = Instantiate(item);
                inventoryArray[index, jindex].transform.SetParent(StoreBackground.transform, false);
                inventoryArray[index, jindex].transform.localPosition = iconPlacement;
                inventoryArray[index, jindex].GetComponent<Item>().setStore(gameObject);
                inventoryArray[index, jindex].GetComponent<Item>().setInventory(inventoryActual);
                iconPlacement += new Vector3(70f, 0f, 0f);
               

                switch (storeType)
                {
                    case 1:   //Armorer
                        inventoryArray[index, jindex].GetComponent<Item>().setEquipable(generateArmor());
                        break;
                    case 2:   //Weaponsmith
                        inventoryArray[index, jindex].GetComponent<Item>().setEquipable(generateWeapon());
                        break;
                    case 3:
                        break;
                }

                inventoryCounter--;
            }
            iconPlacement.x = inventoryArray[0, 0].transform.localPosition.x;
            iconPlacement += new Vector3(0, -80f, 0);
        }
    }

    public void toggleInventory()
    {
       
        if (inventoryHead.transform.GetChild(0).gameObject.activeSelf == true)
        {
            Debug.Log("Toggle Inventory running");
            inventoryHead.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            inventoryHead.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public Armor generateArmor()
    {
        string className = CharacterClasses.classNames[UnityEngine.Random.Range(0, CharacterClasses.classNames.Length)];
        Armor newArmor = ArmorGen.ArmorGenerate(1, className, UnityEngine.Random.Range(0, storeLevel));
        return newArmor;
    }

    public Weapons generateWeapon()
    {
        string className = CharacterClasses.classNames[UnityEngine.Random.Range(0, CharacterClasses.classNames.Length)];
        Weapons newWeapon = WeaponGen.WeaponGenerate(1, className, UnityEngine.Random.Range(0, storeLevel));
        return newWeapon;
    }

    public void populateStore(Item item)
    {
        storeImage.sprite = item.inventoryIcon;

        switch (item.getItemType())
        {
            case "Usable":
                displayUsable(item);
                break;
            case "Equipable":
                displayEquiable(item);
                break;
        }
    }

    void displayEquiable(Item item)
    {
        switch (item.getEquipable().slot)
        {
            case "Armor":
                displayArmor(item.getArmor());
                break;

            case "Weapon":
                displayWeapon(item.getWeapon());
                break;
        }
    }

    void displayArmor(Armor armor)
    {
        storeText.text = "";

        storeText.text += "Name: " + armor.name + "\n";
        storeText.text += "Cost: " + armor.cost + "\n";

        if (armor.str_bonus != 0)
            storeText.text += "Strength Bonus: " + armor.str_bonus + "\n";

        if (armor.dex_bonus != 0)
            storeText.text += "Dexterity Bonus: " + armor.dex_bonus + "\n";

        if (armor.con_bonus != 0)
            storeText.text += "Constitution Bonus: " + armor.con_bonus + "\n";

        if (armor.wis_bonus != 0)
            storeText.text += "Wisdom Bonus: " + armor.wis_bonus + "\n";

        if (armor.int_bonus != 0)
            storeText.text += "Intelligence Bonus: " + armor.int_bonus + "\n";

        if (armor.physical_def != 0)
            storeText.text += "Physical Defense: " + armor.physical_def + "\n";

        if (armor.magic_def != 0)
            storeText.text += "Magic Resistance: " + armor.magic_def + "\n";

    }

    void displayWeapon(Weapons weapon)
    {
        if (weapon.str_bonus != 0)
            storeText.text += "Strength Bonus: " + weapon.str_bonus + "\n";

        if (weapon.dex_bonus != 0)
            storeText.text += "Dexterity Bonus: " + weapon.dex_bonus + "\n";

        if (weapon.con_bonus != 0)
            storeText.text += "Constitution Bonus: " + weapon.con_bonus + "\n";

        if (weapon.wis_bonus != 0)
            storeText.text += "Wisdom Bonus: " + weapon.wis_bonus + "\n";

        if (weapon.int_bonus != 0)
            storeText.text += "Intelligence Bonus: " + weapon.int_bonus + "\n";

    }

    public void setSelectedItem(Item item)
    {
        selectedItem = item;
    }

    public void buyArmor()
    {
        if (inventoryActual.gameObject.activeSelf == false)
        {
            inventoryActual.GetComponent<Inventory>().toggleInventory();
        }

        if (selectedItem == null)
        {
            return;
        }
        inventoryActual.GetComponent<Inventory>().addEquipable(selectedItem.getArmor());
    }

    public void buyWeapon()
    {
        if (inventoryActual.gameObject.activeSelf == false)
        {
            inventoryActual.GetComponent<Inventory>().toggleInventory();
        }

        if (selectedItem == null)
        {
            return;
        }
        inventoryActual.GetComponent<Inventory>().addEquipable(selectedItem.getWeapon());
    }

    void displayUsable(Item item)
    {

    }

    void displayStats()
    {

    }
}
