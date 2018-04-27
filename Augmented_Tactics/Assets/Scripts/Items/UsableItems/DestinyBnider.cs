using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyBnider : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();
        name = "Destiny Binder";
        image = "UI/RPG_inventory_icons/rings";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new Binder(user, true);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
