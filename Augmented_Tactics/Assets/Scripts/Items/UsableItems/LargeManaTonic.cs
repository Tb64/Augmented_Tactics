using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeManaTonic : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Large Mana Tonic";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new ManaSkill(user, 3, false);
        itemAbility.UseSkill(target);
    }
}
