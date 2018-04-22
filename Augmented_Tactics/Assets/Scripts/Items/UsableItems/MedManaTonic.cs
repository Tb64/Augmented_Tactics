using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedManaTonic : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();
        name = "Medium Mana Tonic";
        image = "";
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
