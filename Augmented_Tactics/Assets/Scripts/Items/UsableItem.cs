using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Items
{
    protected Ability itemAbility;
    public bool isHealItem;
    public string itemDesc;


    public virtual void InitInitialize()
    {
        isHealItem = false;
        isManaItem = false;
    }

    public virtual bool UseItem(GameObject user, GameObject target)
    {
        //Initialize ability
        //itemAbility = new Heal(user);
        //itemAbility.UseSkill(target);
        if(CanUseItem(user,target))
            return true;
        else
            return false;
    }

    public bool CanUseItem( GameObject user, GameObject target)
    {
        return itemAbility.CanUseSkill(target);
    }
}
