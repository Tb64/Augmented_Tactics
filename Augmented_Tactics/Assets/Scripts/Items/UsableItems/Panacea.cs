using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panacea : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Small Mana Tonic";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new CureStatus(user, 1);
        itemAbility.UseSkill(target);
    }
}
