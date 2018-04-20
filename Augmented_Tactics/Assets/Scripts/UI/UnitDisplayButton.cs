using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplayButton : MonoBehaviour {

    public Text nameText;
    public Text level;
    public Text classType;
    public Text cost;

    public Image exp;
    public Image image;

    protected PlayerData data;

    public void LoadCharacter(PlayerData input)
    {
        data = input;
        nameText.text = "Name: " + input.getStringByKey(PlayerKey.DisplayName);
        level.text = "" + input.getStatByKey(PlayerKey.Level);
        classType.text = "Class: " + input.getStringByKey(PlayerKey.ClassName);
        cost.text = "Cost: " + 500;//change to cost later
        exp.fillAmount = .4f;//input.getStringByKey(PlayerKey.);

        string icon = input.getStringByKey(PlayerKey.Icon);

        image.sprite = Resources.Load<Sprite>(icon);
    }

    /// <summary>
    /// Override this to implement what should happen when one of the buttons is clicked. 
    /// </summary>
    public virtual void ClickEvent()
    {
        SendMessageUpwards("UnitButtonClicked", data);
        //SendMessage("unitButtonClickedImage", image.sprite);
    }

    public PlayerData getPlayerData()
    { return data; }
}
