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

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new ManaSkill(user, 2, false);
        itemAbility.UseSkill(target);
    }
}
