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
        canTargetTile = true;
        canTargetFriendly = true;
        canTargetEnemy = true;
        canTargetEnemy = true;
        canAffectFriendly = false;

        rangeMarkerPos = null;
        gameObject = obj;
        actor = obj.GetComponent<Actor>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    //make a ubbble or check for tiles occupied
}