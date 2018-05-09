using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff : Ability {

    private string type, type2;
    private int numTurns;
    private bool price,item, buff;
    private float effect;
    public BuffDebuff(GameObject obj, string type,string type2, bool sacrifice, bool buff, float effect,bool item)
    {
        this.type = type;
        this.type2 = type2; 
        this.effect = effect;
        this.item = item;
        this.buff = buff;
        price = sacrifice;
        Initialize(obj);
    }

    public override void ActionSkill(GameObject target)
    {
        Actor targeta = target.GetComponent<Actor>(), actor = gameObject.GetComponent<Actor>();

        if (price)
        {
            Debug.Log(targeta + "'s "+type+ " Will Be Buffed At The Expense of " +type2);
            Buff(targeta,target,type);
            Debuff(targeta,target,type2);
        }
        else
        {
            if (buff)
                Buff(targeta, target,type);
            else
                Debuff(targeta, target,type);
        }
        DwellTime.Attack(dwell_time);
}

    private void Buff(Actor targeta, GameObject target,string type)
    {
        switch (type)
        {
            case "physicaldefense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", true,true));
                break;
            case "magicdefense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", true,false));
                break;
            case "magicaldefense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", true, false));
                break;
            case "strength":
                StatusEffectsController.AddEffect(new BuffStrength(effect, actor, targeta, target.tag == "Enemy", true));
                break;
            case "dexterity":
               // Debug.Log("Buff Still True");
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
    
    private void Debuff(Actor targeta, GameObject target, string type)
    {
        switch (type)
        {
            case "physicaldefense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", false,true));
                break;
            case "defense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", false, true));
                break;
            case "magicdefense":
                StatusEffectsController.AddEffect(new BuffDefense(effect, actor, targeta, target.tag == "Enemy", false, false));
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
        if (item)
            manaCost = 0;
        else
            manaCost = 8;
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill7");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    public static void SwapEffect(bool buff, Actor effectorPlayer, Actor effectedPlayer, float effect, string type)
    {
        type = type.ToLower();
        switch (type)
        {
            case "physicaldefense":
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

            case "magicaldefense":
                if (buff)
                {
                    Debug.Log(effectorPlayer + " Buffing " + effectedPlayer + "'s Magic Defense by " + effect);
                    effectedPlayer.setMagicalDefense(effectedPlayer.getMagicalDefense() + (int)effect);
                    break;
                }
                else
                {
                    Debug.Log(effectorPlayer + " Debuffing " + effectedPlayer + "'s Magic Defense by " + effect);
                    effectedPlayer.setMagicalDefense(effectedPlayer.getMagicalDefense() - (int)effect);
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

            case "wisdom":
                if (buff)
                {
                    Debug.Log(effectorPlayer + " Buffing " + effectedPlayer + "'s Wisdom by " + (int)effect);
                    effectedPlayer.setWisdom(effectedPlayer.getWisdom() + (int)effect);
                    break;
                }
                else
                {
                    Debug.Log(effectorPlayer + " Debuffing " + effectedPlayer + "'s Wisdom by " + (int)effect);
                    effectedPlayer.setWisdom(effectedPlayer.getWisdom() - (int)effect);
                    break;
                }

        }
        
    }

    public static string[] GetStatCalls()
    {
        string[] stats = { "physicaldefense", "magicaldefense", "strength", "dexterity", "wisdom" };
        return stats;
    }
}
