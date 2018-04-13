using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLoader : MonoBehaviour {

    /// <summary>
    /// Used for loading skill in to actor.  Note that to add new skills the text input is converted to lowercase, so switch only works with lowercase.
    /// </summary>
    /// <param name="skillID">String of the skill to be loaded</param>
    /// <param name="gObj">The gamobject that the skill is being added to</param>
    /// <returns></returns>
    static public Ability LoadSkill(string skillID, GameObject gObj)
    {
        skillID = skillID.ToLower();
        switch (skillID)
        {
            // ALL CASES MUST BE LOWER CASE!!!!


            case "basicattack":
                Debug.Log("Loading BasicAttack");
                return new BasicAttack(gObj);

            case "heal":
                return new Heal(gObj);

            case "fire":
                return new Fire(gObj);

            //Brawler
            case "combo":
                Debug.Log("Loading Combo");
                return new Combo(gObj);

            case "cyclonekick":
                return new CycloneKick(gObj);

            case "dragonkick":
                return new DragonKick(gObj);

            case "howlingfist":
                return new HowlingFist(gObj);

            case "gutpunch":
                return new GutPunch(gObj);

            case "twinstrike":
                return new TwinStrike(gObj);

            case "counter":
                return new Counter(gObj);

            //Knight

            case "eviscerate":
                return new Eviscerate(gObj);

            case "disintegrate":
                return new Disintegrate(gObj);

            case "lifeleech":
                return new LifeLeech(gObj);

            case "unholylightning":
                return new UnholyLightning(gObj);

            case "shieldbash":
                return new ShieldBash(gObj);

            //Wizard
            case "ice":
                return new Ice(gObj);

            case "thunder":
                return new Thunder(gObj);

            case "fireball":
                return new FireBall(gObj);


            //Cleric
            case "curewounds":
                return new CureWounds(gObj);
            case "healingword":
                return new HealingWord(gObj);
            case "beaconofhope":
                return new BeaconOfHope(gObj);
            case "spikegrowth":
                return new SpikeGrowth(gObj);
            case "shieldoffaith":
                return new ShieldOfFaith(gObj);

            default:
                return null;
        }
    }

    public static string[] ClassSkills(int classID)
    {
        string[] skills = new string[8];
        switch (classID)
        {
            case CharacterClasses.BrawlerKey:
                skills[0] = "twinstrike";
                skills[1] = "combo";
                skills[2] = "dragonkick";
                skills[3] = "counter";
                skills[4] = "gutpunch";
                skills[5] = "cyclonekick";
                skills[6] = "";
                skills[7] = "howlingfist";
                return skills;

            case CharacterClasses.ThiefKey:
                skills[0] = "";
                skills[1] = "";
                skills[2] = "";
                skills[3] = "";
                skills[4] = "";
                skills[5] = "";
                skills[6] = "";
                skills[7] = "";
                return skills;

            case CharacterClasses.DarkKnightKey:
                skills[0] = "";
                skills[1] = "";
                skills[2] = "";
                skills[3] = "";
                skills[4] = "";
                skills[5] = "";
                skills[6] = "";
                skills[7] = "";
                return skills;

            case CharacterClasses.ClericKey:
                skills[0] = "curewounds";
                skills[1] = "healingword";
                skills[2] = "spikegrowth";
                skills[3] = "";
                skills[4] = "beaconofhope";
                skills[5] = "";
                skills[6] = "";
                skills[7] = "shieldsoffaith";
                return skills;

            case CharacterClasses.MageKey:
                skills[0] = "";
                skills[1] = "";
                skills[2] = "";
                skills[3] = "";
                skills[4] = "";
                skills[5] = "";
                skills[6] = "";
                skills[7] = "";
                return skills;


            default:
                break;
        }

        return null;
    }

    public static string[] ClassSkills(string input)
    {
        return ClassSkills(CharacterClasses.StringToKey(input));
    }

}
