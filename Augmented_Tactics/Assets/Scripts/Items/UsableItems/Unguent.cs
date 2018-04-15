using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unguent : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Small Mana Tonic";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new CureStatus(user, 0);
        itemAbility.UseSkill(target);
    }
}
