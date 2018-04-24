using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour {

    public bool armorUI;

    public Image equipedImg;
    public Text equipedText1;
    public Text equipedText2;

    public Image newImg;
    public Text newText1;
    public Text newText2;

    public Image[] inventoryImg;

    public Button Confirm;
    public Button BackPage;
    public Button NextPage;

    public PlayerData pdata;
    public GameData gdata;

    private int currentlySelected = -1;
    public Sprite nullImage;

    private List<Armor> armors;
    private List<Weapons> weapons;

    // Use this for initialization
    void Start() {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
        //nullImage = newImg.sprite;
        gdata = GameDataController.loadPlayerData();

        if(pdata == null)
            pdata = gdata.armyList[0];
        LoadEquipementUI(pdata, armorUI);
    }

    /// <summary>
    /// Loads the UI with the charcter data and armor/weapon displays
    /// </summary>
    /// <param name="data">The character data you want to load</param>
    /// <param name="armorEquip">True=Armor False=Weapons</param>
    public void LoadEquipementUI(PlayerData data, bool armorEquip)
    {
        //nullImage = newImg.sprite;
        gdata = GameDataController.loadPlayerData();
        pdata = data;
        armorUI = armorEquip;
        newText1.text = "";
        newText2.text = "";
        newImg.sprite = nullImage;
        equipedImg.sprite = nullImage;
        Confirm.interactable = false;
        LoadInventory(armorEquip);
        LoadPlayer(data, armorEquip);
    }

    private void LoadPlayer(PlayerData data, bool armorEquip)
    {
        if (armorEquip)
        {
            equipedImg.sprite = Resources.Load<Sprite>(data.armor.image);
            equipedText1.text = ArmorToText1(data.armor);
            equipedText2.text = ArmorToText2(data.armor);
        }
        else
        {
            equipedImg.sprite = Resources.Load<Sprite>(data.weapon.image);
            equipedText1.text = WeaponToText1(data.weapon);
            equipedText2.text = WeaponToText2(data.weapon);
        }
    }

    private void LoadInventory(bool armorEquip)
    {
        InventoryReset();
        if (armorEquip)
        {
            armors = gdata.armors;
            int index = 0;
            foreach (Armor item in armors)
            {
                if(item.class_req == pdata.ClassName)
                {
                    Debug.Log("Loading slot " + index + " with " + item.name);
                    inventoryImg[index].sprite = Resources.Load<Sprite>(item.image);
                    index++;
                }
                if (index == 25)
                    return;
            }
        }
        else
        {
            weapons = gdata.weapons;
            int index = 0;
            foreach (Weapons item in weapons)
            {
                if (item.class_req == pdata.ClassName)
                {
                    Debug.Log("Loading slot " + index + " with " + item.name);
                    inventoryImg[index].sprite = Resources.Load<Sprite>(item.image);
                    index++;
                }
                if (index == 25)
                    return;

            }
        }
    }

    private void InventoryReset()
    {
        foreach (Image img in inventoryImg)
        {
            img.sprite = nullImage;
        }
    }

    private string ArmorToText1(Armor item)
    {
        string desc1 = "";

        desc1 += "Level: " + item.level_req + " - " + item.name + "\n";
        desc1 += "Physical Defense: " + item.physical_def + "\n";
        desc1 += "Magic Defense: " + item.magic_def + "\n";
        desc1 += "Str: " + item.str_bonus + "\n";
        desc1 += "Dex: " + item.dex_bonus + "\n";
        desc1 += "Con: " + item.con_bonus + "\n";

        return desc1;
    }

    private string ArmorToText2(Armor item)
    {
        string desc2 = "";

        desc2 += "\n";
        desc2 += "\n";
        desc2 += "\n";
        desc2 += "Wis: " + item.wis_bonus + "\n";
        desc2 += "Int: " + item.int_bonus + "\n";

        return desc2;
    }

    private string WeaponToText1(Weapons item)
    {
        string desc1 = "";

        desc1 += "Level: " + item.level_req + " - " + item.name + "\n";
        desc1 += "Physical Damage: " + item.physical_dmg_min + "-" + item.physical_dmg_max + "\n";
        desc1 += "Magic Damage: " + item.magic_dmg_min + "-" + item.magic_dmg_max + "\n";
        desc1 += "Str: " + item.str_bonus + "\n";
        desc1 += "Dex: " + item.dex_bonus + "\n";
        desc1 += "Con: " + item.con_bonus + "\n";

        return desc1;
    }

    private string WeaponToText2(Weapons item)
    {
        string desc2 = "";

        desc2 += "\n";
        desc2 += "\n";
        desc2 += "\n";
        desc2 += "Wis: " + item.wis_bonus + "\n";
        desc2 += "Int: " + item.int_bonus + "\n";

        return desc2;
    }

    public void SelectionChanged(int index)
    {
        if (index < 0)
            return;
        currentlySelected = index;
        if (armorUI)
        {
            if (index >= armors.Count)
                return;
            Armor item = armors[index];
            newImg.sprite = Resources.Load<Sprite>(item.image);
            newText1.text = ArmorToText1(item);
            newText2.text = ArmorToText2(item);
            if (item.level_req <= pdata.Level)
                Confirm.interactable = true;
            else
                Confirm.interactable = false;
        }
        else
        {
            if (index >= weapons.Count)
                return;
            Weapons item = weapons[index];
            newImg.sprite = Resources.Load<Sprite>(item.image);
            newText1.text = WeaponToText1(item);
            newText2.text = WeaponToText2(item);
            if (item.level_req <= pdata.Level)
                Confirm.interactable = true;
            else
                Confirm.interactable = false;
        }
    }

    public void EquipButton()
    {
        Debug.Log("Current: " + currentlySelected);
        if(armorUI)
        {
            Armor oldItem = pdata.armor;
            Armor newItem = armors[currentlySelected];

            pdata.armor = newItem;
            gdata.armors.Remove(newItem);
            gdata.armors.Add(oldItem);
        }
        else
        {
            Weapons oldItem = pdata.weapon;
            Weapons newItem = weapons[currentlySelected];

            pdata.weapon = newItem;
            gdata.weapons.Remove(newItem);
            gdata.weapons.Add(oldItem);
        }
        gdata.savePlayer(pdata);
        GameDataController.savePlayerData(gdata);
        LoadEquipementUI(pdata, armorUI);
    }
}
