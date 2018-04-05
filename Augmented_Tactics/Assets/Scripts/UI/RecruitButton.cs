using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour {

    public Text nameText;
    public Text level;
    public Text classType;
    public Text cost;

    public Image exp;
    public Image image;

    public TEMP_CharacterData character;

    private PlayerData data;

    private RecruitUI controller;

    public void LoadCharacter(TEMP_CharacterData input)
    {

    }

    public void LoadCharacter(PlayerData input, RecruitUI ctrl)
    {
        data = input;
        controller = ctrl;
        nameText.text = "Name: " + input.getStringByKey(PlayerKey.DisplayName);
        level.text = "Level: " + input.getStatByKey(PlayerKey.Level);
        classType.text = "Class: " + input.getStringByKey(PlayerKey.ClassName);
        cost.text = "Cost: " + 500;//change to cost later
        exp.fillAmount = .4f;//input.getStringByKey(PlayerKey.);

        string icon = input.getStringByKey(PlayerKey.Icon);

        image.sprite = Resources.Load<Sprite>(icon);
    }

    public void ChangeSelected()
    {
        controller.ChangeSelected(data, image.sprite);
    }

    public PlayerData getPlayerData()
    { return data; }
}
