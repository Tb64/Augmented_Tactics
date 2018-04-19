using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyShield : UsableItem {

    public override void InitInitialize()
    {
        base.InitInitialize();
        name = "Heavy Shield";
        image = "UI/RPG_inventory_icons/shield" ;
    }

    public override void UseItem(GameObject user, GameObject target)
    {
        base.UseItem(user, target);
        itemAbility = new BuffDebuff(user,"defense","dexterity",true,10,true);
        itemAbility.UseSkill(target);
    }
}
