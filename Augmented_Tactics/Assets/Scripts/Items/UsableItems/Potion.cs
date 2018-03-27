using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();

        name = "Potion";
        image = "";
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new PotionSkill(user);
        itemAbility.UseSkill(target);

        //destroy?
    }
}
