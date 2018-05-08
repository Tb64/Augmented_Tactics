using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedManaTonic : UsableItem {

    //recovers mana of effected player
    public override void InitInitialize()
    {
        base.InitInitialize();
        isManaItem = true;
        name = "Medium Mana Tonic";
        image = "UI/RPG_inventory_icons/mp";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new ManaSkill(user, 2, false);
        if (!base.UseItem(user, target))
            return false;
        itemAbility.UseSkill(target);
        return true;
    }
}
