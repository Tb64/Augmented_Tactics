using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaTonic : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Small Mana Tonic";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new ManaSkill(user,1,false);
        itemAbility.UseSkill(target);
    }

}
