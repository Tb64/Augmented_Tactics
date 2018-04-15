using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHeal : StatusEffects
{
    private GameObject effect1,effect2; //will be set to new glow effect and display text
    private float heal;
    public RandomHeal(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Lucky Health";
        duration = 4;
        effectedPlayer = effected;
        effectorPlayer = effector;
        this.isEnemy = isEnemy;
        anim = effected.gameObject.GetComponentInChildren<Animator>();
    }

    public override void InduceEffect()
    {
        switch (duration)
        {
            case 4:
                heal = Random.Range(1, 20);
                break;

            case 3:
                heal = Random.Range(1, 30);
                break;

            case 2:
                heal = Random.Range(1, 40);
                break;

            case 1:
                heal = Random.Range(1, 50);
                break;
        }
        effectedPlayer.HealHealth(heal);
        Debug.Log(effectedPlayer + " healing random amount " + heal + " of Health");

    }

}
