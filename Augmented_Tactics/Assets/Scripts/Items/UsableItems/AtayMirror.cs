using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtayMirror : UsableItem {
    /*Dervied from the Yata Mirror in Naruto. Counters any attack with double power*/
    public override void InitInitialize()
    {
        base.InitInitialize();
        name = "Atay Mirror";
        image = "UI/RPG_inventory_icons/sword"; //I'm assuming this is for later use of loading sprites?
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new CounterStrike(user);
        itemAbility.UseSkill(target);
    }
}
