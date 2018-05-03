using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeManaTonic : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();
        isManaItem = true;
        name = "Large Mana Tonic";
        image = "UI/RPG_inventory_icons/mp";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new ManaSkill(user, 3, false);
        if(!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
