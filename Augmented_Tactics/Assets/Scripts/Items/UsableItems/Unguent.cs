using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unguent : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Small Mana Tonic";
        image = "UI/RPG_inventory_icons/book";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new CureStatus(user, 0);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
