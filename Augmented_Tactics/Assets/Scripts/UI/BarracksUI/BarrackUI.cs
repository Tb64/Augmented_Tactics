using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrackUI : MonoBehaviour {

    public ArmyList armyUI;
    public EquipStatsUI weaponUI;
    public EquipStatsUI armorUI;
    public GameObject dummy;
    public Transform modelTansform;

    public Text desc1;
    public Text desc2;

    public Image[] skills;
    public Image[] items;

    private Actor dummyActor;
    public Sprite noItem;
    private GameObject modelObj;

    private PlayerData currentData;

    // Use this for initialization
    void Start () {
        GameDataController.loadPlayerData();
        armyUI.LoadList(GameDataController.gameData.armyList);
        dummyActor = dummy.GetComponent<Actor>();
        SelectDefault();
        UpdatePage();
        //noItem = skills[0].sprite;
    }
	
	// Update is called once per frame
	public void UpdatePage () {
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

        if (modelObj != null) { Destroy(modelObj); }
        GameObject model = Resources.Load<GameObject>(pdata.getStringByKey(PlayerKey.Prefab));
        model.GetComponent<PlayerControlled>().combatOn = false;
        modelObj = Instantiate<GameObject>(model);
        modelObj.transform.localScale = modelTansform.lossyScale;
        modelObj.transform.SetPositionAndRotation(modelTansform.position, modelTansform.rotation);
    }

    private void SelectDefault()
    {
        GameDataController.loadPlayerData();
        currentData =  GameDataController.gameData.armyList[0];
        
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


    public void FireSoldierPopup()
    {
        //make a confirmation window to fire the soldier
        FireSoldier();
    }

    private void FireSoldier()
    {
        //throw new NotImplementedException();
        GameDataController.loadPlayerData();
        GameDataController.gameData.removePlayer(currentData);
        GameDataController.savePlayerData();
        Debug.Log("Deleted " + currentData.playerName);

        SelectDefault();
        armyUI.LoadList(GameDataController.gameData.armyList);
    }

    private void OnDisable()
    {
        if (modelObj != null) { Destroy(modelObj); }
    }

    public PlayerData GetPlayerData()
    {
        return this.currentData;
    }
}
