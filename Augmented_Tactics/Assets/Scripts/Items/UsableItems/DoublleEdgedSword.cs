using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublleEdgedSword : UsableItem {

    //attack buff and defensive debuff for effected player
    public override void InitInitialize()
    {
        base.InitInitialize();
        name = "Double-Edged Sword";
        image = "UI/RPG_inventory_icons/sword"; //I'm assuming this is for later use of loading sprites? <-- it was
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new BuffDebuff(user, "strength", "defense", true,true, 10, true);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
