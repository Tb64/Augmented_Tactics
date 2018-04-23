using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipStatsUI : MonoBehaviour {

    public Image icon;
    public Text desc1;
    public Text desc2;

    public void Start()
    {
        desc1.text = "";
        desc2.text = "";
    }

    public void DrawStats(Armor item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);
        desc1.text = ArmorToText1(item);
        desc2.text = ArmorToText2(item);
    }

    public void DrawStats(Weapons item)
    {
        icon.sprite = Resources.Load<Sprite>(item.image);
        desc1.text = WeaponToText1(item);
        desc2.text = WeaponToText2(item);
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
}
