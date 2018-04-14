using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottoPotion : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Lotto Potion";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new PotionSkill(user, 1, true);
        itemAbility.UseSkill(target);
    }
}
