using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitUnitDisplayButton : UnitDisplayButton
{ 
    public Text cost;

    public void LoadCharacter(PlayerData input)
    {
        data = input;
        nameText.text = "" + input.getStringByKey(PlayerKey.DisplayName);
        level.text = "" + input.getStatByKey(PlayerKey.Level);
        classType.text = "" + input.getStringByKey(PlayerKey.ClassName);
        cost.text = "Cost: " + 500;//change to cost later
        if (input.Experience == 0)
            exp.fillAmount = 0f;
        else
            exp.fillAmount = input.Experience / input.Experience;//input.getStringByKey(PlayerKey.);

        string icon = input.getStringByKey(PlayerKey.Icon);

        image.sprite = Resources.Load<Sprite>(icon);
    }
}
