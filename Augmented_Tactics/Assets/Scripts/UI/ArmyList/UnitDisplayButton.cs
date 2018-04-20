using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplayButton : MonoBehaviour {

    public Text nameText;
    public Text level;
    public Text classType;

    public Image exp;
    public Image image;

    protected PlayerData data;

    public virtual void LoadCharacter(PlayerData input)
    {
        data = input;
        nameText.text = "" + input.getStringByKey(PlayerKey.DisplayName);
        level.text = "" + input.getStatByKey(PlayerKey.Level);
        classType.text = "" + input.getStringByKey(PlayerKey.ClassName);
        if (input.Experience == 0)
            exp.fillAmount = 0f;
        else
            exp.fillAmount = input.Experience/input.Experience;//input.getStringByKey(PlayerKey.);

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
