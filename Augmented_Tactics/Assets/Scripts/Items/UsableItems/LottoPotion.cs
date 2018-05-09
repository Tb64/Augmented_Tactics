﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottoPotion : UsableItem {

    //Randomly heals health of effected actor
    public override void InitInitialize()
    {
        base.InitInitialize();
        isHealItem = true;
        name = "Lotto Potion";
        image = "";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new PotionSkill(user,1, true);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
