using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePotion : UsableItem {

    //Heals health of effected actor
    public override void InitInitialize()
    {
        base.InitInitialize();
        isHealItem = true;
        name = "Large Potion";
        image = "UI/RPG_inventory_icons/hp";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new PotionSkill(user,3, false);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
