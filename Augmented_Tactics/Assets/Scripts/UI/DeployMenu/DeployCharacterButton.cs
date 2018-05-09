using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployCharacterButton : MonoBehaviour {

    public Text nameText;
    public Text level;
    public Text classType;

    public Image image;

    public TEMP_CharacterData character;

    private PlayerData data;

    private DeployController controller;

    public void LoadCharacter(TEMP_CharacterData input)
    {

    }

    public void LoadCharacter(PlayerData input, DeployController ctrl)
    {
        data = input;
        controller = ctrl;
        nameText.text = "Name: " + input.getStringByKey(PlayerKey.DisplayName);
        level.text = "Level: " + input.getStatByKey(PlayerKey.Level);
        classType.text = "Class: " + input.getStringByKey(PlayerKey.ClassName);

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
