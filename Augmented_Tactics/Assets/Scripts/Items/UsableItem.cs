using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Items
{
    protected Ability itemAbility;

    public string itemDesc;


    public virtual void InitInitialize()
    {
        
    }

    public virtual void UseItem(GameObject user, GameObject target)
    {
        //Initialize ability
        //itemAbility = new Heal(user);
        //itemAbility.UseSkill(target);


    }
}
