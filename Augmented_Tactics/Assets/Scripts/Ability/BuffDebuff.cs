using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff : Ability {

    private string type, type2;
    private bool price;
    private float effect;
    public BuffDebuff(GameObject obj, string type,string type2, bool sacrifice, float effect)
    {
        this.type = type;
        this.type2 = type2; // if none type just send null or ""
        this.effect = effect;
        price = sacrifice;
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>(), actor = gameObject.GetComponent<Actor>();

        if (price)
        {
            Debug.Log(targeta + "'s "+type+ " Will Be Buffed At A Price");
            Buff(targeta,target);
            Debuff(targeta,target);
        }
        else
        {
            Buff(targeta,target);
        }
        DwellTime.Attack(dwell_time);
}

    private void Buff(Actor targeta, GameObject target)
    {
        switch (type)
        {
            case "defense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", true));
                break;

            case "strength":
                StatusEffectsController.AddEffect(new BuffStrength(effect, actor, targeta, target.tag == "Enemy", true));
                break;
            case "dexterity":
                StatusEffectsController.AddEffect(new BuffDexterity(effect, actor, targeta, target.tag == "Enemy", true));
                break;
            case "wisdom":
                StatusEffectsController.AddEffect(new BuffWisdom(effect, actor, targeta, target.tag == "Enemy", true));
                break;
            default:
                Debug.LogError(type + " Must Be Added To The Switch Statement Above^^^");
                break;
        }

    }

    private void Debuff(Actor targeta, GameObject target)
    {
        switch (type)
        {
            case "defense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", false));
                break;

            case "strength":
                StatusEffectsController.AddEffect(new BuffStrength(effect, actor, targeta, target.tag == "Enemy", false));
                break;
            case "dexterity":
                StatusEffectsController.AddEffect(new BuffDexterity(effect, actor, targeta, target.tag == "Enemy", false));
                break;
            case "wisdom":
                StatusEffectsController.AddEffect(new BuffWisdom(effect, actor, targeta, target.tag == "Enemy", false));
                break;
            default:
                Debug.LogError(type + " Must Be Added To The Switch Statement Above^^^");
                break;
        }
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Buff Debuff";
        manaCost = 0;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    public static void SwapEffect(bool buff, Actor effectorPlayer, Actor effectedPlayer, float effect, string type)
    {
        type = type.ToLower();
        switch (type)
        {
            case "defense":
                if (buff)
                {
                    Debug.Log(effectorPlayer + " Buffing " + effectedPlayer + "'s Defense by " + effect);
                    effectedPlayer.setPhysicalDefense(effectedPlayer.getPhysicalDefense() + (int)effect);
                    break;
                }
                else
                {
                    Debug.Log(effectorPlayer + " Debuffing " + effectedPlayer + "'s Defense by " + effect);
                    effectedPlayer.setPhysicalDefense(effectedPlayer.getPhysicalDefense() - (int)effect);
                    break;
                }

            case "strength":
                if (buff)
                {
                    Debug.Log(effectorPlayer + " Buffing " + effectedPlayer + "'s Strength by " + effect);
                    effectedPlayer.setStrength(effectedPlayer.getStrength() + (int)effect);
                    break;
                }
                else
                {
                    Debug.Log(effectorPlayer + " Debuffing " + effectedPlayer + "'s Strength by " + effect);
                    effectedPlayer.setStrength(effectedPlayer.getStrength() - (int)effect);
                    break;
                }

            case "dexterity":
                if (buff)
                {
                    Debug.Log(effectorPlayer + " Buffing " + effectedPlayer + "'s Dexterity by " + (int)effect);
                    effectedPlayer.setDexterity(effectedPlayer.getDexterity() + (int)effect);
                    break;
                }
                else
                {
                    Debug.Log(effectorPlayer + " Debuffing " + effectedPlayer + "'s Dexterity by " + (int)effect);
                    effectedPlayer.setDexterity(effectedPlayer.getDexterity() - (int)effect);
                    break;
                }
        }
        
    }
}
