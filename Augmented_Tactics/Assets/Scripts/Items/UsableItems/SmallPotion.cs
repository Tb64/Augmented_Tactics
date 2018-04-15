using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPotion : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Small Potion";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new PotionSkill(user, 1, false);
        itemAbility.UseSkill(target);

        //destroy?
    }
}
