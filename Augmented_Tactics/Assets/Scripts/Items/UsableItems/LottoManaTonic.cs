using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottoManaTonic : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Lotto Mana Tonic";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new ManaSkill(user, 1, true);
        itemAbility.UseSkill(target);
    }
}
