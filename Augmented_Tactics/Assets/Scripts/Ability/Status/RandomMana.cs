using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMana : StatusEffects {

    private float heal;
    public RandomMana(float effect, Actor effector, Actor effected, bool isEnemy) : base(effect, effector, effected, isEnemy)
    {
        TurnBehaviour.OnTurnStart += this.decreaseTimeCounter;
        effectText = "Lucky Mana";
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
            case 4 :
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
        if(effectedPlayer.getManaCurrent() < 0)
            effectedPlayer.setManaCurrent(0 + heal);
        else
            effectedPlayer.setManaCurrent(effectedPlayer.getManaCurrent()+heal);
        Debug.Log(effectedPlayer + " healing random amount " + heal + " of Mana");
            
    }

}
