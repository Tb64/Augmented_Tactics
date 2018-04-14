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

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new PotionSkill(user, 3, false);
        itemAbility.UseSkill(target);
    }
}
