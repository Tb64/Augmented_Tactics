using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottoManaTonic : UsableItem {

    //randomly recovers mana of effected player for 5 turns
    public override void InitInitialize()
    {
        base.InitInitialize();
        isManaItem = true;
        name = "Lotto Mana Tonic";
        image = "";
    }

    public override bool UseItem(GameObject user, GameObject target)
    {
        itemAbility = new ManaSkill(user,1, true);
        if (!base.UseItem(user, target))
            return false;
        return itemAbility.UseSkill(target);
    }
}
