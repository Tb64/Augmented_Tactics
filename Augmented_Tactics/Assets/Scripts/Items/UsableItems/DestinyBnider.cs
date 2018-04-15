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

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new Binder(user, true);
        itemAbility.UseSkill(target);
    }
}
