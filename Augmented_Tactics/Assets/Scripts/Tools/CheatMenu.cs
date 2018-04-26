using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour {

    public ArmyList armyUI;
    public EquipStatsUI weaponUI;
    public EquipStatsUI armorUI;
    public GameObject dummy;
    public Text desc1;
    public Text desc2;

    public Image[] skills;
    public Image[] items;
    public Sprite noItem;

    private Actor dummyActor;
    private PlayerData currentData;

    void Start()
    {
        GameDataController.loadPlayerData();
        armyUI.LoadList(GameDataController.gameData.armyList);
        dummyActor = dummy.GetComponent<Actor>();
        SelectDefault();
        UpdatePage();
        //noItem = skills[0].sprite;
    }

    public void UpdatePage()
    {
        UnitButtonClicked(currentData);

    }

    public void UnitButtonClicked(PlayerData pdata)
    {
        currentData = pdata;
        dummyActor.LoadStatsFromData(pdata);
        UpdateSkills();
        UpdateText(pdata);
        weaponUI.DrawStats(pdata.weapon);
        armorUI.DrawStats(pdata.armor);
    }

    private void SelectDefault()
    {
        GameDataController.loadPlayerData();
        currentData = GameDataController.gameData.armyList[0];

    }

    private void UpdateSkills()
    {
        for (int index = 0; index < 4; index++)
        {
            skills[index].sprite = noItem;
            if (dummyActor.abilitySet[index] != null &&
                dummyActor.abilitySet[index].abilityImage != null)
                skills[index].sprite = dummyActor.abilitySet[index].abilityImage;
        }

    }

    private void UpdateText(PlayerData pdata)
    {
        string descText1 = "";
        descText1 += pdata.DisplayName + "\n";
        descText1 += pdata.ClassName + "\n";
        descText1 += "Level: " + pdata.Level + "\n";
        descText1 += "EXP: " + pdata.Experience + "\n";
        descText1 += "HP: " + pdata.getTotalMaxHealth() + "\n";
        descText1 += "MP: " + pdata.getTotalMaxMana() + "\n";

        string descText2 = "\n";
        descText2 += "Str: " + pdata.getTotalStr() + "\n";
        descText2 += "Dex: " + pdata.getTotalDex() + "\n";
        descText2 += "Con: " + pdata.getTotalCon() + "\n";
        descText2 += "Wis: " + pdata.getTotalWis() + "\n";
        descText2 += "Int: " + pdata.getTotalInt() + "\n";

        desc1.text = descText1;
        desc2.text = descText2;
    }

    public void LevelUp()
    {
        PlayerData.LevelUp(currentData, true);
        GameDataController.gameData.savePlayer(currentData);
        GameDataController.savePlayerData();
        UpdatePage();
    }

    public PlayerData GetPlayerData()
    {
        return this.currentData;
    }
}
