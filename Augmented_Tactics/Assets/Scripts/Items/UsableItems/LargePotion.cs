using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePotion : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Large Potion";
        image = "";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new PotionSkill(user,3, false);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
