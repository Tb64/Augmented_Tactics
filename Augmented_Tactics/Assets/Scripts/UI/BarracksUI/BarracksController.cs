using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksController : MonoBehaviour {

    public BarrackUI barracksUI;
    public EquipmentUI equipUI;
    public SkillSelectionMenu skillUI;


    public void EnableEquip(bool armor)
    {
        PlayerData pdata = barracksUI.GetPlayerData();
        //barracksUI.gameObject.SetActive(false);
        equipUI.gameObject.SetActive(true);
        equipUI.LoadEquipementUI(pdata, armor);
    }

    public void EnableSkill()
    {
        PlayerData pdata = barracksUI.GetPlayerData();
        //barracksUI.gameObject.SetActive(false);
        skillUI.gameObject.SetActive(true);
        skillUI.SetPlayerData(pdata);
    }

    public void EnableBarracks()
    {

    }
}
