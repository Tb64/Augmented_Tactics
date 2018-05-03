using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPotion : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();
        isHealItem = true;
        name = "Small Potion";
        image = "UI/RPG_inventory_icons/hp";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new PotionSkill(user, 1,false);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
