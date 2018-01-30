using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOE : Ability
{
    protected bool canAffectFriendly;
    protected bool canAffectEnemy;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        canTargetTile = true;
        canTargetFriendly = true;
        canTargetEnemy = true;
        canTargetEnemy = true;
        canAffectFriendly = false;
        
    }



    //make a bubble or check for tiles occupied
}